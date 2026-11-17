namespace Pos_system.Models
{
    public class GoodsReceipt
    {
        public int Id { get; set; }
        public string ReceiptNumber { get; set; } = "";
        public int PoId { get; set; }
        public string PoNumber { get; set; } = "";
        public int BranchId { get; set; }
        public int? ReceivedBy { get; set; }
        public string ReceivedByName { get; set; } = "";
        public string? Notes { get; set; }
        public DateTime ReceivedAt { get; set; } = DateTime.Now;
        public List<GoodsReceiptItem> Items { get; set; } = new();
    }

    public class GoodsReceiptItem
    {
        public int Id { get; set; }
        public int ReceiptId { get; set; }
        public int PoItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int QuantityReceived { get; set; }
        public decimal UnitCost { get; set; }
    }
}