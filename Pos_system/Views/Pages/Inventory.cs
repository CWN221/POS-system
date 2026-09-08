using DocumentFormat.OpenXml.Bibliography;
using Pos_system.Models;
using Pos_system.Services;
using Pos_system.ViewModels;
using Pos_system.ViewModels.Pages;
using AppContext = Pos_system.Core.AppContext;


namespace Pos_system.Views.Pages
{
    public partial class Inventory : UserControl
    {
        ReportsService reports = new();
        public Inventory()
        {
            InitializeComponent();
            this.Load += Inventory_Load;
            LoadInventoryTab();

            TabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;
        }


        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == viewpTabPage)
            {
                LoadInventoryTab();
            }
        }

        private async void Inventory_Load(object sender, EventArgs e)
        {
            ProductViewModel productViewModel = new();

            await LoadProductCategories();
            await LoadVatCategories();
            await productViewModel.LoadProductsAsync();
        }

        private async void LoadInventoryTab()
        {
            try
            {
                InventoryGridView.AutoGenerateColumns = true;

                var data = await reports.LoadInventoryReportAsync(AppContext.CurrentBranchId);

                InventoryGridView.DataSource = data;

                InventoryGridView.ColumnHeadersVisible = true;

                // Set headers
                InventoryGridView.Columns["ProductName"].HeaderText = "Product Name";
                InventoryGridView.Columns["Category"].HeaderText = "Category";
                InventoryGridView.Columns["StockQuantity"].HeaderText = "Stock Qty";
                InventoryGridView.Columns["CostPrice"].HeaderText = "Cost Price";
                InventoryGridView.Columns["SellingPrice"].HeaderText = "Selling Price";
                InventoryGridView.Columns["StockValue"].HeaderText = "Stock Value";
                InventoryGridView.Columns["StockStatus"].HeaderText = "Status";

                // Formatting
                InventoryGridView.Columns["CostPrice"].DefaultCellStyle.Format = "C2";
                InventoryGridView.Columns["SellingPrice"].DefaultCellStyle.Format = "C2";
                InventoryGridView.Columns["StockValue"].DefaultCellStyle.Format = "C2";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
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
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show($"Error loading VAT categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private async void AddBtn_Click(object sender, EventArgs e)
        {
            // Validate input fields
            if (string.IsNullOrWhiteSpace(pnameTextBox.Text))
            {
                MessageBox.Show("Product name is required");
                return;
            }

            if (pCategoryComboBox.SelectedItem == null)
            {
                MessageBox.Show("Select category");
                return;
            }

            if (vatComboBox.SelectedItem == null)
            {
                MessageBox.Show("Select VAT category");
                return;
            }

            if (!decimal.TryParse(priceTextBox.Text, out decimal price))
            {
                MessageBox.Show("Invalid price");
                return;
            }

            if (!decimal.TryParse(sellingpriceTextBox.Text, out decimal sellingPrice))
            {
                MessageBox.Show("Invalid selling price");
                return;
            }

            if (!int.TryParse(stockqtyTextBox.Text, out int stockQty))
            {
                MessageBox.Show("Invalid stock quantity");
                return;
            }

            if (!int.TryParse(lowstockthresholdTextBox.Text, out int lowStockThreshold))
            {
                MessageBox.Show("Invalid low stock threshold");
                return;
            }

            Product newProduct = new Product
            {
                PName = pnameTextBox.Text,
                SKU = skuTextBox.Text,
                PCategory = pCategoryComboBox.SelectedItem.ToString(),
                VatCategory = vatComboBox.SelectedItem.ToString(),
                Price = price,
                SellingPrice = sellingPrice,
                Stock_quantity = stockQty,
                Low_stock_threshold = lowStockThreshold,
                Stock_status = stockCheckBox.Checked ? "In Stock" : "Out of Stock"
            };

            ProductDAO productDAO = new ProductDAO();
            var result = await productDAO.AddProduct(newProduct);

            if (result.Success)
            {
                MessageBox.Show(result.message);

                pnameTextBox.Clear();
                skuTextBox.Clear();
                pCategoryComboBox.SelectedIndex = -1;
                vatComboBox.SelectedIndex = -1;
                priceTextBox.Clear();
                sellingpriceTextBox.Clear();
                stockqtyTextBox.Clear();
                lowstockthresholdTextBox.Clear();
                stockCheckBox.Checked = false;
            }
            else
            {
                MessageBox.Show("Failed to add product");
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

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            // clear all fields
            pnameTextBox.Clear();
            skuTextBox.Clear();
            pCategoryComboBox.SelectedIndex = -1;
            vatComboBox.SelectedIndex = -1;
            priceTextBox.Clear();
            sellingpriceTextBox.Clear();
            stockqtyTextBox.Clear();
            lowstockthresholdTextBox.Clear();
            stockCheckBox.Checked = false;
        }
    }
}
