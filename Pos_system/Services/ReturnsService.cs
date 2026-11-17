using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.Models;
using Pos_system.logs;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Services
{
    public class ReturnsService
    {
        public async Task<ReturnResult> ProcessReturn(ReturnRequest request)
        {
            AuthorizationGuard.RequireProcessReturns();
            AppContext.EnsureBranchAccess(request.BranchId);

            if (request.Items.Count == 0)
                return new ReturnResult { Approved = false, Message = "No items selected for return." };

            if (request.RefundType == "StoreCredit" && string.IsNullOrWhiteSpace(request.CustomerPhone))
            {
                return new ReturnResult
                {
                    Approved = false,
                    Message = "Customer phone number is required when issuing store credit."
                };
            }

            decimal totalRefund = 0;

            using var conn = DBConnection.GetConnection();
            await using var transaction = await conn.BeginTransactionAsync();

            try
            {
                const string saleCheck = "SELECT BranchId FROM sales WHERE id = @saleId";
                using (var checkCmd = new NpgsqlCommand(saleCheck, conn, transaction))
                {
                    checkCmd.Parameters.AddWithValue("@saleId", request.SaleId);
                    var branchObj = await checkCmd.ExecuteScalarAsync();
                    if (branchObj == null)
                        return new ReturnResult { Approved = false, Message = "Sale not found." };

                    var saleBranchId = Convert.ToInt32(branchObj);
                    if (saleBranchId != request.BranchId)
                        return new ReturnResult { Approved = false, Message = "This sale belongs to a different branch." };
                }

                const string returnString = @"
                    INSERT INTO returns(receipt_no, sale_id, reason, refund_type, refund_amount, requested_by, approved_by, branch_id, customer_phone)
                    VALUES(@receipt_no, @sale_id, @reason, @refund_type, 0, @requested_by, @approved_by, @branch_id, @customer_phone)
                    RETURNING id";

                int returnId;
                using (var cmd = new NpgsqlCommand(returnString, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@receipt_no", request.ReceiptNo);
                    cmd.Parameters.AddWithValue("@sale_id", request.SaleId);
                    cmd.Parameters.AddWithValue("@reason", request.Reason);
                    cmd.Parameters.AddWithValue("@refund_type", request.RefundType);
                    cmd.Parameters.AddWithValue("@requested_by", request.RequestedBy);
                    cmd.Parameters.AddWithValue("@approved_by", request.ApprovedBy > 0 ? request.ApprovedBy : request.RequestedBy);
                    cmd.Parameters.AddWithValue("@branch_id", request.BranchId);
                    cmd.Parameters.AddWithValue("@customer_phone",
                        string.IsNullOrWhiteSpace(request.CustomerPhone) ? DBNull.Value : (object)request.CustomerPhone.Trim());
                    returnId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                foreach (var item in request.Items)
                {
                    const string qtyCheck = @"
                        SELECT
                            si.quantity,
                            COALESCE((SELECT SUM(ri.qty_returned)
                                      FROM return_items ri
                                      WHERE ri.sale_item_id = si.id), 0) AS already_returned
                        FROM sale_items si
                        WHERE si.id = @id AND si.sale_id = @saleId";

                    int soldQty;
                    int alreadyReturned;

                    using (var qtyCmd = new NpgsqlCommand(qtyCheck, conn, transaction))
                    {
                        qtyCmd.Parameters.AddWithValue("@id", item.SaleItemId);
                        qtyCmd.Parameters.AddWithValue("@saleId", request.SaleId);

                        using var reader = await qtyCmd.ExecuteReaderAsync();
                        if (!await reader.ReadAsync())
                            throw new InvalidOperationException($"Sale item {item.SaleItemId} not found on sale {request.SaleId}.");

                        soldQty = reader.GetInt32(0);
                        alreadyReturned = reader.GetInt32(1);
                    }

                    if (item.QtyToReturn <= 0)
                        throw new InvalidOperationException($"Return quantity must be greater than zero for item {item.SaleItemId}.");

                    if (alreadyReturned + item.QtyToReturn > soldQty)
                        throw new InvalidOperationException(
                            $"Cannot return {item.QtyToReturn} of this item. " +
                            $"Sold: {soldQty}, already returned: {alreadyReturned}, " +
                            $"remaining returnable: {soldQty - alreadyReturned}.");

                    const string insertItem = @"
                        INSERT INTO return_items(return_id, sale_item_id, product_id, qty_returned, line_amount)
                        VALUES(@return_id, @sale_item_id, @product_id, @qty, @amount)";

                    using var cmd = new NpgsqlCommand(insertItem, conn, transaction);
                    cmd.Parameters.AddWithValue("@return_id", returnId);
                    cmd.Parameters.AddWithValue("@sale_item_id", item.SaleItemId);
                    cmd.Parameters.AddWithValue("@product_id", item.ProductId);
                    cmd.Parameters.AddWithValue("@qty", item.QtyToReturn);
                    cmd.Parameters.AddWithValue("@amount", item.LineAmount);
                    await cmd.ExecuteNonQueryAsync();

                    if (item.Restock)
                    {
                        const string stockUpdate = @"UPDATE products SET stock_quantity = stock_quantity + @qty
                                                     WHERE idproducts = @pid AND BranchId = @branchId";
                        using var stockCmd = new NpgsqlCommand(stockUpdate, conn, transaction);
                        stockCmd.Parameters.AddWithValue("@qty", item.QtyToReturn);
                        stockCmd.Parameters.AddWithValue("@pid", item.ProductId);
                        stockCmd.Parameters.AddWithValue("@branchId", request.BranchId);
                        await stockCmd.ExecuteNonQueryAsync();

                        const string movement = @"INSERT INTO stock_movements(product_id, type, quantity, reference_id, branch_id)
                                                  VALUES (@pid, 'RETURN', @qty, @returnId, @branchId)";
                        using var moveCmd = new NpgsqlCommand(movement, conn, transaction);
                        moveCmd.Parameters.AddWithValue("@pid", item.ProductId);
                        moveCmd.Parameters.AddWithValue("@qty", item.QtyToReturn);
                        moveCmd.Parameters.AddWithValue("@returnId", returnId);
                        moveCmd.Parameters.AddWithValue("@branchId", request.BranchId);
                        await moveCmd.ExecuteNonQueryAsync();
                    }

                    totalRefund += item.LineAmount;
                }

                const string updateRefund = "UPDATE returns SET refund_amount=@amt WHERE id=@id";
                using (var cmd = new NpgsqlCommand(updateRefund, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@amt", totalRefund);
                    cmd.Parameters.AddWithValue("@id", returnId);
                    await cmd.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();

                await AppServices.GetRequiredService<AuditService>().LogAsync(
                    "RETURN_PROCESSED",
                    "return",
                    returnId.ToString(),
                    $"Refund: {totalRefund:C}, Approved by user {request.ApprovedBy}");

                // Issue store credit AFTER the return has been committed.
                if (request.RefundType == "StoreCredit" && !string.IsNullOrWhiteSpace(request.CustomerPhone))
                {
                    try
                    {
                        var creditDao = AppServices.GetRequiredService<StoreCreditDAO>();
                        var creditResult = await creditDao.IssueCreditAsync(
                            request.CustomerPhone,
                            totalRefund,
                            returnId,
                            $"Refund for receipt {request.ReceiptNo}");

                        if (!creditResult.success)
                        {
                            Logger.Log($"Store credit issue failed for return {returnId}: {creditResult.message}");
                        }
                    }
                    catch (Exception creditEx)
                    {
                        Logger.Log($"Store credit issue threw for return {returnId}: {creditEx.Message}");
                    }
                }

                return new ReturnResult
                {
                    ReturnId = returnId,
                    Approved = true,
                    RefundAmount = totalRefund,
                    Message = request.RefundType == "StoreCredit"
                        ? $"Return processed. {totalRefund:C} issued as store credit to {request.CustomerPhone}."
                        : "Return processed successfully."
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return new ReturnResult
                {
                    Approved = false,
                    RefundAmount = 0,
                    Message = $"Return processing failed: {ex.Message}"
                };
            }
        }

        public async Task<decimal> GetTodayRefundTotal()
        {
            try
            {
                using var conn = DBConnection.GetConnection();
                const string query = @"
                    SELECT COALESCE(SUM(refund_amount), 0)
                    FROM returns
                    WHERE branch_id = @branchId AND DATE(created_at) = CURRENT_DATE";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);

                var result = await cmd.ExecuteScalarAsync();
                return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}