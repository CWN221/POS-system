namespace Pos_system.Models
{
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public string PoNumber { get; set; } = "";
        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = "";
        public int BranchId { get; set; }
        public string Status { get; set; } = "DRAFT";
        public DateTime OrderDate { get; set; } = DateTime.Today;
        public DateTime? ExpectedDate { get; set; }
        public string? Notes { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByName { get; set; } = "";
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<PurchaseOrderItem> Items { get; set; } = new();

        public decimal TotalValue => Items.Sum(i => i.QuantityOrdered * i.UnitCost);
        public int TotalItems => Items.Sum(i => i.QuantityOrdered);
        public bool IsFullyReceived => Items.All(i => i.QuantityReceived >= i.QuantityOrdered);
    }

    public class PurchaseOrderItem
    {
        public int Id { get; set; }
        public int PoId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }
        public decimal UnitCost { get; set; }

        public int QuantityOutstanding => Math.Max(0, QuantityOrdered - QuantityReceived);
    }
}