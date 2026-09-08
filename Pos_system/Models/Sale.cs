/*
 * Sale.cs
 * This class represents a complete sale or transaction in the POS system, encapsulating details such as sale date, list of items sold, payment method & total amount.
 * Properties include:
 * - 'Id': Unique identifier for the sale transaction
 * - 'SaleDate': Date and time when the sale occurred
 * - 'Items': List of items included in the sale
 * - 'PaymentMethod': Method used for payment 
 * - 'TotalAmount': Total amount for the sale including all items and taxes
 */

namespace Pos_system.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;

        public List<SaleItem> Items { get; set; } = new List<SaleItem>();
        public string PaymentMethod { get; set; } = "";

        public decimal TotalAmount
        {
            get
            {
                decimal total = 0;
                foreach(var item in Items)
                {
                    total += item.TotalPrice;
                }
                return total;
            }
        }
        
    }
}
