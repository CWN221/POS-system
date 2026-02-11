/*
 * 
 * This file handles the user authoriztion of sign up and login into the system
 * It accesse the Database to verify the credentials
 * 
*/

using Microsoft.VisualBasic.ApplicationServices;
using MySql.Data.MySqlClient;
using Pos_system.Database;
using Pos_system.logs;
using Pos_system.Models;
using BCrypt.Net;

namespace Pos_system.Services
{
    public class Authorization
    {

        /*
         * 
         * Method to create a new user and add to the DB (Signup)
         * 
         */
        public async Task<string> Signup(string username, string password, string role)
        {

            using var conn = DBConnection.GetConnection();
            string query = "INSERT INTO users(Username, PasswordHash, Role, IsActive) VALUES(@Username, @Passwordhash, @Role, @IsActive)";

            if (conn == null)
            {
                return "Database could not be reached!";
            }

            if (CheckUsername(username))
            {
                return "Username already exists. Please choose a different username.";
            }

            try
            {
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Passwordhash", BCrypt.Net.BCrypt.HashPassword(password));
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@IsActive", true);

                await cmd.ExecuteNonQueryAsync();
                return "User Registration Successful";
            }
            catch (MySqlException e)
            {
                return "Registration failed: " + e.Message;
            }            
        }


        /*
         * User Login Method
         *
        */
        public UserModel? Login(string username, string password)
        {
            using var conn = DBConnection.GetConnection();

            if (conn == null)  return null;

            string query = "SELECT Id, Username, PasswordHash, Role, IsActive FROM users WHERE Username = @Username";

            try
            {
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string hashed_password = reader.GetString("PasswordHash");

                    if(!reader.GetBoolean("IsActive"))
                    {
                        throw new Exception("User account is inactive. Please contact administrator.");
                    }

                    if (!BCrypt.Net.BCrypt.Verify(password, hashed_password)) 
                        return null;

                    return new UserModel
                    {
                        Id = reader.GetInt32("Id"),
                        Username = reader.GetString("Username"),
                        PasswordHash = hashed_password,
                        Role = reader.GetString("Role"),
                        IsActive = reader.GetBoolean("IsActive")
                    };

                }
                
            }
            catch (MySqlException e)
            {
                Logger.Log("Login failed: " + e.Message);
            }
            return null;

        }        



        // Method to check if user already exists
        private bool CheckUsername(string username)
        {
            using var conn = DBConnection.GetConnection();

            string query = "SELECT COUNT(*) FROM users WHERE Username = @Username";

            using var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
       

    }
}
