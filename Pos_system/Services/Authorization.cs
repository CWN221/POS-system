/*
 * 
 * This file handles the user authoriztion of sign up and login into the system
 * It accesse the Database to verify the credentials
 * 
*/

using MySql.Data.MySqlClient;
using Pos_system.Database;

namespace Pos_system.Services
{
    public class Authorization
    {

        /*
         * 
         * Method to create a new user and add to the DB (Signup)
         * 
         */
        public string signup(string username, string password)
        {
            var conn = DBConnection.GetConnection();
            string query = "INSERT INTO users(username, password) VALUES(@username, @password)";

            if (conn == null)
            {
                return "Database could not be reached!";
            }

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                cmd.ExecuteNonQuery();
                return "User Registration Successful";
            }
            catch (MySqlException e)
            {
                return "Registration failed: " + e.Message;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Dispose();
                }
            }
        }


        /*
         * User Login Method
         *
        */
        public string login(string username, string password)
        {
            var conn = DBConnection.GetConnection();

            if (conn == null)
            {
                return "Database could not be reached!";
            }

            string query = "SELECT password FROM users WHERE username = @username";

            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string saved_password = reader.GetString("password");
                    if (saved_password == password)
                    {
                        return "Login Successful";
                    }
                    else
                    {
                        return "Incorrect Username/Password!";
                    }
                }
                else
                {
                    return "User not found!";
                }
            }
            catch (MySqlException e)
            {
                return "Login failed: " + e.Message;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Dispose();
                }
            }

        }


    }
}
