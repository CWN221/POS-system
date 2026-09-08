/**
 * 
 * GetUsers.cs
 * Service class to retrieve list of users from the database.
 * This is used in Settings page(Users tab) to display a list of users and their details.
 *
 **/

using System.Data;
using Pos_system.Models;
using Pos_system.Database;
using Npgsql;
using Pos_system.logs;

namespace Pos_system.Services
{
    public class GetUsers
    {
        // Method to get all users from the DB
        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = new List<UserModel>();
            using var conn = DBConnection.GetConnection();

            if (conn == null) return users;

            string query = "SELECT Id, Username, Role, IsActive FROM Users";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var user = new UserModel
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        Role = Enum.Parse<UserRole>(reader.GetString(reader.GetOrdinal("Role"))),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                    };
                    users.Add(user);
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