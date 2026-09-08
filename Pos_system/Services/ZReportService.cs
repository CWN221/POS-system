using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.Models;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Services
{
    public class ZReportService
    {
        public async Task<ZReport> GenerateZReport(DateTime date, int? branchId = null)
        {
            branchId ??= AppContext.CurrentBranchId;
            AppContext.EnsureBranchAccess(branchId.Value);

            using var conn = DBConnection.GetConnection();

            decimal totalSales = 0;
            int transactions = 0;
            decimal totalVat = 0;

            var report = new ZReport
            {
                ReportDate = date,
                CashTotal = 0,
                MpesaTotal = 0,
                CardTotal = 0
            };

            const string query = @"
                SELECT payment_method, COALESCE(SUM(total_amount), 0) AS total, COUNT(id) AS transactions
                FROM sales
                WHERE DATE(sale_date) = @date AND BranchId = @branchId
                GROUP BY payment_method";

            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@date", date.Date);
                cmd.Parameters.AddWithValue("@branchId", branchId);

                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    string? method = reader["payment_method"] == DBNull.Value ? null : reader["payment_method"].ToString();
                    decimal amount = reader.GetDecimal(reader.GetOrdinal("total"));
                    int count = reader.GetInt32(reader.GetOrdinal("transactions"));

                    totalSales += amount;
                    transactions += count;

                    switch (method)
                    {
                        case "Cash": report.CashTotal = amount; break;
                        case "Mpesa": report.MpesaTotal = amount; break;
                        case "Card": report.CardTotal = amount; break;
                    }
                }
            }

            const string vatQuery = @"
                SELECT COALESCE(SUM(si.vat_amount), 0)
                FROM sale_items si
                JOIN sales s ON si.sale_id = s.id
                WHERE DATE(s.sale_date) = @date AND s.BranchId = @branchId";

            using (var vatCmd = new NpgsqlCommand(vatQuery, conn))
            {
                vatCmd.Parameters.AddWithValue("@date", date.Date);
                vatCmd.Parameters.AddWithValue("@branchId", branchId);
                totalVat = Convert.ToDecimal(await vatCmd.ExecuteScalarAsync() ?? 0);
            }

            const string upsert = @"
                INSERT INTO z_reports(report_date, branch_id, total_sales, total_vat, total_transactions)
                VALUES(@date, @branchId, @sale, @vat, @transactions)
                ON CONFLICT (report_date, branch_id)
                DO UPDATE SET total_sales = EXCLUDED.total_sales, total_vat = EXCLUDED.total_vat,
                              total_transactions = EXCLUDED.total_transactions, generated_at = CURRENT_TIMESTAMP";

            using (var insertCmd = new NpgsqlCommand(upsert, conn))
            {
                insertCmd.Parameters.AddWithValue("@date", date.Date);
                insertCmd.Parameters.AddWithValue("@branchId", branchId);
                insertCmd.Parameters.AddWithValue("@sale", totalSales);
                insertCmd.Parameters.AddWithValue("@vat", totalVat);
                insertCmd.Parameters.AddWithValue("@transactions", transactions);
                await insertCmd.ExecuteNonQueryAsync();
            }

            report.TotalSales = totalSales;
            report.TotalVat = totalVat;
            report.TotalTransactions = transactions;

            await AppServices.GetRequiredService<AuditService>()
                .LogAsync("Z_REPORT_GENERATED", "z_report", date.ToString("yyyy-MM-dd"), $"Branch: {branchId}");

            return report;
        }
    }
}
