/*
 * 
 * Model to display all products in InventoryPage
 * 
 */
using Pos_system.Services;
using Pos_system.Models;
using System.Collections.ObjectModel;

namespace Pos_system.ViewModels.Pages
{
    public class ProductViewModel
    {
        public ObservableCollection<Product> Products { get; set; }

        private readonly ProductDAO productsDAO;

        public ProductViewModel()
        {
            productsDAO = new ProductDAO();
            //Products = new ObservableCollection<Product>((IEnumerable<Product>)productsDAO.GetAllProducts());
        }

        public async Task LoadProductsAsync()
        {
            var productList = await productsDAO.GetAllProducts();
            Products = new ObservableCollection<Product>(productList);
        }
    }
}
