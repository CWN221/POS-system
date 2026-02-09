using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Pos_system.Models;
using Pos_system.Services;
using Pos_system.ViewModels;
using Pos_system.ViewModels.Pages;

namespace Pos_system.Views.Pages
{
    public partial class Inventory : UserControl
    {

        private readonly ProductViewModel productViewModel;
        public Inventory()
        {
            InitializeComponent();
            LoadProductCategories();
            LoadVatCategories();

            ProductViewModel productVM = new ProductViewModel();
            productViewModelBindingSource1.DataSource = productVM.Products;
            dataGridView1.AutoGenerateColumns = true;
        }

        private async void Inventory_Load(object sender, EventArgs e)
        {
            await LoadProductCategories();
            await productViewModel.LoadProductsAsync();
        }


        private async Task LoadProductCategories()
        {
            try
            {
                ProductDAO productdao = new ProductDAO();
                List<string> categories = await productdao.GetCategoriesAsync();

                // Add categories to the combobox
                pCategoryComboBox.Items.Clear();
                pCategoryComboBox.Items.AddRange(categories.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading catgories", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadVatCategories()
        {
            try
            {
                VatDAO vatDAO = new VatDAO();
                List<string> vatCategories = await vatDAO.GetVatCategoriesAsync();

                // Add VAT categories to the combobox
                vatComboBox.Items.Clear();
                vatComboBox.Items.AddRange(vatCategories.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading VAT categories", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void AddBtn_Click(object sender, EventArgs e)
        {
            // Collecting User input
            string name = pnameTextBox.Text;
            string? category = pCategoryComboBox.SelectedItem?.ToString();
            decimal price = decimal.Parse(priceTextBox.Text);
            string? vatCategory = vatComboBox.SelectedItem?.ToString();
            bool inStock = stockCheckBox.Checked;

            // Object for New product
            Product newProduct = new Product
            {
                PName = name,
                PCategory = category,
                Price = price,
                VatCategory = vatCategory,
                Stock_status = inStock ? "In Stock" : "Out of Stock"
            };

            ProductDAO productDAO = new ProductDAO();
            string message;
            bool result = productDAO.AddProduct(newProduct, out message);

            if (result)
            {
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            } 
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (message == "Product added successfully.")
            {
                // Clear the input fields
                pnameTextBox.Clear();
                pCategoryComboBox.SelectedIndex = -1;
                priceTextBox.Clear();
                vatComboBox.SelectedIndex = -1;
                stockCheckBox.Checked = false;
            }

        }

        private void backToDashboardBtn_Click(object sender, EventArgs e)
        {
            var dashboardPage = new Dashboard();

            var dashboardForm = this.FindForm() as DashboardForm;

            if (dashboardForm != null)
            {
                dashboardForm.LoadPage(dashboardPage);
            }
        }
    }
}
