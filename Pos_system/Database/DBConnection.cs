/*
 * 
 * This file stores the logic to connect to the PostgreSQL Database
 * Create a database named "supermarket_pos"
 * 
 * It can be accessed in other classes using DBConnection.GetConnection()
 * 
*/

using System;
using Npgsql;

namespace Pos_system.Database
{
    public static class DBConnection
    {
        private static readonly string? connStr = Environment.GetEnvironmentVariable("POS_DB_CONNECTION");

        public static NpgsqlConnection GetConnection()
        {            

            if (string.IsNullOrWhiteSpace(connStr))
                throw new InvalidOperationException("Database connection string is not set. Please set the POS_DB_CONNECTION environment variable.");

            var conn = new NpgsqlConnection(connStr);
            conn.Open();
            return conn;            
        }

    }
}
