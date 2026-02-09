/*
 * 
 * ProductDAO (Data Access Objects)
 * This file handles CRUD functions for products in the DB
 * 
*/ 

using MySql.Data.MySqlClient;
using Pos_system.Database;
using Pos_system.logs;
using Pos_system.Models;
using System.Collections.Generic;

namespace Pos_system.Services
{
    public class ProductDAO
    {


        /*
         * 
         * This method is used to add a product to the DB
         * 
         */
        public bool AddProduct(Product product, out string message)
        {
            using var conn = DBConnection.GetConnection();
            string query = "INSERT INTO products (pName, pCategory, price, vatCategory, stock_status, stock_quantity)"
                + "VALUES (@pName, @pCategory, @price, @vatCategory, @stock_status, @stock_quantity)";

            if (conn == null)
            {
                message = "Database connection failed!";
                return false;
            }

            try
            {
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pName", product.PName);
                cmd.Parameters.AddWithValue("@pCategory", product.PCategory);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@vatCategory", product.VatCategory);
                cmd.Parameters.AddWithValue("@stock_status", product.Stock_status);
                cmd.Parameters.AddWithValue("@stock_quantity", product.Stock_quantity);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                {
                    message = "Product added successfully."; return true;
                }
                else { 
                    message = "No Product was added."; return false;
                }
            }
            catch (MySqlException ex)
            {
                Logger.Log($"Failed to add product: {product.PName}" + ex.Message);
                message = "Error while adding product.";
                return false;
            }            
        }



        /*
         * 
         * Method to delete a product from the DB
         *  
         */
        public bool DeleteProduct(int id, out string message)
        {
            using var conn = DBConnection.GetConnection();
            string query = "DELETE FROM products WHERE idproducts = @id";

            if (conn == null)
            {
                message = "Database connection failed!";
                return false;
            }

            try
            {
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                cmd.ExecuteNonQuery();
                message =  "Item deleted successfully";
                return true;
            }
            catch (MySqlException ex)
            {
                Logger.Log($"DeleteProduct failed for ID {id}" + id);
                Logger.Log(ex);

                message = "Database error while deleting product.";
                return false;
            }            
        }



        /*
         * 
         * Method to update products in the DB
         * 
         */ 
        public bool UpdateProduct(Product product, out string message)
        {
            using var conn = DBConnection.GetConnection();
            string query = "UPDATE products SET pName = @pName, pCategory = @pCategory, price = @price, " +
                "vatCategory = @vatCategory, stock_status = @stock_status, stock_quantity = @stock_quantity WHERE idproducts = @id";

            if (conn == null)
            {
                message = "Database connection failed!";
                return false;
            }

            try
            {
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@pName", product.PName);
                cmd.Parameters.AddWithValue("@pCategory", product.PCategory);
                cmd.Parameters.AddWithValue("@price", product.Price);
                cmd.Parameters.AddWithValue("@vatCategory", product.VatCategory);
                cmd.Parameters.AddWithValue("@stock_status", product.Stock_status);
                cmd.Parameters.AddWithValue("@stock_quantity", product.Stock_quantity);
                cmd.Parameters.AddWithValue("@id", product.Idproducts);

                cmd.ExecuteNonQuery();
                message =  "Product Updated.";
                return true;
            }
            catch (MySqlException ex)
            {
                Logger.Log("Update failed: " + ex.Message);
                message = "Database error while updating product.";
                return false;
            }            
        }



        /*
         * 
         * Method to get and display all products
         * 
         */
        public async Task<List<Product>> GetAllProducts()
        {
            var products = new List<Product>();
            using var conn = DBConnection.GetConnection();
            string query = "SELECT idproducts, pName, pCategory, price, vatCategory, stock_status, stock_quantity FROM products";

            try
            {
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var product = new Product
                    {
                        Idproducts = Convert.ToInt32(reader["idproducts"]),
                        PName = reader["pName"]?.ToString() ?? string.Empty,
                        PCategory = reader["pCategory"]?.ToString() ?? string.Empty,
                        Price = Convert.ToDecimal(reader["price"]),
                        VatCategory = reader["vatCategory"]?.ToString() ?? string.Empty,
                        Stock_status = reader["stock_status"] as string ?? string.Empty,
                        Stock_quantity = Convert.ToInt32(reader["stock_quantity"])
                    };
                    products.Add(product);
                }

            }
            catch (MySqlException ex)
            {
                Logger.Log("Products fetch failed: " + ex.Message);
            }
            return products;
        }


        /*
         * 
         * Method to create product category 
         * 
         */
        public bool CreateCategory(string categoryName, out string message)
        {
            using var conn = DBConnection.GetConnection();
            string query = "INSERT INTO product_categories(name) VALUE (@name)";

            if (conn == null)
            {
                message = "Database connection failed!";
                return false;
            }

            try
            {
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", categoryName);

                cmd.ExecuteNonQuery();
                message =  "Category created";
                return true;
            }
            catch (MySqlException e)
            {
                Logger.Log(e.Message);
                message = "Database error while creating category.";
                return false;
            }            

        }



        /*
         * 
         * Method to fetch product categories
         * 
         */
        public async Task<List<string>> GetCategoriesAsync()
        {
            var categories = new List<string>();
            await using var conn = DBConnection.GetConnection();
            var query = "SELECT name FROM product_categories";

            try
            {
                await using var cmd = new MySqlCommand(query, conn);
                await using var reader = await cmd.ExecuteReaderAsync();

                while(await reader.ReadAsync())
                {
                    var category = reader["name"].ToString();
                    categories.Add(category);
                }
                reader.Close();
            }
            catch (MySqlException ex)
            {
                Logger.Log("GetCategories failed" + ex.Message);
                return new List<string>();

            }            
            return categories;
        }



        /*
         * 
         * This method fetches the total stock quantity from the DB
         * 
         */
        public int GetTotalStockQuantity()
        {
            using var conn = DBConnection.GetConnection ();
            string query = "SELECT SUM(stock_quantity) FROM products";
            var totalItems = 0;

            try
            {
                using MySqlCommand cmd = new MySqlCommand(query, conn);
                var result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    totalItems = Convert.ToInt32(result);
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Failed to fetch total stock quantity", ex);
            }            
            return totalItems;
        }

        
    }
}
