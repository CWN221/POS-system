using Pos_system.Models;
using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.logs;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Services
{
    public class CreateSaleDAO
    {
        private readonly VatDAO _vatDAO = new();
        private readonly ProductDAO _productDAO = new();

        public async Task<string> CreateSale(Sale sale)
        {
            if (sale.Items == null || sale.Items.Count == 0)
                throw new Exception("Cannot save a sale with no items.");

            if (string.IsNullOrWhiteSpace(sale.PaymentMethod))
                throw new Exception("Please select a payment method.");

            var branchId = AppContext.CurrentBranchId;
            AppContext.EnsureBranchAccess(branchId);

            foreach (var item in sale.Items)
            {
                if (item.Price < 0)
                    throw new Exception($"{item.ProductName} has invalid price: {item.Price}");
                if (item.SellingPrice < 0)
                    throw new Exception($"{item.ProductName} has invalid selling price: {item.SellingPrice}");
                if (item.Quantity <= 0)
                    throw new Exception($"{item.ProductName} has invalid quantity: {item.Quantity}");

                var stock = await _productDAO.GetStockQuantityAsync(item.ProductId, branchId);
                if (stock < item.Quantity)
                    throw new Exception($"Insufficient stock for {item.ProductName}. Available: {stock}, requested: {item.Quantity}.");
            }

            var productIds = sale.Items.Select(i => i.ProductId).Distinct().ToList();
            var productVatMap = await _productDAO.GetVatCategoriesForProducts(productIds);
            var vatCache = new Dictionary<string, decimal>();

            foreach (var item in sale.Items)
            {
                if (!productVatMap.TryGetValue(item.ProductId, out var vatCat))
                    throw new Exception($"Could not determine VAT category for {item.ProductName}.");

                item.VatCategory = vatCat;

                if (!vatCache.ContainsKey(vatCat))
                    vatCache[vatCat] = await _vatDAO.GetVatPercentageAsync(vatCat);

                item.VatPercentage = vatCache[vatCat];
            }

            decimal serverComputedTotal = sale.Items.Sum(i => i.TotalPrice);
            decimal clientTotal = sale.TotalAmount;

            if (Math.Abs(serverComputedTotal - clientTotal) > 0.01m)
            {
                Logger.Log($"Sale total mismatch. Client: {clientTotal}, Server: {serverComputedTotal}");
                throw new Exception(
                    $"Sale total mismatch. Client: {clientTotal:N2}, Server: {serverComputedTotal:N2}. " +
                    $"The sale was not saved. Please recheck the cart.");
            }

            if (sale.SaleUuid == Guid.Empty)
                sale.SaleUuid = Guid.NewGuid();

            using var conn = DBConnection.GetConnection();
            await using var transaction = await conn.BeginTransactionAsync();

            try
            {
                int currentYear = sale.SaleDate.Year;

                const string seqQuery = @"
                                INSERT INTO receipt_sequences(year, last_sequence)
                                VALUES(@year, 1)
                                ON CONFLICT (year) DO UPDATE
                                SET last_sequence = receipt_sequences.last_sequence + 1
                                RETURNING last_sequence";

                int sequence;
                using (var seqCmd = new NpgsqlCommand(seqQuery, conn, transaction))
                {
                    seqCmd.Parameters.AddWithValue("@year", currentYear);
                    sequence = Convert.ToInt32(await seqCmd.ExecuteScalarAsync());
                }

                string receiptNumber = $"INV-{currentYear}-{sequence:D6}";
                sale.ReceiptNumber = receiptNumber;

                const string query = @"
                            INSERT INTO sales(sale_date, total_amount, BranchId, payment_method, cashier_id, amount_tendered, change_given, sale_uuid, receipt_number, store_credit_used, mpesa_status)
                            VALUES(@sale_date, @total_amount, @BranchId, @payment_method, @cashier_id, @amount_tendered, @change_given, @sale_uuid, @receipt_number, @store_credit_used, @mpesa_status)
                            RETURNING id";

                int saleId;
                using (var cmd = new NpgsqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@sale_date", sale.SaleDate);
                    cmd.Parameters.AddWithValue("@total_amount", serverComputedTotal);
                    cmd.Parameters.AddWithValue("@BranchId", branchId);
                    cmd.Parameters.AddWithValue("@payment_method", sale.PaymentMethod);
                    cmd.Parameters.AddWithValue("@cashier_id", AppContext.CurrentUserId);
                    cmd.Parameters.AddWithValue("@amount_tendered", sale.AmountTendered > 0 ? sale.AmountTendered : serverComputedTotal);
                    cmd.Parameters.AddWithValue("@change_given", sale.ChangeGiven);
                    cmd.Parameters.AddWithValue("@sale_uuid", sale.SaleUuid);
                    cmd.Parameters.AddWithValue("@receipt_number", receiptNumber);
                    cmd.Parameters.AddWithValue("@store_credit_used", sale.StoreCreditUsed);
                    cmd.Parameters.AddWithValue("@mpesa_status", (object?)sale.MpesaStatus ?? "None");
                    saleId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                sale.Id = saleId;

                if (sale.Payments.Count > 0)
                {
                    const string payQuery = @"
                                            INSERT INTO payments(sale_id, method, amount, reference, mpesa_checkout_request_id)
                                            VALUES(@sale_id, @method, @amount, @reference, @checkout)";

                    foreach (var payment in sale.Payments)
                    {
                        using var payCmd = new NpgsqlCommand(payQuery, conn, transaction);
                        payCmd.Parameters.AddWithValue("@sale_id", saleId);
                        payCmd.Parameters.AddWithValue("@method", payment.Method);
                        payCmd.Parameters.AddWithValue("@amount", payment.Amount);
                        payCmd.Parameters.AddWithValue("@reference", (object?)payment.Reference ?? DBNull.Value);
                        payCmd.Parameters.AddWithValue("@checkout", (object?)payment.MpesaCheckoutRequestId ?? DBNull.Value);
                        await payCmd.ExecuteNonQueryAsync();
                    }
                }
                else
                {
                    const string payQuery = @"
                                            INSERT INTO payments(sale_id, method, amount, reference, mpesa_checkout_request_id)
                                            VALUES(@sale_id, @method, @amount, NULL, NULL)";

                    using var payCmd = new NpgsqlCommand(payQuery, conn, transaction);
                    payCmd.Parameters.AddWithValue("@sale_id", saleId);
                    payCmd.Parameters.AddWithValue("@method", string.IsNullOrWhiteSpace(sale.PaymentMethod) ? "Cash" : sale.PaymentMethod);
                    payCmd.Parameters.AddWithValue("@amount", serverComputedTotal);
                    await payCmd.ExecuteNonQueryAsync();
                }

                foreach (var item in sale.Items)
                {
                    const string itemQuery = @"
                                            INSERT INTO sale_items(sale_id, product_id, product_name, unit_price, selling_price, profit, discount, quantity, vat_percentage, vat_amount, total_price)
                                            VALUES (@sale_id, @product_id, @product_name, @unit_price, @selling_price, @profit, @discount, @quantity, @vat_percentage, @vat_amount, @total_price)";

                    using var itemCmd = new NpgsqlCommand(itemQuery, conn, transaction);
                    itemCmd.Parameters.AddWithValue("@sale_id", saleId);
                    itemCmd.Parameters.AddWithValue("@product_id", item.ProductId);
                    itemCmd.Parameters.AddWithValue("@product_name", item.ProductName);
                    itemCmd.Parameters.AddWithValue("@selling_price", item.SellingPrice);
                    itemCmd.Parameters.AddWithValue("@unit_price", item.Price);
                    itemCmd.Parameters.AddWithValue("@profit", item.Profit);
                    itemCmd.Parameters.AddWithValue("@discount", item.Discount);
                    itemCmd.Parameters.AddWithValue("@quantity", item.Quantity);
                    itemCmd.Parameters.AddWithValue("@vat_percentage", item.VatPercentage);
                    itemCmd.Parameters.AddWithValue("@vat_amount", item.VatAmount);
                    itemCmd.Parameters.AddWithValue("@total_price", item.TotalPrice);
                    await itemCmd.ExecuteNonQueryAsync();

                    const string logQuery = @"
                                INSERT INTO stock_movements(product_id, type, quantity, reference_id, branch_id)
                                VALUES (@productId, 'SALE', @qty, @saleId, @branchId)";
                    using var logCmd = new NpgsqlCommand(logQuery, conn, transaction);
                    logCmd.Parameters.AddWithValue("@productId", item.ProductId);
                    logCmd.Parameters.AddWithValue("@qty", item.Quantity);
                    logCmd.Parameters.AddWithValue("@saleId", saleId);
                    logCmd.Parameters.AddWithValue("@branchId", branchId);
                    await logCmd.ExecuteNonQueryAsync();

                    const string updateStock = @"
                                            UPDATE products SET stock_quantity = stock_quantity - @qty
                                            WHERE idproducts = @productId AND BranchId = @branchId AND stock_quantity >= @qty";
                    using var stockCmd = new NpgsqlCommand(updateStock, conn, transaction);
                    stockCmd.Parameters.AddWithValue("@qty", item.Quantity);
                    stockCmd.Parameters.AddWithValue("@productId", item.ProductId);
                    stockCmd.Parameters.AddWithValue("@branchId", branchId);

                    if (await stockCmd.ExecuteNonQueryAsync() == 0)
                        throw new Exception($"Stock update failed for {item.ProductName}. Another sale may have consumed inventory.");
                }

                await transaction.CommitAsync();

                await AppServices.GetRequiredService<AuditService>().LogAsync("SALE_COMPLETED", "sale", saleId.ToString(),
                    $"Total: {serverComputedTotal:C}, Payment: {sale.PaymentMethod}, MpesaStatus: {sale.MpesaStatus}, UUID: {sale.SaleUuid}");

                return "Sale complete";
            }
            catch (PostgresException ex) when (ex.SqlState == "23505" && ex.ConstraintName == "idx_sales_uuid")
            {
                await transaction.RollbackAsync();
                Logger.Log($"Duplicate sale UUID attempted: {sale.SaleUuid}");
                throw new Exception("This sale has already been recorded. Duplicate submission blocked.");
            }
            catch (PostgresException ex)
            {
                await transaction.RollbackAsync();
                Logger.Log("Failed to save Sale: " + ex.Message);
                return "Failed to save Sale: " + ex.Message;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<decimal> GetTotalSales(int? branchId = null)
        {
            branchId ??= AppContext.CurrentBranchId;

            using var conn = DBConnection.GetConnection();
            const string query = "SELECT COALESCE(SUM(total_amount), 0) FROM sales WHERE BranchId = @BranchId AND is_voided = FALSE";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BranchId", branchId);
            var result = await cmd.ExecuteScalarAsync();
            return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        public async Task<decimal> GetTodaySales(int? branchId = null)
        {
            branchId ??= AppContext.CurrentBranchId;

            using var conn = DBConnection.GetConnection();
            const string query = @"SELECT COALESCE(SUM(total_amount), 0) FROM sales
                                   WHERE BranchId = @BranchId AND DATE(sale_date) = CURRENT_DATE AND is_voided = FALSE";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BranchId", branchId);
            var result = await cmd.ExecuteScalarAsync();
            return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }

        public async Task<int> GetTodayTransactionCount(int? branchId = null)
        {
            branchId ??= AppContext.CurrentBranchId;

            using var conn = DBConnection.GetConnection();
            const string query = @"SELECT COUNT(*) FROM sales
                                   WHERE BranchId = @BranchId AND DATE(sale_date) = CURRENT_DATE AND is_voided = FALSE";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BranchId", branchId);
            return Convert.ToInt32(await cmd.ExecuteScalarAsync());
        }

        public async Task<(bool success, string message)> VoidSaleAsync(int saleId, int approvedBy, string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
                return (false, "A reason is required to void a sale.");

            var branchId = AppContext.CurrentBranchId;

            using var conn = DBConnection.GetConnection();
            await using var transaction = await conn.BeginTransactionAsync();

            try
            {
                bool alreadyVoided;
                DateTime saleDate;

                const string checkQuery = @"
                                    SELECT is_voided, sale_date FROM sales
                                    WHERE id = @id AND BranchId = @branchId FOR UPDATE";

                using (var cmd = new NpgsqlCommand(checkQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", saleId);
                    cmd.Parameters.AddWithValue("@branchId", branchId);

                    using var reader = await cmd.ExecuteReaderAsync();
                    if (!await reader.ReadAsync())
                    {
                        await transaction.RollbackAsync();
                        return (false, "Sale not found in this branch.");
                    }

                    alreadyVoided = reader.GetBoolean(reader.GetOrdinal("is_voided"));
                    saleDate = reader.GetDateTime(reader.GetOrdinal("sale_date"));
                }

                if (alreadyVoided)
                {
                    await transaction.RollbackAsync();
                    return (false, "This sale has already been voided.");
                }

                if (saleDate.Date != DateTime.Today)
                {
                    await transaction.RollbackAsync();
                    return (false, "Only sales completed today can be voided. Use a return for older sales.");
                }

                const string returnCheck = "SELECT COUNT(*) FROM returns WHERE sale_id = @id";
                using (var cmd = new NpgsqlCommand(returnCheck, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", saleId);
                    var count = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    if (count > 0)
                    {
                        await transaction.RollbackAsync();
                        return (false, "Cannot void a sale that has returns processed against it.");
                    }
                }

                const string itemsQuery = "SELECT product_id, quantity FROM sale_items WHERE sale_id = @id";
                var itemsToRestore = new List<(int ProductId, int Quantity)>();

                using (var cmd = new NpgsqlCommand(itemsQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", saleId);
                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        itemsToRestore.Add((
                            reader.GetInt32(reader.GetOrdinal("product_id")),
                            reader.GetInt32(reader.GetOrdinal("quantity"))
                        ));
                    }
                }

                const string voidQuery = @"
                                        UPDATE sales SET
                                            is_voided = TRUE,
                                            voided_by = @voidedBy,
                                            voided_at = CURRENT_TIMESTAMP,
                                            void_reason = @reason
                                        WHERE id = @id";

                using (var cmd = new NpgsqlCommand(voidQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", saleId);
                    cmd.Parameters.AddWithValue("@voidedBy", approvedBy);
                    cmd.Parameters.AddWithValue("@reason", reason.Trim());
                    await cmd.ExecuteNonQueryAsync();
                }

                foreach (var (productId, quantity) in itemsToRestore)
                {
                    const string stockQuery = @"
                                            UPDATE products SET stock_quantity = stock_quantity + @qty
                                            WHERE idproducts = @productId AND BranchId = @branchId";

                    using (var cmd = new NpgsqlCommand(stockQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@qty", quantity);
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.Parameters.AddWithValue("@branchId", branchId);
                        await cmd.ExecuteNonQueryAsync();
                    }

                    const string movementQuery = @"
                                            INSERT INTO stock_movements(product_id, type, quantity, reference_id, branch_id, reason, user_id)
                                            VALUES(@productId, 'VOID', @qty, @saleId, @branchId, @reason, @userId)";

                    using (var cmd = new NpgsqlCommand(movementQuery, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.Parameters.AddWithValue("@qty", quantity);
                        cmd.Parameters.AddWithValue("@saleId", saleId);
                        cmd.Parameters.AddWithValue("@branchId", branchId);
                        cmd.Parameters.AddWithValue("@reason", $"Void of sale {saleId}: {reason.Trim()}");
                        cmd.Parameters.AddWithValue("@userId", approvedBy);
                        await cmd.ExecuteNonQueryAsync();
                    }
                }

                await transaction.CommitAsync();

                await AppServices.GetRequiredService<AuditService>().LogAsync("SALE_VOIDED", "sale", saleId.ToString(), $"Approved by user {approvedBy}. Reason: {reason}");

                return (true, $"Sale #{saleId} voided successfully. Stock has been restored.");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return (false, $"Void failed: {ex.Message}");
            }
        }
    }
}