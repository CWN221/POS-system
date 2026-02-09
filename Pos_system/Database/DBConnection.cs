/*
 * 
 * This file stores the logic to connect to the MySQL Database
 * Create a database named "supermarket_pos"
 * 
 * It can be accessed in other classes using DBConnection.GetConnection()
 * 
*/

using System;
using MySql.Data.MySqlClient;

namespace Pos_system.Database
{
    public static class DBConnection
    {
        private static string connStr = ("server=localhost; user id=root; password=; database=supermarket_pos");

        public static MySqlConnection GetConnection()
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open();
                Console.WriteLine("Database Connection Successful.");
                return conn;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Database Connection failed " + e.Message);
                return null;
            }
        }

    }
}
