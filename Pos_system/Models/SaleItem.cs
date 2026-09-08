/*
 * SaleItem.cs
 * Creating a new Sale 
 * This class represents an individual item in a sale transaction, encapsulating details such as product info, pricing, quantity, VAT calculations & profit. 
 * It serves as a data model for items being sold in the POS system.
 * Properties include:
 * - 'Id': Unique identifier for the sale item.
 * - 'SaleId': Identifier linking the item to a specific sale transaction.
 * - 'ProductId': Identifier for the product being sold.
 * - 'ProductName': Name of the product
 * - 'Price': Original/Marked price of the product
 * - 'SellingPrice': Price at which the product is sold
 * - 'Profit': Calculated profit for the item based on the difference between selling price and original price, multiplied by quantity.
 * - 'Quantity': Number of units sold
 * - 'Subtotal': Total price for the item before VAT calculated as (selling price * quantity)
 * - 'Discount': Total discount applied to the item calculated as (original price - selling price) * quantity
 * - 'VatCategory': VAT category applicable to the product
 * - 'VatPercentage': VAT percentage applicable to the product
 * - 'VatAmount': Total VAT amount for the item calculated as (subtotal * (VAT percentage / 100))
 * - 'TotalPrice': Total price for the item including VAT calculated as (subtotal + VAT amount)
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
        public decimal ProfitPerUnit => SellingPrice - Price;
        public decimal Profit => (SellingPrice - Price) * Quantity;
        public int Quantity { get; set; }
        public decimal Subtotal => SellingPrice * Quantity;
        public decimal Discount => (Price - SellingPrice) * Quantity;
        public string VatCategory { get; set; }
        public decimal VatPercentage { get; set; }
        public decimal VatAmount => Subtotal * (VatPercentage / 100);
        public decimal TotalPrice => Subtotal + VatAmount;

    }
}
