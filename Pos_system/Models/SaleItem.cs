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
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public decimal VatPercentage { get; set; } = 0;
        public decimal VatAmount => Math.Round((Price * Quantity) * (VatPercentage / 100),2);
        public decimal TotalPrice => Math.Round((Price * Quantity) + VatAmount, 2);

    }
}
