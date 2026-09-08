/*
 * This is implemented to represent a return request in the POS system. It is used to capture the details of a return transaction initiated by a customer.
 * Handled in the ReturnsService, this class serves as a data model for return requests, allowing the system to process and manage returns.
 * 
 * The class contains properties that represent the details of a return request in the POS system.
 * The properties include:
 * - 'ReceiptNo': Represents the receipt number associated with the return.
 * - 'SaleId': An integer representing the unique identifier of the sale transaction.
 * - 'Reason': A string describing the reason for the return.
 * - 'RefundType': A string indicating the type of refund (e.g., cash, store credit).
 * - 'RequestedBy': User ID of the person processing the return.
 * 
 */

namespace Pos_system.Models
{
    public class ReturnRequest
    {
        public string ReceiptNo { get; set; }
        public int SaleId { get; set; }
        public List<ReturnItemRequest> Items { get; set; } = new();
        public string Reason { get; set; }
        public string RefundType { get; set; }
        public int RequestedBy { get; set; }
        public int BranchId { get; set; }

    }
}
