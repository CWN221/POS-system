/*
 * 
 * Handled in the ReportsService, this class serves as a data model for sales reports, allowing the system to process and manage sales data.
 * 
 * The SalesReport class contains properties to hold information about sales including date of the report, total sales amount, total profits, total VAT & total number of transactions.
 * - 'Date': Represents the date for which the sales report is generated.
 * - 'TotalSales': Represents the total sales amount for the specified date.
 * - 'TotalProfit': Represents the total profit amount for the specified date.
 * - 'TotalVat': Represents the total VAT amount for the specified date.
 * - 'TotalTransactions': Represents the total number of transactions that occurred on the specified date.
 */

namespace Pos_system.Models
{
    public class SalesReport
    {
        public DateTime Date { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalProfit { get; set; }
        public decimal TotalVat { get; set; }
        public int TotalTransactions { get; set; }
    }
}
