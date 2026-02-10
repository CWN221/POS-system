/*
 * 
 * This class creates a sale and stores it in the DB
 * 
 */

using Pos_system.Models;
using MySql.Data.MySqlClient;
using Pos_system.Database;
using Pos_system.logs;
using Pos_system.Views.Pages;

namespace Pos_system.Services
{
    public class CreateSaleDAO
    {

        private int saleId;

        /*
         * 
         * This method creates a sale into the DB
         * 
         */
        public async Task<string> CreateSale(Sale sale)
        {

            // Validate sales
            if (sale.Items == null || sale.Items.Count == 0)
            {
                throw new Exception("Cannot save a sale with no items.");
            }

            // Validate each items first
            foreach(var item in sale.Items)
            {
                if (item.Price < 0)
                {
                    throw new Exception($"{item.ProductName} has invalid price: {item.Price}");
                }
                if (item.Quantity < 0)
                {
                    throw new Exception($"{item.ProductName} has invalid quantity: {item.Quantity}");
                }
                // Add stock quantity vs item.Quantity check
            }

            using var conn = DBConnection.GetConnection();
            const string query = "INSERT INTO sales(sale_date, total_amount) VALUES(@sale_date, @total_amount)";

            if (conn == null)
            {
                return "Database could not be reached!";
            }

            try
            {
                await conn.OpenAsync();
                await using var transaction = conn.BeginTransaction();

                using (var cmd = new MySqlCommand(query, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@sale_date", sale.SaleDate);
                    cmd.Parameters.AddWithValue("@total_amount", sale.TotalAmount);
                    await cmd.ExecuteNonQueryAsync();
                    saleId = (int)cmd.LastInsertedId;
                }
                                   

                foreach (var item in sale.Items)
                {
                    const string ItemQuery = "INSERT INTO sale_items(sale_id, product_id, product_name, unit_price, selling_price, discount, quantity, vat_amount, total_price) " +
                        "VALUES (@sale_id, @product_id, @product_name, @unit_price, @selling_price, @discount, @quantity, @vat_amount, @total_price)";

                    using var itemCmd = new MySqlCommand(ItemQuery, conn, transaction);
                    itemCmd.Parameters.AddWithValue("@sale_id", saleId);
                    itemCmd.Parameters.AddWithValue("@product_id", item.ProductId);
                    itemCmd.Parameters.AddWithValue("@product_name", item.ProductName);
                    itemCmd.Parameters.AddWithValue("@selling_price", item.SellingPrice);
                    itemCmd.Parameters.AddWithValue("@unit_price", item.Price);
                    itemCmd.Parameters.AddWithValue("@discount", item.Discount);
                    itemCmd.Parameters.AddWithValue("@quantity", item.Quantity);
                    itemCmd.Parameters.AddWithValue("@vat_amount", item.VatAmount);
                    itemCmd.Parameters.AddWithValue("@total_price", item.TotalPrice);

                    await itemCmd.ExecuteNonQueryAsync();
                }
                await transaction.CommitAsync();
                return "Sale complete";
            } 
            catch (MySqlException ex)
            {
                Logger.Log("Failed to save Sale: " + ex.Message);
                return "Failed to save Sale: " + ex.Message;
            }             
        }


        /*
         * 
         * This method fetches total amount of sales made from the DB
         * 
         */
        public async Task<decimal> GetTotalSales()
        {
            using var conn = DBConnection.GetConnection();
            string query = "SELECT IFNULL(SUM(total_amount), 0) FROM sales";
            decimal totalSales = 0;

            if (conn == null)
            {
                Logger.Log("Total sales fetch failed. Database connection = NULL ");
                return totalSales;
            }

            try
            {     
                using var cmd = new MySqlCommand(query, conn);
                var result = await cmd.ExecuteScalarAsync();
                if (result != null)
                {
                    totalSales = Convert.ToDecimal(result);
                }
            }
            catch (MySqlException ex)
            {
                Logger.Log("Total Sales fetch failed: " + ex.Message);
                throw new Exception("Failed to fetch total sales: " + ex.Message);                
            }            
            return totalSales;
        }
        
    }
}
