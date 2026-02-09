using MySql.Data.MySqlClient;
using Pos_system.Database;
using Pos_system.Models;
using Pos_system.logs;

namespace Pos_system.Services
{
    public class VatDAO
    {

        /*
         * Method to set the VAT in the DB
         */
        public bool SetVat(VatSettings vatSettings, out string message)
        {
            using var conn = DBConnection.GetConnection();
            const string query = "UPDATE vat_settings SET vat_percentage = @vat_percentage WHERE id = 1";

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@vat_percentage", vatSettings.VatPercentage);

                cmd.ExecuteNonQuery();

                message = "VAT Updated.";
                return true;
            }
            catch (MySqlException ex)
            {
                Logger.Log("SetVat failed: " + ex.Message);
                message = "VAT update failed!";
                return false;
            }            
        }

        
        /*
         * 
         * Method to fetch VAT categories
         * 
         */
        public async Task<List<string>> GetVatCategoriesAsync()
        {
            var Vatcategories = new List<string>();
            await using var conn = DBConnection.GetConnection();
            const string query = "SELECT name FROM vat_settings";

            try
            {
                await using var cmd = new MySqlCommand(query, conn);
                await using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync()) 
                {
                    string category = reader["name"].ToString();
                    Vatcategories.Add(category);
                }
            }
            catch (Exception ex)
            {
                Logger.Log("VAT Category fetch failed: " + ex.Message);
                throw new Exception("Failed to fetch VAT categories!");
            }            
            return Vatcategories;
        }



        /*
         * 
         * Method to fetch VAT Percentage(%)
         * 
         */
        public async Task<decimal> GetVatPercentageAsync(string vatCategory)
        {
            await using var conn = DBConnection.GetConnection();
            const string query = "SELECT vat_percentage FROM vat_settings WHERE name = @name";

            try
            {
                await using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", vatCategory);
                var result = await cmd.ExecuteScalarAsync();

                if (result != null)
                {
                    return Convert.ToDecimal(result);
                }
                else
                {
                    throw new Exception($"VAT percentage not found for category: {vatCategory}");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("VAT percentage fetch failed: " + ex.Message);
                throw new Exception("Failed to fetch VAT Percentage");
            }            
        }

    }
}
