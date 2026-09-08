using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.logs;
using Pos_system.Models;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Services
{
    public class ProductDAO
    {
        public async Task<(bool Success, string message)> AddProduct(Product product)
        {
            Pos_system.Core.AuthorizationGuard.RequireManageInventory();

            int branchId = AppContext.CurrentBranchId;
            AppContext.EnsureBranchAccess(branchId);

            using var conn = DBConnection.GetConnection();
            const string query = @"INSERT INTO products (pName, sku, pCategory, price, sellingPrice, vatCategory, stock_status, stock_quantity, low_stock_threshold, BranchId)
                VALUES (@pName, @sku, @pCategory, @price, @sellingPrice, @vatCategory, @stock_status, @stock_quantity, @low_stock_threshold, @BranchId)";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pName", product.PName);
                cmd.Parameters.AddWithValue("@sku", product.SKU ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@pCategory", product.PCategory);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@sellingPrice", product.SellingPrice);
                cmd.Parameters.AddWithValue("@vatCategory", product.VatCategory);
                cmd.Parameters.AddWithValue("@stock_status", product.Stock_status ?? "In Stock");
                cmd.Parameters.AddWithValue("@stock_quantity", product.Stock_quantity);
                cmd.Parameters.AddWithValue("@low_stock_threshold", product.Low_stock_threshold > 0 ? product.Low_stock_threshold : 5);
                cmd.Parameters.AddWithValue("@BranchId", branchId);

                int rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    await AppServices.GetRequiredService<AuditService>()
                        .LogAsync("PRODUCT_CREATED", "product", product.PName);
                    return (true, "Product added successfully.");
                }
                return (false, "No product was added.");
            }
            catch (PostgresException ex)
            {
                Logger.Log($"Failed to add product: {product.PName} - {ex.Message}");
                return (false, "Error while adding product.");
            }
        }

        public bool DeleteProduct(int id, out string message)
        {
            Pos_system.Core.AuthorizationGuard.RequireManageInventory();

            using var conn = DBConnection.GetConnection();
            const string query = "DELETE FROM products WHERE idproducts = @id AND BranchId = @branchId";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    message = "Item deleted successfully";
                    return true;
                }
                message = "Product not found in current branch.";
                return false;
            }
            catch (PostgresException ex)
            {
                Logger.Log($"DeleteProduct failed for ID {id}: {ex.Message}");
                message = "Database error while deleting product.";
                return false;
            }
        }

        public bool UpdateProduct(Product product, out string message)
        {
            Pos_system.Core.AuthorizationGuard.RequireManageInventory();

            using var conn = DBConnection.GetConnection();
            const string query = @"UPDATE products SET pName = @pName, pCategory = @pCategory, price = @price,
                vatCategory = @vatCategory, stock_status = @stock_status, stock_quantity = @stock_quantity,
                low_stock_threshold = @low_stock_threshold
                WHERE idproducts = @id AND BranchId = @branchId";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pName", product.PName);
                cmd.Parameters.AddWithValue("@pCategory", product.PCategory);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@vatCategory", product.VatCategory);
                cmd.Parameters.AddWithValue("@stock_status", product.Stock_status ?? "In Stock");
                cmd.Parameters.AddWithValue("@stock_quantity", product.Stock_quantity);
                cmd.Parameters.AddWithValue("@low_stock_threshold", product.Low_stock_threshold > 0 ? product.Low_stock_threshold : 5);
                cmd.Parameters.AddWithValue("@id", product.Idproducts);
                cmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    message = "Product Updated.";
                    return true;
                }
                message = "Product not found in current branch.";
                return false;
            }
            catch (PostgresException ex)
            {
                Logger.Log("Update failed: " + ex.Message);
                message = "Database error while updating product.";
                return false;
            }
        }

        public async Task<List<Product>> GetAllProducts(int? branchId = null)
        {
            branchId ??= AppContext.CurrentBranchId;
            var products = new List<Product>();

            using var conn = DBConnection.GetConnection();
            const string query = @"SELECT idproducts, pName, sku, pCategory, price, sellingPrice, vatCategory, stock_status, stock_quantity, low_stock_threshold
                                   FROM products WHERE BranchId = @branchId ORDER BY pName";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@branchId", branchId);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    products.Add(new Product
                    {
                        Idproducts = Convert.ToInt32(reader["idproducts"]),
                        PName = reader["pName"]?.ToString() ?? string.Empty,
                        SKU = reader["sku"]?.ToString(),
                        PCategory = reader["pCategory"]?.ToString() ?? string.Empty,
                        Price = reader["price"] != DBNull.Value ? Convert.ToDecimal(reader["price"]) : 0m,
                        SellingPrice = reader["sellingPrice"] != DBNull.Value ? Convert.ToDecimal(reader["sellingPrice"]) : 0m,
                        VatCategory = reader["vatCategory"]?.ToString() ?? string.Empty,
                        Stock_status = reader["stock_status"] as string ?? string.Empty,
                        Stock_quantity = Convert.ToInt32(reader["stock_quantity"]),
                        Low_stock_threshold = reader["low_stock_threshold"] != DBNull.Value ? Convert.ToInt32(reader["low_stock_threshold"]) : 5
                    });
                }
            }
            catch (PostgresException ex)
            {
                Logger.Log("Products fetch failed: " + ex.Message);
            }
            return products;
        }

        public async Task<int> GetStockQuantityAsync(int productId, int branchId)
        {
            using var conn = DBConnection.GetConnection();
            const string query = "SELECT stock_quantity FROM products WHERE idproducts = @id AND BranchId = @branchId";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", productId);
            cmd.Parameters.AddWithValue("@branchId", branchId);
            var result = await cmd.ExecuteScalarAsync();
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        public async Task<List<Product>> GetLowStockProducts(int? branchId = null)
        {
            branchId ??= AppContext.CurrentBranchId;
            var products = new List<Product>();

            using var conn = DBConnection.GetConnection();
            const string query = @"SELECT idproducts, pName, sku, pCategory, price, sellingPrice, vatCategory, stock_status, stock_quantity, low_stock_threshold
                                   FROM products WHERE BranchId = @branchId AND stock_quantity <= low_stock_threshold
                                   ORDER BY stock_quantity ASC";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@branchId", branchId);
            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                products.Add(new Product
                {
                    Idproducts = Convert.ToInt32(reader["idproducts"]),
                    PName = reader["pName"]?.ToString() ?? string.Empty,
                    SKU = reader["sku"]?.ToString(),
                    PCategory = reader["pCategory"]?.ToString() ?? string.Empty,
                    Price = reader["price"] != DBNull.Value ? Convert.ToDecimal(reader["price"]) : 0m,
                    SellingPrice = reader["sellingPrice"] != DBNull.Value ? Convert.ToDecimal(reader["sellingPrice"]) : 0m,
                    VatCategory = reader["vatCategory"]?.ToString() ?? string.Empty,
                    Stock_status = reader["stock_status"] as string ?? string.Empty,
                    Stock_quantity = Convert.ToInt32(reader["stock_quantity"]),
                    Low_stock_threshold = Convert.ToInt32(reader["low_stock_threshold"])
                });
            }
            return products;
        }

        public bool CreateCategory(string categoryName, out string message)
        {
            using var conn = DBConnection.GetConnection();
            const string query = "INSERT INTO product_categories(name) VALUES (@name) ON CONFLICT (name) DO NOTHING";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", categoryName);
                cmd.ExecuteNonQuery();
                message = "Category created";
                return true;
            }
            catch (PostgresException e)
            {
                Logger.Log(e.Message);
                message = "Database error while creating category.";
                return false;
            }
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            var categories = new List<string>();
            await using var conn = DBConnection.GetConnection();
            const string query = "SELECT name FROM product_categories ORDER BY name";

            try
            {
                await using var cmd = new NpgsqlCommand(query, conn);
                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    var category = reader["name"].ToString();
                    if (!string.IsNullOrEmpty(category))
                        categories.Add(category);
                }
            }
            catch (PostgresException ex)
            {
                Logger.Log("GetCategories failed" + ex.Message);
            }
            return categories;
        }

        public int GetTotalStockQuantity(int? branchId = null)
        {
            branchId ??= AppContext.CurrentBranchId;

            using var conn = DBConnection.GetConnection();
            const string query = "SELECT COALESCE(SUM(stock_quantity), 0) FROM products WHERE BranchId = @branchId";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@branchId", branchId);
            var result = cmd.ExecuteScalar();
            return result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
        }

        public async Task<Dictionary<int, string>> GetVatCategoriesForProducts(List<int> productIds)
        {
            if (productIds.Count == 0)
                return new Dictionary<int, string>();

            using var conn = DBConnection.GetConnection();
            var parameters = new List<string>();
            var paramValues = new Dictionary<string, object>();

            for (int i = 0; i < productIds.Count; i++)
            {
                string paramName = $"@p{i}";
                parameters.Add(paramName);
                paramValues[paramName] = productIds[i];
            }

            string query = $"SELECT idproducts, vatCategory FROM products WHERE idproducts IN ({string.Join(",", parameters)}) AND BranchId = @branchId";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);
            foreach (var param in paramValues)
                cmd.Parameters.AddWithValue(param.Key, param.Value);

            using var reader = await cmd.ExecuteReaderAsync();
            var result = new Dictionary<int, string>();

            while (await reader.ReadAsync())
            {
                result.Add(
                    reader.GetInt32(reader.GetOrdinal("idproducts")),
                    reader.GetString(reader.GetOrdinal("vatCategory"))
                );
            }
            return result;
        }
    }
}
