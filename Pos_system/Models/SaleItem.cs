/*
 * 
 * Creating a new Sale
 * 
 */

using System;

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
        public int Quantity { get; set; }
        public decimal Subtotal => Math.Round(SellingPrice * Quantity, 2);
        public decimal Discount => Math.Round((Price - SellingPrice) * Quantity, 2);
        public decimal VatPercentage { get; set; } = 0;
        public decimal VatAmount => Math.Round(Subtotal * (VatPercentage / 100),2);
        public decimal TotalPrice => Math.Round(Subtotal + VatAmount, 2);

    }
}
