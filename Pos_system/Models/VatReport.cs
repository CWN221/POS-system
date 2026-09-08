/*
 * VatReport.cs
 * This class represents the structure of a VAT report entry in the POS system, encapsulating details such as the date of the transaction, VAT category, VAT percentage, taxable amount & VAT amount.
 * It serves as a data model for generating VAT reports, allowing the system to process and manage VAT-related data.
 * Properties include:
 * - 'Date': Represents the date for which the VAT report entry is generated.
 * - 'VatCategory': Represents the category of VAT applicable to the transaction.
 * - 'VatPercentage': Represents the percentage of VAT applied to the taxable amount.
 * - 'TaxableAmount': Represents the amount subject to VAT.
 * - 'VatAmount': Represents the calculated VAT amount for the transaction.
 * 
 */

namespace Pos_system.Models
{
    public class VatReport
    {
        public DateTime Date { get; set; }
        public string VatCategory { get; set; } = "";
        public decimal VatPercentage { get; set; }
        public decimal TaxableAmount { get; set; }
        public decimal VatAmount { get; set; }
    }
}
