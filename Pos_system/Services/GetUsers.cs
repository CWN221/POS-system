using Pos_system.Models;
using Pos_system.Database;
using Npgsql;
using Pos_system.logs;

namespace Pos_system.Services
{
    public class GetUsers
    {
        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = new List<UserModel>();
            using var conn = DBConnection.GetConnection();

            const string query = "SELECT Id, Username, Role, IsActive, BranchId FROM users WHERE IsActive = true ORDER BY Username";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    users.Add(new UserModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        PasswordHash = string.Empty,
                        Role = Enum.Parse<UserRole>(reader.GetString(reader.GetOrdinal("Role"))),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        BranchId = reader.GetInt32(reader.GetOrdinal("BranchId"))
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error retrieving users from database." + ex.Message);
            }
            return users;
        }
    }
}
