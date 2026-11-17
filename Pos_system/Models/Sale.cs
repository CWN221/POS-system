/*
 * Sale.cs
 * This class represents a complete sale or transaction in the POS system, encapsulating details such as sale date, list of items sold, payment method & total amount.
 * Properties include:
 * - 'Id': Unique identifier for the sale transaction
 * - 'SaleDate': Date and time when the sale occurred
 * - 'Items': List of items included in the sale
 * - 'PaymentMethod': Method used for payment 
 * - 'MpesaReference': M-Pesa Reference message
 * - 'CustomerName': Customer name fetched from M-Pesa transaction
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

        public string? MpesaReference { get; set; }
        public string? MpesaStatus { get; set; }
        public string? MpesaCheckoutRequestId { get; set; }
        public List<Payment> Payments { get; set; } = new();

        public decimal AmountTendered { get; set; }

        public decimal ChangeGiven { get; set; }

        public string? CustomerName { get; set; }

        public Guid SaleUuid { get; set; } = Guid.NewGuid();

        public bool IsVoided { get; set; }
        public int? VoidedBy { get; set; }
        public DateTime? VoidedAt { get; set; }
        public string? VoidReason { get; set; }

        public string ReceiptNumber { get; set; } = "";
        public decimal StoreCreditUsed { get; set; }
        public string? CustomerPhone { get; set; }

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
