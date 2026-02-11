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
using MySql.Data.MySqlClient;
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

                using var cmd = new MySqlCommand(query, conn);
                using var reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var user = new UserModel
                    {
                        Id = reader.GetInt32("Id"),
                        Username = reader.GetString("Username"),
                        Role = reader.GetString("Role"),
                        IsActive = reader.GetBoolean("IsActive")
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
