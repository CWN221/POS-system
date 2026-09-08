/*
 * InventoryReport.cs
 * This class represents the structure of an inventory report containing details about products, their stock levels, cost and selling prices & overall stock value.
 * It is used to generate reports that provide insights into the current inventory status of products in the system.
 * 
 * Author: CWN221
 * Properties include:
 * - 'ProductId': Unique identifier for the product
 * - 'ProductName': Name of the product
 * - 'Category': Category to which the product belongs
 * - 'StockQuantity': Current quantity of the product in stock
 * - 'CostPrice': Cost price of the product
 * - 'SellingPrice': Selling price of the product
 * - 'StockValue': Total value of the stock for the product calculated as (StockQuantity * CostPrice)
 * - 'StockStatus': Status indicating whether the product is in stock or out of stock
 */
public class InventoryReport
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = "";
    public string Category { get; set; } = "";
    public int StockQuantity { get; set; }
    public decimal CostPrice { get; set; }
    public decimal SellingPrice { get; set; }
    public decimal StockValue { get; set; }
    public string StockStatus { get; set; } = "";
}