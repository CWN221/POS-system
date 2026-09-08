/*
 * ReportsService.cs
 * Responsible for generating sales, vat & branch comparison reports based on specified date ranges and branch filters.
 * Handles database interactions to retrieve aggregated sales data, including total sales, total profit, total transactions & total VAT for the given criteria.
 * 
 */

using Npgsql;
using Pos_system.Database;
using Pos_system.Models;
using System.Data;

namespace Pos_system.Services
{
    public class ReportsService
    {
        // Method to load sales report based on date range from the DB
        public async Task<List<SalesReport>> LoadSalesReportAsync(DateTime from, DateTime to, int? branchId)
        {
            var results = new List<SalesReport>();

            using var conn = DBConnection.GetConnection() ?? throw new InvalidOperationException("Failed to establish database connection.");

            string query = @"
                    SELECT 
                        DATE(s.sale_date) AS SaleDate,
                        COALESCE(SUM(s.total_amount), 0) AS TotalSales,
                        COALESCE(SUM(si.profit), 0) AS TotalProfit,
                        COUNT(DISTINCT s.id) AS TotalTransactions,
                        COALESCE(SUM(si.vat_amount), 0) AS TotalVat
                    FROM sales s
                    JOIN sale_items si ON s.id = si.sale_id                 
                    WHERE (@BranchId IS NULL OR s.BranchId = @BranchId)
                    AND s.sale_date BETWEEN @FromDate AND @ToDate
                    GROUP BY DATE(s.sale_date)
                    ORDER BY SaleDate DESC";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BranchId", branchId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FromDate", from);
            cmd.Parameters.AddWithValue("@ToDate", to);

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new SalesReport
                {
                    Date = reader.GetDateTime(reader.GetOrdinal("SaleDate")),
                    TotalSales = reader.GetDecimal(reader.GetOrdinal("TotalSales")),
                    TotalProfit = reader.GetDecimal(reader.GetOrdinal("TotalProfit")),
                    TotalTransactions = reader.GetInt32(reader.GetOrdinal("TotalTransactions")),
                    TotalVat = reader.GetDecimal(reader.GetOrdinal("TotalVat"))
                });
            }

            return results;
        }

        // Method to load VAT report based on date range from the DB
        public async Task<List<VatReport>> LoadVatReportAsync(DateTime from, DateTime to, int? branchId)
        {
            var results = new List<VatReport>();

            using var conn = DBConnection.GetConnection() ?? throw new InvalidOperationException("Failed to establish database connection.");

            string query = @"
                            SELECT 
                                DATE(s.sale_date) AS SaleDate,
                                COALESCE(si.vat_category, 'Unknown') AS vat_category,
                                si.vat_percentage,
                                COALESCE(SUM(si.total_price - si.vat_amount), 0) AS TaxableAmount,
                                COALESCE(SUM(si.vat_amount), 0) AS VatAmount
                            FROM sale_items si
                            JOIN sales s ON si.sale_id = s.id
                            WHERE (@BranchId IS NULL OR s.BranchId = @BranchId)
                            AND s.sale_date BETWEEN @FromDate AND @ToDate
                            GROUP BY DATE(s.sale_date), si.vat_category, si.vat_percentage
                            ORDER BY SaleDate DESC;";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BranchId", branchId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@FromDate", from);
            cmd.Parameters.AddWithValue("@ToDate", to);

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new VatReport
                {
                    Date = reader.GetDateTime(reader.GetOrdinal("SaleDate")),
                    VatCategory = reader.GetString(reader.GetOrdinal("vat_category")),
                    VatPercentage = reader.GetDecimal(reader.GetOrdinal("vat_percentage")),
                    TaxableAmount = reader.GetDecimal(reader.GetOrdinal("TaxableAmount")),
                    VatAmount = reader.GetDecimal(reader.GetOrdinal("VatAmount"))
                });
            }

            return results;
        }

        // Method to load branch comparison report based on date range from the DB
        public async Task<List<BranchReport>> LoadBranchComparisonAsync(DateTime from, DateTime to)
        {
            var results = new List<BranchReport>();

            using var conn = DBConnection.GetConnection() ?? throw new InvalidOperationException("Failed to establish database connection.");

            string query = @"
                            SELECT 
                                b.Id AS BranchId,
                                b.BranchName,
                                b.Location,
                                COALESCE(SUM(s.total_amount), 0) AS TotalSales,
                                COALESCE(SUM(si.vat_amount), 0) AS TotalVat,
                                COALESCE(SUM(si.profit), 0) AS TotalProfit,
                                COUNT(DISTINCT s.id) AS TotalTransactions
                            FROM sales s
                            JOIN sale_items si ON s.id = si.sale_id
                            JOIN branches b ON s.BranchId = b.Id
                            WHERE s.sale_date BETWEEN @FromDate AND @ToDate
                            GROUP BY b.Id, b.BranchName
                            ORDER BY TotalSales DESC;";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@FromDate", from);
            cmd.Parameters.AddWithValue("@ToDate", to);

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new BranchReport
                {
                    BranchId = reader.GetInt32(reader.GetOrdinal("BranchId")),
                    BranchName = reader.GetString(reader.GetOrdinal("BranchName")),
                    Location = reader.GetString(reader.GetOrdinal("Location")),
                    TotalSales = reader.GetDecimal(reader.GetOrdinal("TotalSales")),
                    TotalVat = reader.GetDecimal(reader.GetOrdinal("TotalVat")),
                    TotalProfit = reader.GetDecimal(reader.GetOrdinal("TotalProfit")),
                    TotalTransactions = reader.GetInt32(reader.GetOrdinal("TotalTransactions"))
                });
            }

            return results;
        }

        // Method to load inventory report based on branch filter from the DB
        public async Task<List<InventoryReport>> LoadInventoryReportAsync(int? branchId)
        {
            var results = new List<InventoryReport>();

            using var conn = DBConnection.GetConnection() ?? throw new InvalidOperationException("Failed to establish database connection.");

            string query = @"
                            SELECT 
                                p.idproducts,
                                p.pName,
                                p.pCategory,
                                p.stock_quantity,
                                p.price,
                                p.sellingPrice,
                                p.stock_status,
                                (p.stock_quantity * p.price) AS StockValue
                            FROM products p
                            WHERE (@BranchId IS NULL OR p.BranchId = @BranchId)
                            ORDER BY p.pName;";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BranchId", branchId ?? (object)DBNull.Value);

            using var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                results.Add(new InventoryReport
                {
                    ProductId = reader.GetInt32(reader.GetOrdinal("idproducts")),
                    ProductName = reader.GetString(reader.GetOrdinal("pName")),
                    Category = reader.GetString(reader.GetOrdinal("pCategory")),
                    StockQuantity = reader.GetInt32(reader.GetOrdinal("stock_quantity")),
                    CostPrice = reader.GetDecimal(reader.GetOrdinal("price")),
                    SellingPrice = reader.GetDecimal(reader.GetOrdinal("sellingPrice")),
                    StockStatus = reader.GetString(reader.GetOrdinal("stock_status")),
                    StockValue = reader.GetDecimal(reader.GetOrdinal("StockValue"))
                });
            }

            return results;
        }

        public async Task<List<(string Month, decimal Total)>> GetMonthlySalesTrendAsync(int months = 6, int? branchId = null)
        {
            branchId ??= Pos_system.Core.AppContext.CurrentBranchId;
            var results = new List<(string, decimal)>();

            using var conn = DBConnection.GetConnection() ?? throw new InvalidOperationException("Failed to establish database connection.");

            const string query = @"
                SELECT TO_CHAR(DATE_TRUNC('month', s.sale_date), 'Mon') AS MonthLabel,
                       DATE_TRUNC('month', s.sale_date) AS MonthStart,
                       COALESCE(SUM(s.total_amount), 0) AS TotalSales
                FROM sales s
                WHERE s.BranchId = @BranchId
                  AND s.sale_date >= DATE_TRUNC('month', CURRENT_DATE) - (@Months || ' months')::INTERVAL
                GROUP BY DATE_TRUNC('month', s.sale_date)
                ORDER BY MonthStart";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@BranchId", branchId);
            cmd.Parameters.AddWithValue("@Months", months - 1);

            using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                results.Add((reader.GetString(0), reader.GetDecimal(2)));
            }

            return results;
        }
    }
}