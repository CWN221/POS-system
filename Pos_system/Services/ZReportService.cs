using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.Models;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Services
{
    public class ZReportService
    {
        public async Task<ZReport> GenerateZReportAsync(int branchId, DateTime? asOf = null)
        {
            AppContext.EnsureBranchAccess(branchId);
            var reportDate = (asOf ?? DateTime.Now).Date;

            var existing = await GetExistingZReportAsync(branchId, reportDate);
            if (existing != null && existing.IsLocked)
            {
                throw new InvalidOperationException(
                    $"Z-report for {reportDate:dd MMM yyyy} is already closed and locked. It cannot be regenerated.");
            }

            return await BuildReportAsync(branchId, asOf ?? DateTime.Now, persist: true);
        }

        public async Task<ZReport?> GetExistingZReportAsync(int branchId, DateTime reportDate)
        {
            using var conn = DBConnection.GetConnection();
            const string query = @"
                SELECT id, report_date, branch_id, total_sales, total_vat, total_transactions,
                       gross_sales, refund_amount, net_sales,
                       cash_total, mpesa_total, card_total,
                       closed_by, is_locked, generated_at
                FROM z_reports
                WHERE branch_id = @branchId AND report_date = @reportDate";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@branchId", branchId);
            cmd.Parameters.AddWithValue("@reportDate", reportDate.Date);

            using var reader = await cmd.ExecuteReaderAsync();
            if (!await reader.ReadAsync()) return null;

            return ReadReport(reader);
        }

        private async Task<ZReport> BuildReportAsync(int branchId, DateTime cutoff, bool persist)
        {
            using var conn = DBConnection.GetConnection();
            await using var transaction = await conn.BeginTransactionAsync();

            try
            {
                var report = new ZReport
                {
                    ReportDate = cutoff.Date,
                    BranchId = branchId
                };

                var dayStart = cutoff.Date;
                var dayEnd = dayStart.AddDays(1);

                const string salesQuery = @"
                    SELECT COALESCE(SUM(total_amount), 0) AS gross,
                           COALESCE(SUM((SELECT COALESCE(SUM(si.vat_amount), 0)
                                         FROM sale_items si
                                         WHERE si.sale_id = s.id)), 0) AS vat,
                           COUNT(*) AS txn_count
                    FROM sales s
                    WHERE s.BranchId = @branchId
                      AND s.sale_date >= @dayStart
                      AND s.sale_date < @dayEnd
                      AND s.is_voided = FALSE";

                using (var cmd = new NpgsqlCommand(salesQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@branchId", branchId);
                    cmd.Parameters.AddWithValue("@dayStart", dayStart);
                    cmd.Parameters.AddWithValue("@dayEnd", dayEnd);

                    using var reader = await cmd.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        report.GrossSales = reader.GetDecimal(0);
                        report.TotalVat = reader.GetDecimal(1);
                        report.TotalTransactions = reader.GetInt32(2);
                    }
                }

                const string refundQuery = @"
                    SELECT COALESCE(SUM(refund_amount), 0)
                    FROM returns
                    WHERE branch_id = @branchId
                      AND created_at >= @dayStart
                      AND created_at < @dayEnd";

                using (var cmd = new NpgsqlCommand(refundQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@branchId", branchId);
                    cmd.Parameters.AddWithValue("@dayStart", dayStart);
                    cmd.Parameters.AddWithValue("@dayEnd", dayEnd);

                    var result = await cmd.ExecuteScalarAsync();
                    report.RefundAmount = result != null && result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                }

                report.NetSales = report.GrossSales - report.RefundAmount;
                report.TotalSales = report.NetSales;

                const string paymentQuery = @"
                    SELECT p.method, COALESCE(SUM(p.amount), 0) AS total
                    FROM payments p
                    JOIN sales s ON s.id = p.sale_id
                    WHERE s.BranchId = @branchId
                      AND s.sale_date >= @dayStart
                      AND s.sale_date < @dayEnd
                    GROUP BY p.method";

                using (var cmd = new NpgsqlCommand(paymentQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@branchId", branchId);
                    cmd.Parameters.AddWithValue("@dayStart", dayStart);
                    cmd.Parameters.AddWithValue("@dayEnd", dayEnd);

                    using var reader = await cmd.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        var method = reader.IsDBNull(0) ? "" : reader.GetString(0);
                        var amount = reader.GetDecimal(1);

                        switch (method)
                        {
                            case "Cash": report.CashTotal = amount; break;
                            case "Mpesa": report.MpesaTotal = amount; break;
                            case "Card": report.CardTotal = amount; break;
                        }
                    }
                }

                if (persist)
                {
                    const string insertQuery = @"
                        INSERT INTO z_reports(
                            report_date, branch_id, total_sales, total_vat, total_transactions,
                            gross_sales, refund_amount, net_sales,
                            cash_total, mpesa_total, card_total,
                            closed_by, is_locked, generated_at)
                        VALUES(
                            @report_date, @branch_id, @total_sales, @total_vat, @total_transactions,
                            @gross_sales, @refund_amount, @net_sales,
                            @cash_total, @mpesa_total, @card_total,
                            @closed_by, TRUE, CURRENT_TIMESTAMP)
                        RETURNING id";

                    using var cmd = new NpgsqlCommand(insertQuery, conn, transaction);
                    cmd.Parameters.AddWithValue("@report_date", report.ReportDate);
                    cmd.Parameters.AddWithValue("@branch_id", branchId);
                    cmd.Parameters.AddWithValue("@total_sales", report.TotalSales);
                    cmd.Parameters.AddWithValue("@total_vat", report.TotalVat);
                    cmd.Parameters.AddWithValue("@total_transactions", report.TotalTransactions);
                    cmd.Parameters.AddWithValue("@gross_sales", report.GrossSales);
                    cmd.Parameters.AddWithValue("@refund_amount", report.RefundAmount);
                    cmd.Parameters.AddWithValue("@net_sales", report.NetSales);
                    cmd.Parameters.AddWithValue("@cash_total", report.CashTotal);
                    cmd.Parameters.AddWithValue("@mpesa_total", report.MpesaTotal);
                    cmd.Parameters.AddWithValue("@card_total", report.CardTotal);
                    cmd.Parameters.AddWithValue("@closed_by", AppContext.CurrentUserId);

                    report.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    report.IsLocked = true;
                    report.ClosedBy = AppContext.CurrentUserId;
                    report.GeneratedAt = DateTime.Now;
                }

                await transaction.CommitAsync();

                if (persist)
                {
                    await AppServices.GetRequiredService<AuditService>().LogAsync("Z_REPORT_CLOSED", "z_report", report.Id.ToString(),$"Date: {report.ReportDate:yyyy-MM-dd}, Net: {report.NetSales:C}");
                }

                return report;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        private static ZReport ReadReport(NpgsqlDataReader reader)
        {
            return new ZReport
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                ReportDate = reader.GetDateTime(reader.GetOrdinal("report_date")),
                BranchId = reader.IsDBNull(reader.GetOrdinal("branch_id")) ? 0 : reader.GetInt32(reader.GetOrdinal("branch_id")),
                TotalSales = reader.IsDBNull(reader.GetOrdinal("total_sales")) ? 0 : reader.GetDecimal(reader.GetOrdinal("total_sales")),
                TotalVat = reader.IsDBNull(reader.GetOrdinal("total_vat")) ? 0 : reader.GetDecimal(reader.GetOrdinal("total_vat")),
                TotalTransactions = reader.IsDBNull(reader.GetOrdinal("total_transactions")) ? 0 : reader.GetInt32(reader.GetOrdinal("total_transactions")),
                GrossSales = reader.IsDBNull(reader.GetOrdinal("gross_sales")) ? 0 : reader.GetDecimal(reader.GetOrdinal("gross_sales")),
                RefundAmount = reader.IsDBNull(reader.GetOrdinal("refund_amount")) ? 0 : reader.GetDecimal(reader.GetOrdinal("refund_amount")),
                NetSales = reader.IsDBNull(reader.GetOrdinal("net_sales")) ? 0 : reader.GetDecimal(reader.GetOrdinal("net_sales")),
                CashTotal = reader.IsDBNull(reader.GetOrdinal("cash_total")) ? 0 : reader.GetDecimal(reader.GetOrdinal("cash_total")),
                MpesaTotal = reader.IsDBNull(reader.GetOrdinal("mpesa_total")) ? 0 : reader.GetDecimal(reader.GetOrdinal("mpesa_total")),
                CardTotal = reader.IsDBNull(reader.GetOrdinal("card_total")) ? 0 : reader.GetDecimal(reader.GetOrdinal("card_total")),
                ClosedBy = reader.IsDBNull(reader.GetOrdinal("closed_by")) ? null : reader.GetInt32(reader.GetOrdinal("closed_by")),
                IsLocked = !reader.IsDBNull(reader.GetOrdinal("is_locked")) && reader.GetBoolean(reader.GetOrdinal("is_locked")),
                GeneratedAt = reader.IsDBNull(reader.GetOrdinal("generated_at")) ? DateTime.Now : reader.GetDateTime(reader.GetOrdinal("generated_at"))
            };
        }
    }
}