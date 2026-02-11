/**
 * StoreSettingsService.cs
 * 
 * This file is responsible for managing the store settings in the POS System.
 * It porvides methods to edit, save & retrieve store settings e.g. store name, address, contact info.
 * 
 */


using MySql.Data.MySqlClient;
using Pos_system.Models;
using Pos_system.Database;
using Pos_system.logs;

namespace Pos_system.Services
{
    public class StoreSettingsService
    {

        // Method to save store info
        public async Task<bool> SaveStoreSettings(StoreSettings settings)
        {
            using var conn = DBConnection.GetConnection();

            if (conn == null)
                throw new InvalidOperationException("Database connection failed.");

            try
            {
                string checkQuery = "SELECT COUNT(*) FROM StoreSettings WHERE Id = @Id";
                using var checkcmd = new MySqlCommand(checkQuery, conn);
                checkcmd.Parameters.AddWithValue("@Id", settings.Id);

                var exists = Convert.ToInt32(await checkcmd.ExecuteScalarAsync()) > 0;

                string query;

                if (exists)
                {
                    query = "UPDATE StoreSettings SET StoreName = @StoreName, Address = @Address, Phone = @Phone, LogoPath = @LogoPath WHERE Id = @Id";
                   
                }
                else
                {
                    query = "INSERT INTO StoreSettings (StoreName, Address, Phone, LogoPath) VALUES (@StoreName, @Address, @Phone, @LogoPath)";                    
                }


                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StoreName", settings.Storename);
                cmd.Parameters.AddWithValue("@Address", settings.Address);
                cmd.Parameters.AddWithValue("@Phone", settings.Contact);
                cmd.Parameters.AddWithValue("@LogoPath", settings.LogoPath);

                return await cmd.ExecuteNonQueryAsync() > 0;
            }
            catch (Exception ex)
            {
                Logger.Log("Error checking store settings existence: " + ex.Message);
                return false;
            }
        }



    }
}
