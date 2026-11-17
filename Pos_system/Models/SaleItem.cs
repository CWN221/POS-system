using Pos_system.Core;

namespace Pos_system.Models
{
    public class SaleItem
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required decimal Price { get; set; }
        public required decimal SellingPrice { get; set; }
        public decimal ProfitPerUnit => SellingPrice - Price;
        public decimal Profit => (SellingPrice - Price) * Quantity;
        public int Quantity { get; set; }
        public decimal Discount => (Price - SellingPrice) * Quantity;
        public string VatCategory { get; set; } = string.Empty;
        public decimal VatPercentage { get; set; }

        /// <summary>VAT-inclusive line total (what the customer pays).</summary>
        public decimal GrossTotal => VatCalculator.GrossTotal(SellingPrice, Quantity);

        /// <summary>Net amount excluding VAT.</summary>
        public decimal Subtotal => VatCalculator.ExtractNet(GrossTotal, VatPercentage);

        /// <summary>VAT component extracted from the inclusive price.</summary>
        public decimal VatAmount => VatCalculator.ExtractVat(GrossTotal, VatPercentage);

        /// <summary>Same as gross — price is VAT-inclusive.</summary>
        public decimal TotalPrice => GrossTotal;
    }
}
