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
            const string query = @"INSERT INTO products (pname, sku, pcategory, price, sellingprice, vatcategory, stock_status, stock_quantity, low_stock_threshold, branchid)
                VALUES (@pname, @sku, @pcategory, @price, @sellingprice, @vatcategory, @stock_status, @stock_quantity, @low_stock_threshold, @branchid)";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pname", product.PName);
                cmd.Parameters.AddWithValue("@sku", product.SKU ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@pcategory", product.PCategory);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@sellingprice", product.SellingPrice);
                cmd.Parameters.AddWithValue("@vatcategory", product.VatCategory);
                cmd.Parameters.AddWithValue("@stock_status", product.Stock_status ?? "In Stock");
                cmd.Parameters.AddWithValue("@stock_quantity", product.Stock_quantity);
                cmd.Parameters.AddWithValue("@low_stock_threshold", product.Low_stock_threshold > 0 ? product.Low_stock_threshold : 5);
                cmd.Parameters.AddWithValue("@branchid", branchId);

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
                return (false, $"Error while adding product: {ex.Message}");
            }
        }

        public bool DeleteProduct(int id, out string message)
        {
            AuthorizationGuard.RequireManageInventory();

            using var conn = DBConnection.GetConnection();

            try
            {
                const string checkQuery = @"
                                            SELECT
                                                (SELECT COUNT(*) FROM sale_items WHERE product_id = @id) +
                                                (SELECT COUNT(*) FROM return_items WHERE product_id = @id) +
                                                (SELECT COUNT(*) FROM purchase_order_items WHERE product_id = @id) AS total_refs";

                int references;
                using (var checkCmd = new NpgsqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@id", id);
                    references = Convert.ToInt32(checkCmd.ExecuteScalar());
                }

                if (references > 0)
                {
                    const string softQuery = @"UPDATE products SET is_active = FALSE
                                       WHERE idproducts = @id AND BranchId = @branchId";

                    using var softCmd = new NpgsqlCommand(softQuery, conn);
                    softCmd.Parameters.AddWithValue("@id", id);
                    softCmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);

                    if (softCmd.ExecuteNonQuery() > 0)
                    {
                        message = $"This product has {references} historical reference(s) (sales, returns or purchase orders). " +
                                  $"It was deactivated instead of deleted so history is preserved.";
                        return true;
                    }

                    message = "Product not found in current branch.";
                    return false;
                }

                const string deleteQuery = "DELETE FROM products WHERE idproducts = @id AND BranchId = @branchId";

                using var deleteCmd = new NpgsqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@id", id);
                deleteCmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);

                if (deleteCmd.ExecuteNonQuery() > 0)
                {
                    message = "Product deleted permanently.";
                    return true;
                }

                message = "Product not found in current branch.";
                return false;
            }
            catch (PostgresException ex)
            {
                Logger.Log($"DeleteProduct failed for ID {id}: {ex.Message}");

                if (ex.SqlState == "23503")
                    message = "This product cannot be deleted because it is referenced by existing records. Try deactivating it instead.";
                else
                    message = "Database error while deleting product.";

                return false;
            }
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            using var conn = DBConnection.GetConnection();
            const string query = @"SELECT idproducts, pName, sku, pCategory, price, sellingPrice, vatCategory, stock_status, stock_quantity, low_stock_threshold
                                   FROM products WHERE idproducts = @id AND BranchId = @branchId";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);

            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync())
                return null;

            return new Product
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
            };
        }

        public bool UpdateProduct(Product product, out string message)
        {
            Pos_system.Core.AuthorizationGuard.RequireManageInventory();

            using var conn = DBConnection.GetConnection();
            const string query = @"UPDATE products SET pName = @pName, sku = @sku, pCategory = @pCategory, price = @price,
                sellingPrice = @sellingPrice, vatCategory = @vatCategory, stock_status = @stock_status,
                low_stock_threshold = @low_stock_threshold
                WHERE idproducts = @id AND BranchId = @branchId";

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
                //cmd.Parameters.AddWithValue("@stock_quantity", product.Stock_quantity);
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
            const string query = @"SELECT idproducts, pName, sku, pCategory, price, sellingPrice, vatCategory, stock_status, stock_quantity, low_stock_threshold, is_active
                                   FROM products WHERE BranchId = @branchId AND is_active = TRUE ORDER BY pName";

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
                        Low_stock_threshold = reader["low_stock_threshold"] != DBNull.Value ? Convert.ToInt32(reader["low_stock_threshold"]) : 5,
                        IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
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
            const string query = "SELECT stock_quantity FROM products WHERE idproducts = @id AND BranchId = @branchId AND is_active = TRUE";

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
                                   FROM products WHERE BranchId = @branchId AND is_active = TRUE AND stock_quantity <= low_stock_threshold
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
            AuthorizationGuard.RequireManageInventory();

            using var conn = DBConnection.GetConnection();
            const string query = "INSERT INTO product_categories(name) VALUES (@name) ON CONFLICT (name) DO NOTHING RETURNING id";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", categoryName.Trim());
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    message = "Category created successfully.";
                    return true;
                }

                message = "That category already exists.";
                return false;
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
            const string query = "SELECT COALESCE(SUM(stock_quantity), 0) FROM products WHERE BranchId = @branchId AND is_active = TRUE";

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

        public bool AdjustStock(int productId, int delta, string reason, int branchId, out string message)
        {
            message = "";
            if (delta == 0)
            {
                message = "No change requested.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(reason))
            {
                message = "A reason is required for stock adjustments.";
                return false;
            }

            try
            {
                using var conn = DBConnection.GetConnection();
                using var transaction = conn.BeginTransaction();

                int currentQty;
                const string qtyQuery = @"SELECT stock_quantity FROM products
                                  WHERE idproducts = @id AND BranchId = @branchId FOR UPDATE";
                using (var cmd = new NpgsqlCommand(qtyQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    cmd.Parameters.AddWithValue("@branchId", branchId);

                    var obj = cmd.ExecuteScalar();
                    if (obj == null || obj == DBNull.Value)
                    {
                        transaction.Rollback();
                        message = "Product not found in this branch.";
                        return false;
                    }

                    currentQty = Convert.ToInt32(obj);
                }

                int newQty = currentQty + delta;
                if (newQty < 0)
                {
                    transaction.Rollback();
                    message = $"Adjustment would result in negative stock ({newQty}). Current: {currentQty}, requested delta: {delta}.";
                    return false;
                }

                const string updateQuery = @"UPDATE products SET stock_quantity = @qty
                                    WHERE idproducts = @id AND branchid = @branchId AND is_active = true";
                using (var cmd = new NpgsqlCommand(updateQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@qty", newQty);
                    cmd.Parameters.AddWithValue("@id", productId);
                    cmd.Parameters.AddWithValue("@branchId", branchId);
                    cmd.ExecuteNonQuery();
                }

                const string movementQuery = @"INSERT INTO stock_movements(product_id, type, quantity, reason, user_id, reference_id, branch_id)
                                       VALUES(@product_id, 'ADJUSTMENT', @qty, @reason, @user_id, NULL, @branch_id)";
                using (var cmd = new NpgsqlCommand(movementQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@product_id", productId);
                    cmd.Parameters.AddWithValue("@qty", Math.Abs(delta));
                    cmd.Parameters.AddWithValue("@reason", reason);
                    cmd.Parameters.AddWithValue("@user_id", AppContext.CurrentUserId);
                    cmd.Parameters.AddWithValue("@branch_id", branchId);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                message = $"Stock adjusted: {currentQty} → {newQty}.";
                return true;
            }
            catch (Exception ex)
            {
                message = $"Stock adjustment failed: {ex.Message}";
                return false;
            }
        }

        public bool SoftDeleteProduct(int productId, out string message)
        {
            AuthorizationGuard.RequireManageInventory();

            using var conn = DBConnection.GetConnection();
            const string query = @"UPDATE products SET is_active = FALSE
                           WHERE idproducts = @id AND BranchId = @branchId";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", productId);
                cmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);

                if (cmd.ExecuteNonQuery() > 0)
                {
                    message = "Product deactivated.";
                    return true;
                }

                message = "Product not found in current branch.";
                return false;
            }
            catch (PostgresException ex)
            {
                Logger.Log($"SoftDeleteProduct failed for ID {productId}: {ex.Message}");
                message = "Database error while deactivating product.";
                return false;
            }
        }

        public async Task<List<Product>> GetAllProductsIncludingInactive(int? branchId = null)
        {
            branchId ??= AppContext.CurrentBranchId;
            var products = new List<Product>();

            using var conn = DBConnection.GetConnection();
            const string query = @"SELECT idproducts, pName, sku, pCategory, price, sellingPrice, vatCategory, stock_status, stock_quantity, low_stock_threshold, is_active
                           FROM products WHERE BranchId = @branchId ORDER BY is_active DESC, pName";

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
                    Low_stock_threshold = reader["low_stock_threshold"] != DBNull.Value ? Convert.ToInt32(reader["low_stock_threshold"]) : 5,
                    IsActive = reader["is_active"] != DBNull.Value && Convert.ToBoolean(reader["is_active"])
                });
            }
            return products;
        }

        public async Task<List<string>> GetActiveCategoriesAsync()
        {
            var categories = new List<string>();
            using var conn = DBConnection.GetConnection();
            const string sql = @"SELECT DISTINCT pCategory FROM products 
                        WHERE BranchId = @branchId AND is_active = TRUE AND stock_quantity > 0
                        ORDER BY pCategory";

            using var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);
            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                var c = reader["pCategory"]?.ToString();
                if (!string.IsNullOrEmpty(c)) categories.Add(c);
            }
            return categories;
        }
    }
}