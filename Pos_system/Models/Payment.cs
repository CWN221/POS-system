namespace Pos_system.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int SaleId { get; set; }
        public string Method { get; set; } = "Cash";
        public decimal Amount { get; set; }
        public string? Reference { get; set; }
        public string? MpesaCheckoutRequestId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}