/*
 * 
 * Implemented to generate Z reports for the POS system, which summarize daily sales, VAT & transaction counts.
 * Hanled in ZReportService, this class serves as a data model for Z reports, allowing the system to capture and manage daily sales summaries.
 * 
 * The class contains properties that represent the details of a Z report in the POS system.
 * Properties include:
 * - 'Id': Represents the unique identifier for the Z report.
 * - 'ReportDate':represents the date of the report.
 * - 'BranchId': Represents the unique identifier of the branch the report belongs to.
 * - 'TotalSales': Represents the total sales amount for the report date.
 * - 'TotalVat': Represents the total VAT amount for the report date.
 * - 'TotalTransactions': Represents the total number of transactions for the report date.
 * - 'GrossSales': Represents total sales before refunds are deducted.
 * - 'RefundAmount': Represents the total amount refunded on the report date.
 * - 'NetSales': Represents total sales after refunds have been deducted.
 * - 'CashTotal': Represents the total amount received via cash.
 * - 'MpesaTotal': Represents the total amount received via M-Pesa.
 * - 'CardTotal': Represents the total amount received via card.
 * - 'ClosedBy': Represents the user who closed the report.
 * - 'IsLocked': Indicates whether the report is final and cannot be regenerated.
 * - 'GeneratedAt': Represents the timestamp when the report was generated.
 * 
 */

namespace Pos_system.Models
{
    public class ZReport
    {
        public int Id { get; set; }
        public DateTime ReportDate { get; set; }
        public int BranchId { get; set; }
        public decimal TotalSales { get; set; }
        public decimal TotalVat { get; set; }
        public int TotalTransactions { get; set; }

        public decimal GrossSales { get; set; }
        public decimal RefundAmount { get; set; }
        public decimal NetSales { get; set; }

        public decimal CashTotal { get; set; }
        public decimal MpesaTotal { get; set; }
        public decimal CardTotal { get; set; }

        public int? ClosedBy { get; set; }
        public bool IsLocked { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
}