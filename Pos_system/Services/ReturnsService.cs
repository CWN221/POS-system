using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.Models;
using Pos_system.Core;
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

            decimal totalRefund = 0;

            using var conn = DBConnection.GetConnection();
            await using var transaction = await conn.BeginTransactionAsync();

            try
            {
                // Verify sale belongs to branch
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
                    INSERT INTO returns(receipt_no, sale_id, reason, refund_type, refund_amount, requested_by, branch_id)
                    VALUES(@receipt_no, @sale_id, @reason, @refund_type, 0, @requested_by, @branch_id)
                    RETURNING id";

                int returnId;
                using (var cmd = new NpgsqlCommand(returnString, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@receipt_no", request.ReceiptNo);
                    cmd.Parameters.AddWithValue("@sale_id", request.SaleId);
                    cmd.Parameters.AddWithValue("@reason", request.Reason);
                    cmd.Parameters.AddWithValue("@refund_type", request.RefundType);
                    cmd.Parameters.AddWithValue("@requested_by", request.RequestedBy);
                    cmd.Parameters.AddWithValue("@branch_id", request.BranchId);
                    returnId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                foreach (var item in request.Items)
                {
                    // Validate return qty against original sale qty
                    const string qtyCheck = "SELECT quantity FROM sale_items WHERE id = @id AND sale_id = @saleId";
                    using (var qtyCmd = new NpgsqlCommand(qtyCheck, conn, transaction))
                    {
                        qtyCmd.Parameters.AddWithValue("@id", item.SaleItemId);
                        qtyCmd.Parameters.AddWithValue("@saleId", request.SaleId);
                        var soldQty = Convert.ToInt32(await qtyCmd.ExecuteScalarAsync());
                        if (item.QtyToReturn <= 0 || item.QtyToReturn > soldQty)
                            throw new InvalidOperationException($"Invalid return quantity for item {item.SaleItemId}.");
                    }

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

                await AppServices.GetRequiredService<AuditService>()
                    .LogAsync("RETURN_PROCESSED", "return", returnId.ToString(), $"Refund: {totalRefund:C}");

                return new ReturnResult
                {
                    ReturnId = returnId,
                    Approved = true,
                    RefundAmount = totalRefund,
                    Message = "Return processed successfully."
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
    }
}
