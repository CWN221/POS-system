/*
 * 
 * Handled in the ReturnsService, this class serves as a data model for return results, allowing the system to represent the outcome of a return transaction.
 * This class is used to represent the result of a return transaction in the POS system, indicating whether the return was approved, amount refunded & any relevant message.
 * 
 * Properties include:
 * - 'ReturnId': Represents the unique identifier of the return transaction.
 * - 'Approved': A boolean indicating whether the return was approved or not.
 * - 'RefundAmount': Represents the amount refunded to the customer.
 * - 'Message': Represents any relevant message regarding the return transaction.
 */

namespace Pos_system.Models
{
    public class ReturnResult
    {
        public int ReturnId { get; set; }
        public bool Approved { get; set; }
        public decimal RefundAmount { get; set; }
        public string Message { get; set; }
    }
}