/*
 * 
 * Implemented to generate Z reports for the POS system, which summarize daily sales, VAT & transaction counts.
 * Hanled in ZReportService, this class serves as a data model for Z reports, allowing the system to capture and manage daily sales summaries.
 * 
 * The class contains properties that represent the details of a Z report in the POS system.
 * Properties include:
 * - 'ReportDate':represents the date of the report.
 * - 'TotalSales': Represents the total sales amount for the report date.
 * - 'TotalVat': Represents the total VAT amount for the report date.
 * - 'TotalTransactions': Represents the total number of transactions for the report date.
 * 
 */

namespace Pos_system.Models
{
    public class ZReport
    {
        public DateTime ReportDate { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalVat { get; set; }
        public int TotalTransactions { get; set; }

        public decimal CashTotal { get; set; }
        public decimal MpesaTotal { get; set; }
        public decimal CardTotal { get; set; }
    }
}
