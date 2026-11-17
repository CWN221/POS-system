namespace Pos_system.Models
{
    public class ReturnPrintData
    {
        public int ReturnId { get; set; }
        public string ReceiptNo { get; set; } = "";
        public int OriginalSaleId { get; set; }
        public string Reason { get; set; } = "";
        public string RefundType { get; set; } = "Cash";
        public decimal RefundAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RequestedByName { get; set; } = "—";
        public string ApprovedByName { get; set; } = "—";
        public List<ReturnPrintItem> Items { get; set; } = new();
    }

    public class ReturnPrintItem
    {
        public string ProductName { get; set; } = "";
        public int Quantity { get; set; }
        public decimal LineAmount { get; set; }
    }
}