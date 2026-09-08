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

            if (sale.TotalAmount <= 0)
                throw new Exception("Invalid sale total");

            if (string.IsNullOrWhiteSpace(sale.PaymentMethod))
                throw new Exception("Please select a payment method.");

            var branchId = AppContext.CurrentBranchId;
            AppContext.EnsureBranchAccess(branchId);

            foreach (var item in sale.Items)
            {
                if (item.Price < 0)
                    throw new Exception($"{item.ProductName} has invalid price: {item.Price}");
                if (item.Quantity <= 0)
                    throw new Exception($"{item.ProductName} has invalid quantity: {item.Quantity}");

                var stock = await _productDAO.GetStockQuantityAsync(item.ProductId, branchId);
                if (stock < item.Quantity)
                    throw new Exception($"Insufficient stock for {item.ProductName}. Available: {stock}, requested: {item.Quantity}.");
            }

            using var conn = DBConnection.GetConnection();
            const string query = @"INSERT INTO sales(sale_date, total_amount, BranchId, payment_method, cashier_id)
                                   VALUES(@sale_date, @total_amount, @BranchId, @payment_method, @cashier_id) RETURNING id";

            try
            {
                await using var transaction = await conn.BeginTransactionAsync();
                int saleId;

                using (var cmd = new NpgsqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@sale_date", sale.SaleDate);
                    cmd.Parameters.AddWithValue("@total_amount", sale.TotalAmount);
                    cmd.Parameters.AddWithValue("@BranchId", branchId);
                    cmd.Parameters.AddWithValue("@payment_method", sale.PaymentMethod);
                    cmd.Parameters.AddWithValue("@cashier_id", AppContext.CurrentUserId);
                    saleId = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }

                sale.Id = saleId;
                var productIds = sale.Items.Select(i => i.ProductId).Distinct().ToList();
                var productVatMap = await _productDAO.GetVatCategoriesForProducts(productIds);
                var vatCache = new Dictionary<string, decimal>();

                foreach (var item in sale.Items)
                {
                    item.VatCategory = productVatMap[item.ProductId];
                    if (!vatCache.ContainsKey(item.VatCategory))
                        vatCache[item.VatCategory] = await _vatDAO.GetVatPercentageAsync(item.VatCategory);
                    item.VatPercentage = vatCache[item.VatCategory];
                }

                foreach (var item in sale.Items)
                {
                    const string itemQuery = @"INSERT INTO sale_items(sale_id, product_id, product_name, unit_price, selling_price, profit, discount, quantity, vat_percentage, vat_amount, total_price)
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

                    const string logQuery = @"INSERT INTO stock_movements(product_id, type, quantity, reference_id, branch_id)
                                              VALUES (@productId, 'SALE', @qty, @saleId, @branchId)";
                    using var logCmd = new NpgsqlCommand(logQuery, conn, transaction);
                    logCmd.Parameters.AddWithValue("@productId", item.ProductId);
                    logCmd.Parameters.AddWithValue("@qty", item.Quantity);
                    logCmd.Parameters.AddWithValue("@saleId", saleId);
                    logCmd.Parameters.AddWithValue("@branchId", branchId);
                    await logCmd.ExecuteNonQueryAsync();

                    const string updateStock = @"UPDATE products SET stock_quantity = stock_quantity - @qty
                                                 WHERE idproducts = @productId AND BranchId = @branchId AND stock_quantity >= @qty";
                    using var stockCmd = new NpgsqlCommand(updateStock, conn, transaction);
                    stockCmd.Parameters.AddWithValue("@qty", item.Quantity);
                    stockCmd.Parameters.AddWithValue("@productId", item.ProductId);
                    stockCmd.Parameters.AddWithValue("@branchId", branchId);

                    if (await stockCmd.ExecuteNonQueryAsync() == 0)
                        throw new Exception($"Stock update failed for {item.ProductName}. Another sale may have consumed inventory.");
                }

                await transaction.CommitAsync();

                await AppServices.GetRequiredService<AuditService>()
                    .LogAsync("SALE_COMPLETED", "sale", saleId.ToString(), $"Total: {sale.TotalAmount:C}, Payment: {sale.PaymentMethod}");

                return "Sale complete";
            }
            catch (PostgresException ex)
            {
                Logger.Log("Failed to save Sale: " + ex.Message);
                return "Failed to save Sale: " + ex.Message;
            }
        }

        public async Task<decimal> GetTotalSales(int? branchId = null)
        {
            branchId ??= AppContext.CurrentBranchId;

            using var conn = DBConnection.GetConnection();
            const string query = "SELECT COALESCE(SUM(total_amount), 0) FROM sales WHERE BranchId = @BranchId";

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
                                   WHERE BranchId = @BranchId AND DATE(sale_date) = CURRENT_DATE";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BranchId", branchId);
            var result = await cmd.ExecuteScalarAsync();
            return result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
        }
    }
}
