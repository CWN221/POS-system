/*
 * 
 * Model for products. This model is used in ProductDAO.cs file
 * 
 */
namespace Pos_system.Models
{
    public class Product
    {
        public int Idproducts { get; set; }
        public required string PName { get; set; }
        public string? SKU { get; set; }
        public required string PCategory { get; set; }
        public required decimal Price { get; set; }
        public required decimal SellingPrice { get; set; }
        public required string VatCategory { get; set; }
        public string? Stock_status { get; set; }
        public int Stock_quantity { get; set; }
        public int Low_stock_threshold { get; set; }
    }
}
