/*
 * 
 * This class is used to fetch the latest transactions from the DB
 * 
 */

using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Pos_system.Database;
using Pos_system.Models;

namespace Pos_system.Services
{
    public class TransactionDAO
    {

        public List<SaleTransaction> GetTransactions()
        {
            List<SaleTransaction> transactions = new List<SaleTransaction>();

            using var conn = DBConnection.GetConnection();
            var query = "SELECT * FROM sales ORDER BY sale_date DESC";

            try
            {
                using var cmd = new MySqlCommand(query, conn);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SaleTransaction newTransaction = new SaleTransaction
                    {
                        TransactionID = reader.GetInt32("id"),
                        TransactionDate = reader.GetDateTime("sale_date"),
                        TotalAmount = reader.GetDecimal("total_amount")
                    };
                    transactions.Add(newTransaction);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve transactions", ex);
            }           
            return transactions;
        }

    }
}
