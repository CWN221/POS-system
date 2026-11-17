using Pos_system.Models;
using Pos_system.Services;
using Pos_system.ViewModels.Pages;
using Pos_system.Views.Dialogs;
using Pos_system.Core;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class Inventory : UserControl
    {
        private readonly ReportsService _reports = new();
        private int? _editingProductId;
        private List<InventoryReport> _allInventory = new();

        public Inventory()
        {
            InitializeComponent();

            InventoryGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            InventoryGridView.ReadOnly = true;
            InventoryGridView.CellDoubleClick += InventoryGridView_CellDoubleClick;

            TabControl.SelectedIndexChanged += TabControl_SelectedIndexChanged;

            Load += Inventory_Load;
        }

        private async void Inventory_Load(object? sender, EventArgs e)
        {
            branchLabel.Text = $"Branch: {AppContext.CurrentBranch?.BranchName ?? "—"}";

            await LoadProductCategoriesAsync();
            await LoadVatCategoriesAsync();
            await LoadInventoryTabAsync();
        }

        private void TabControl_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (TabControl.SelectedTab == viewpTabPage)
                _ = LoadInventoryTabAsync();
        }

        private void SearchBox_TextChanged(object? sender, EventArgs e)
        {
            ApplyInventoryFilter();
        }

        private async void InventoryGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (TabControl.SelectedTab != viewpTabPage) return;
            if (InventoryGridView.Rows[e.RowIndex].DataBoundItem is not InventoryReport row) return;

            try
            {
                var product = await AppServices.GetRequiredService<ProductDAO>().GetProductByIdAsync(row.ProductId);
                if (product == null)
                {
                    MessageBox.Show("Product not found in this branch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PopulateProductForm(product);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading product: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateProductForm(Product product)
        {
            _editingProductId = product.Idproducts;
            editModeLabel.Text = $"Editing: {product.PName}";
            AddBtn.Text = "Update Product";
            adjustStockBtn.Visible = true;
            deleteProductBtn.Visible = true;

            pnameTextBox.Text = product.PName;
            skuTextBox.Text = product.SKU ?? string.Empty;
            priceTextBox.Text = product.Price.ToString("F2");
            sellingpriceTextBox.Text = product.SellingPrice.ToString("F2");
            stockqtyTextBox.Text = product.Stock_quantity.ToString();
            lowstockthresholdTextBox.Text = product.Low_stock_threshold.ToString();
            stockCheckBox.Checked = product.Stock_status != "Out of Stock";

            if (pCategoryComboBox.Items.Contains(product.PCategory))
                pCategoryComboBox.SelectedItem = product.PCategory;
            if (vatComboBox.Items.Contains(product.VatCategory))
                vatComboBox.SelectedItem = product.VatCategory;

            TabControl.SelectedTab = tabPage2;
        }

        private void ClearProductForm()
        {
            _editingProductId = null;
            editModeLabel.Text = "Add New Product";
            AddBtn.Text = "Add Product";
            adjustStockBtn.Visible = false;
            deleteProductBtn.Visible = false;

            pnameTextBox.Clear();
            skuTextBox.Clear();
            pCategoryComboBox.SelectedIndex = -1;
            vatComboBox.SelectedIndex = -1;
            priceTextBox.Clear();
            sellingpriceTextBox.Clear();
            stockqtyTextBox.Clear();
            lowstockthresholdTextBox.Clear();
            stockCheckBox.Checked = false;

            InventoryGridView.ClearSelection();
            InventoryGridView.CurrentCell = null;
        }

        private async void AddCategoryBtn_Click(object? sender, EventArgs e)
        {
            using var dialog = new Form
            {
                Text = "New Product Category",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(380, 150),
                MaximizeBox = false,
                MinimizeBox = false
            };

            var label = new Label { Text = "Category name:", Location = new Point(20, 24), AutoSize = true };
            var input = new TextBox { Location = new Point(20, 50), Width = 330 };
            var saveBtn = new Button { Text = "Create", Location = new Point(180, 90), DialogResult = DialogResult.OK };
            var cancelDlgBtn = new Button { Text = "Cancel", Location = new Point(270, 90), DialogResult = DialogResult.Cancel };

            dialog.Controls.AddRange([label, input, saveBtn, cancelDlgBtn]);
            dialog.AcceptButton = saveBtn;
            dialog.CancelButton = cancelDlgBtn;

            if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
                return;

            var categoryName = input.Text.Trim();
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                MessageBox.Show("Please enter a category name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productDao = AppServices.GetRequiredService<ProductDAO>();
            if (productDao.CreateCategory(categoryName, out string message))
            {
                MessageBox.Show(message, "Category Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await LoadProductCategoriesAsync();
                pCategoryComboBox.SelectedItem = categoryName;
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadInventoryTabAsync()
        {
            try
            {
                _allInventory = await _reports.LoadInventoryReportAsync(AppContext.CurrentBranchId);
                ApplyInventoryFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ApplyInventoryFilter()
        {
            if (_allInventory == null) return;

            var query = (searchBox.Text ?? string.Empty).Trim().ToLowerInvariant();

            List<InventoryReport> filtered;
            if (string.IsNullOrEmpty(query))
            {
                filtered = _allInventory;
            }
            else
            {
                filtered = _allInventory.Where(p =>
                    (p.ProductName ?? string.Empty).ToLowerInvariant().Contains(query) ||
                    (p.Category ?? string.Empty).ToLowerInvariant().Contains(query) ||
                    (p.StockStatus ?? string.Empty).ToLowerInvariant().Contains(query)
                ).ToList();
            }

            InventoryGridView.SuspendLayout();
            InventoryGridView.DataSource = null;
            InventoryGridView.AutoGenerateColumns = true;
            InventoryGridView.DataSource = filtered;

            FormatInventoryColumns();

            InventoryGridView.ClearSelection();
            InventoryGridView.CurrentCell = null;
            InventoryGridView.ResumeLayout();

            productCountLbl.Text = filtered.Count == _allInventory.Count
                ? $"{_allInventory.Count} products"
                : $"{filtered.Count} of {_allInventory.Count}";
        }

        private void FormatInventoryColumns()
        {
            if (InventoryGridView.Columns.Contains("ProductId"))
                InventoryGridView.Columns["ProductId"].Visible = false;

            if (InventoryGridView.Columns.Contains("ProductName"))
                InventoryGridView.Columns["ProductName"].HeaderText = "Product Name";
            if (InventoryGridView.Columns.Contains("Category"))
                InventoryGridView.Columns["Category"].HeaderText = "Category";
            if (InventoryGridView.Columns.Contains("StockQuantity"))
                InventoryGridView.Columns["StockQuantity"].HeaderText = "Stock Qty";
            if (InventoryGridView.Columns.Contains("CostPrice"))
            {
                InventoryGridView.Columns["CostPrice"].HeaderText = "Cost Price";
                InventoryGridView.Columns["CostPrice"].DefaultCellStyle.Format = "C2";
            }
            if (InventoryGridView.Columns.Contains("SellingPrice"))
            {
                InventoryGridView.Columns["SellingPrice"].HeaderText = "Selling Price (incl. VAT)";
                InventoryGridView.Columns["SellingPrice"].DefaultCellStyle.Format = "C2";
            }
            if (InventoryGridView.Columns.Contains("StockValue"))
            {
                InventoryGridView.Columns["StockValue"].HeaderText = "Stock Value";
                InventoryGridView.Columns["StockValue"].DefaultCellStyle.Format = "C2";
            }
            if (InventoryGridView.Columns.Contains("StockStatus"))
                InventoryGridView.Columns["StockStatus"].HeaderText = "Status";
        }

        private async Task LoadProductCategoriesAsync()
        {
            try
            {
                var categories = await AppServices.GetRequiredService<ProductDAO>().GetCategoriesAsync();
                pCategoryComboBox.Items.Clear();
                pCategoryComboBox.Items.AddRange(categories.ToArray());

                if (categories.Count == 0)
                    pCategoryComboBox.Items.Add("(No categories — click New)");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadVatCategoriesAsync()
        {
            try
            {
                var vatCategories = await AppServices.GetRequiredService<VatDAO>().GetVatCategoriesAsync();
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
            if (string.IsNullOrWhiteSpace(pnameTextBox.Text))
            {
                MessageBox.Show("Product name is required", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pCategoryComboBox.SelectedItem == null || pCategoryComboBox.SelectedItem.ToString()?.StartsWith("(No categories") == true)
            {
                MessageBox.Show("Select or create a category first.", "Category Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (vatComboBox.SelectedItem == null)
            {
                MessageBox.Show("Select VAT category", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(priceTextBox.Text, out decimal price) ||
                !decimal.TryParse(sellingpriceTextBox.Text, out decimal sellingPrice) ||
                !int.TryParse(stockqtyTextBox.Text, out int stockQty) ||
                !int.TryParse(lowstockthresholdTextBox.Text, out int lowStockThreshold))
            {
                MessageBox.Show("Check numeric fields (cost, selling price, stock, threshold).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var product = new Product
            {
                Idproducts = _editingProductId ?? 0,
                PName = pnameTextBox.Text.Trim(),
                SKU = skuTextBox.Text.Trim(),
                PCategory = pCategoryComboBox.SelectedItem.ToString()!,
                VatCategory = vatComboBox.SelectedItem.ToString()!,
                Price = price,
                SellingPrice = sellingPrice,
                Stock_quantity = stockQty,
                Low_stock_threshold = lowStockThreshold,
                Stock_status = stockCheckBox.Checked ? "In Stock" : "Out of Stock"
            };

            var productDao = AppServices.GetRequiredService<ProductDAO>();

            if (_editingProductId.HasValue)
            {
                // Fetch the current stock from DB to compare
                var existing = await AppServices.GetRequiredService<ProductDAO>().GetProductByIdAsync(_editingProductId.Value);
                if (existing != null && int.TryParse(stockqtyTextBox.Text, out int typedQty) && typedQty != existing.Stock_quantity)
                {
                    MessageBox.Show(
                        "Stock quantity cannot be changed by editing the product. Use the Adjust Stock button instead - it requires a reason and is logged.",
                        "Stock Adjustment Required",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    stockqtyTextBox.Text = existing.Stock_quantity.ToString();
                    return;
                }

                if (productDao.UpdateProduct(product, out string updateMsg))
                {
                    MessageBox.Show(updateMsg, "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearProductForm();
                    await LoadInventoryTabAsync();
                }
                else
                {
                    MessageBox.Show(updateMsg, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                var result = await productDao.AddProduct(product);
                if (result.Success)
                {
                    MessageBox.Show(result.message, "Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearProductForm();
                    await LoadInventoryTabAsync();
                }
                else
                {
                    MessageBox.Show(result.message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void adjustStockBtn_Click(object? sender, EventArgs e)
        {
            if (!_editingProductId.HasValue)
            {
                MessageBox.Show("Save the product first before adjusting stock.", "Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(stockqtyTextBox.Text, out int currentQty))
            {
                MessageBox.Show("Current stock quantity is invalid.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new StockAdjustmentDialog(pnameTextBox.Text, currentQty);
            if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;

            var dao = AppServices.GetRequiredService<ProductDAO>();
            if (dao.AdjustStock(_editingProductId.Value, dialog.Delta, dialog.Reason,
                                AppContext.CurrentBranchId, out string message))
            {
                stockqtyTextBox.Text = (currentQty + dialog.Delta).ToString();
                MessageBox.Show(message, "Stock Adjusted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _ = LoadInventoryTabAsync();
            }
            else
            {
                MessageBox.Show(message, "Adjustment Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void deleteProductBtn_Click(object? sender, EventArgs e)
        {
            if (!_editingProductId.HasValue)
            {
                MessageBox.Show("No product is currently loaded for editing.", "Nothing to Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var productName = pnameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(productName))
                productName = $"Product #{_editingProductId.Value}";

            var confirm = MessageBox.Show(
                $"Delete \"{productName}\"?\n\n" +
                $"Product will be deactivated",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (confirm != DialogResult.Yes) return;

            try
            {
                var productDao = AppServices.GetRequiredService<ProductDAO>();
                var success = productDao.DeleteProduct(_editingProductId.Value, out string message);

                if (success)
                {
                    MessageBox.Show(message, "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearProductForm();
                    await LoadInventoryTabAsync();
                }
                else
                {
                    MessageBox.Show(message, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting product: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backToDashboardBtn_Click(object sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }

        private void cancelBtn_Click(object sender, EventArgs e) => ClearProductForm();

    }
}