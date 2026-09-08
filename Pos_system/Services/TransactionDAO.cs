/*
 * 
 * This class is used to fetch the latest transactions from the DB
 * 
 */

using Npgsql;
using Pos_system.Database;
using Pos_system.Models;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Services
{
    public class TransactionDAO
    {
        public List<SaleTransaction> GetTransactionsByCurrentBranch(int limit = 50)
        {
            var branchId = AppContext.CurrentBranchId;
            var transactions = new List<SaleTransaction>();

            using var conn = DBConnection.GetConnection();
            var query = "SELECT id, sale_date, total_amount FROM sales WHERE BranchId = @BranchId ORDER BY sale_date DESC LIMIT @limit";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BranchId", branchId);
                cmd.Parameters.AddWithValue("@limit", limit);

                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SaleTransaction newTransaction = new SaleTransaction
                    {
                        TransactionID = reader.GetInt32(reader.GetOrdinal("id")),
                        TransactionDate = reader.GetDateTime(reader.GetOrdinal("sale_date")),
                        TotalAmount = reader.GetDecimal(reader.GetOrdinal("total_amount"))
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