/*
 * The class contains four properties: SaleItemId, ProductId, QtyToReturn & LineAmount. 
 * These properties are used to represent a request to return an item in the POS system.
 * Properties include:
 * - 'SaleItemId': Represents the unique identifier of the sale item being returned
 * - 'ProductId': Represents the unique identifier of the product being returned
 * - 'QtyToReturn': Represents the quantity of the item being returned
 * - 'LineAmount': Represents the total amount for the line item being returned.
 * - 'Restock': A boolean indicating whether the returned items should be restocked.
 * 
 */

namespace Pos_system.Models
{
    public class ReturnItemRequest
    {
        public int SaleItemId { get; set; }
        public int ProductId { get; set; }
        public int QtyToReturn { get; set; }
        public decimal LineAmount { get; set; }
        public bool Restock { get; set; }
    }
}