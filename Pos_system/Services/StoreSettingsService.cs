using Npgsql;
using Pos_system.Database;
using Pos_system.logs;
using Pos_system.Models;

namespace Pos_system.Services
{
    /**
    * StoreSettingsService.cs
    * 
    * This file is responsible for managing the store settings in the POS System.
    * It provides methods to edit, save & retrieve store settings e.g. store name, address, contact info.
    * 
    */
    public class StoreSettingsService
    {
        // Method to save store info
        public async Task<bool> SaveStoreSettings(StoreSettings settings)
        {
            using var conn = DBConnection.GetConnection() ?? throw new InvalidOperationException("Database connection failed.");
            try
            {
                string checkQuery = "SELECT COUNT(*) FROM StoreSettings WHERE BranchId = @BranchId";
                using var checkcmd = new NpgsqlCommand(checkQuery, conn);
                checkcmd.Parameters.AddWithValue("@BranchId", settings.BranchId);

                var exists = Convert.ToInt64(await checkcmd.ExecuteScalarAsync()) > 0;

                string query;

                if (exists)
                {
                    query = "UPDATE StoreSettings SET BranchId = @BranchId, StoreName = @StoreName, Address = @Address, Phone1 = @Phone1, Phone2 = @Phone2, Email = @Email, Business_PIN = @Business_PIN, LogoPath = @LogoPath WHERE BranchId = @BranchId";
                }
                else
                {
                    query = "INSERT INTO StoreSettings (BranchId, StoreName, Address, Phone1, Phone2, Email, Business_PIN, LogoPath) VALUES (@BranchId, @StoreName, @Address, @Phone1, @Phone2, @Email, @Business_PIN, @LogoPath)";
                }

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BranchId", Core.AppContext.CurrentBranchId);
                cmd.Parameters.AddWithValue("@StoreName", settings.Storename ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", settings.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone1", settings.Contact ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone2", settings.Contact2 ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", settings.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Business_PIN", settings.BusinessPin ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@LogoPath", settings.LogoPath ?? (object)DBNull.Value);

                return await cmd.ExecuteNonQueryAsync() > 0;
            }
            catch (Exception ex)
            {
                Logger.Log("Error checking store settings existence: " + ex.Message);
                return false;
            }
        }

        // Method to get Store settings and display them on labels
        public async Task<StoreSettings?> GetStoreSettings(int branchId)
        {
            using var conn = DBConnection.GetConnection() ?? throw new InvalidOperationException("Database connection failed!");
            try
            {
                string query = "SELECT * FROM storesettings WHERE BranchId = @BranchId";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BranchId", branchId);

                using var reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new StoreSettings
                    {
                        BranchId = branchId,
                        Storename = reader["StoreName"].ToString(),
                        Address = reader["Address"].ToString(),
                        Contact = reader["Phone1"].ToString(),
                        Contact2 = reader["Phone2"].ToString(),
                        Email = reader["Email"].ToString(),
                        BusinessPin = reader["Business_PIN"].ToString(),
                        LogoPath = reader["LogoPath"].ToString()
                    };
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.Log("Error fetching Store settings: " + ex.Message);
                return null;
            }
        }

        // Method to add a new Branch to the DB
        public async Task<bool> SaveBranch(StoreBranch branch)
        {
            using var conn = DBConnection.GetConnection() ?? throw new InvalidOperationException("Database connection failed.");
            try
            {
                string query = "INSERT INTO branches (BranchName, Location) VALUES(@BranchName, @Location)";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BranchName", branch.BranchName);
                cmd.Parameters.AddWithValue("@Location", branch.Location ?? (object)DBNull.Value);

                return await cmd.ExecuteNonQueryAsync() > 0;
            }
            catch (Exception ex)
            {
                Logger.Log("Error saving branch: " + ex.Message);
                return false;
            }
        }

        // Method to fetch all branches
        public async Task<List<StoreBranch>> GetAllBranches()
        {
            using var conn = DBConnection.GetConnection() ?? throw new InvalidOperationException("Database connection error.");

            List<StoreBranch> branches = new();

            try
            {
                string query = "SELECT Id, BranchName, Location FROM branches";

                using var cmd = new NpgsqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    branches.Add(new StoreBranch
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        BranchName = reader["BranchName"].ToString(),
                        Location = reader["Location"].ToString()
                    });
                }
                return branches;
            }
            catch (Exception ex)
            {
                Logger.Log("Error loading branches: " + ex.Message);
                return branches;
            }
        }
    }
}