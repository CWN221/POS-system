using Pos_system.Core;
using Pos_system.Models;
using Pos_system.Services;
using System.Drawing.Printing;
using System.Globalization;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class Sales : UserControl
    {
        private List<SaleItem> saleItemsList = new();
        private List<Product> productList = new();
        private Sale? lastCompletedSale;
        private readonly PrintDocument printDocument = new();
        private readonly ComboBox paymentMethodComboBox = new();
        private string storeName = "POS Store";

        public Sales()
        {
            InitializeComponent();
            SetupPaymentMethodSelector();
            _ = InitializePageAsync();
        }

        private void SetupPaymentMethodSelector()
        {
            var paymentLabel = new Label
            {
                Text = "Payment Method",
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 73, 94),
                Location = new Point(40, 320),
                AutoSize = true
            };

            paymentMethodComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            paymentMethodComboBox.Font = new Font("Segoe UI", 11F);
            paymentMethodComboBox.Location = new Point(40, 350);
            paymentMethodComboBox.Size = new Size(400, 33);
            paymentMethodComboBox.Items.AddRange(["Cash", "Mpesa", "Card"]);
            paymentMethodComboBox.SelectedIndex = 0;

            Controls.Add(paymentLabel);
            Controls.Add(paymentMethodComboBox);
        }

        private async Task InitializePageAsync()
        {
            var storeService = AppServices.GetRequiredService<StoreSettingsService>();
            var store = await storeService.GetStoreSettings(AppContext.CurrentBranchId);
            if (!string.IsNullOrWhiteSpace(store?.Storename))
                storeName = store.Storename;

            await LoadProducts();
        }

        private async Task LoadProducts()
        {
            var productDAO = AppServices.GetRequiredService<ProductDAO>();
            productList = await productDAO.GetAllProducts();

            productComboBox.Items.Clear();
            foreach (var product in productList.Where(p => p.Stock_quantity > 0))
                productComboBox.Items.Add(product.PName);

            if (productComboBox.Items.Count == 0)
                MessageBox.Show("No products in stock for this branch.", "Inventory", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void completeSaleBtn(object sender, EventArgs e)
        {
            if (saleItemsList.Count == 0)
            {
                MessageBox.Show("No item in the sale.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (paymentMethodComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a payment method.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var newSale = new Sale
                {
                    SaleDate = DateTime.Now,
                    PaymentMethod = paymentMethodComboBox.SelectedItem.ToString()!,
                    Items = new List<SaleItem>(saleItemsList)
                };

                var saleDAO = AppServices.GetRequiredService<CreateSaleDAO>();
                string result = await saleDAO.CreateSale(newSale);

                if (result == "Sale complete")
                {
                    saleItemsList.Clear();
                    saleGridView.Rows.Clear();
                    subTotalLabel.Text = "0.00";
                    vatLabel.Text = "0.00";
                    totalLabel.Text = "0.00";

                    MessageBox.Show("Sale complete", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lastCompletedSale = newSale;
                    printDocument.PrintPage -= PrintReceiptPage;
                    printDocument.PrintPage += PrintReceiptPage;

                    int baseHeight = 300;
                    int itemHeight = lastCompletedSale.Items.Count * 30;
                    printDocument.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 415, baseHeight + itemHeight);
                    printDocument.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);

                    await Task.Run(() =>
                    {
                        using var preview = new PrintPreviewDialog { Document = printDocument };
                        preview.ShowDialog();
                    });

                    await LoadProducts();
                }
                else
                {
                    MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintReceiptPage(object sender, PrintPageEventArgs e)
        {
            if (lastCompletedSale == null || e.Graphics == null) return;

            var culture = new CultureInfo("en-KE");
            CultureInfo.CurrentCulture = culture;

            Graphics g = e.Graphics;
            Font font = new("Courier New", 10);
            float lineHeight = font.GetHeight(g) + 4;
            float x = 10;
            float y = 10;

            g.DrawString(storeName, new Font("Courier New", 12, FontStyle.Bold), Brushes.Black, x, y);
            y += lineHeight * 2;

            g.DrawString("DATE: " + lastCompletedSale.SaleDate.ToString("g"), font, Brushes.Black, x, y);
            y += lineHeight;
            g.DrawString("RECEIPT #: " + lastCompletedSale.Id, font, Brushes.Black, x, y);
            y += lineHeight;
            g.DrawString("----------------------------------------", font, Brushes.Black, x, y);
            y += lineHeight;
            g.DrawString("ITEM           QTY   PRICE   AMOUNT", font, Brushes.Black, x, y);
            y += lineHeight;
            g.DrawString("----------------------------------------", font, Brushes.Black, x, y);
            y += lineHeight;

            foreach (var item in lastCompletedSale.Items)
            {
                string line = $"{item.ProductName.PadRight(14)} {item.Quantity,3}  {item.SellingPrice,6:C}  {item.TotalPrice,6:C}";
                g.DrawString(line, font, Brushes.Black, x, y);
                y += lineHeight;
            }

            g.DrawString("----------------------------------------", font, Brushes.Black, x, y);
            y += lineHeight;

            decimal subtotal = lastCompletedSale.Items.Sum(i => i.Subtotal);
            decimal vat = lastCompletedSale.Items.Sum(i => i.VatAmount);
            decimal total = lastCompletedSale.TotalAmount;

            g.DrawString($"Subtotal: {subtotal,30:C}", font, Brushes.Black, x, y); y += lineHeight;
            g.DrawString($"VAT:      {vat,30:C}", font, Brushes.Black, x, y); y += lineHeight;
            g.DrawString($"Total:    {total,30:C}", new Font("Courier New", 10, FontStyle.Bold), Brushes.Black, x, y); y += lineHeight;
            g.DrawString("----------------------------------------", font, Brushes.Black, x, y);
            y += lineHeight;
            g.DrawString($"Payment Method: {lastCompletedSale.PaymentMethod}", font, Brushes.Black, x, y);
            y += lineHeight * 2;
            g.DrawString("PRICES INCLUSIVE OF VAT WHERE APPLICABLE.", font, Brushes.Black, x, y);
            y += lineHeight * 2;
            g.DrawString("Thank you for shopping with us!", font, Brushes.Black, x, y);
        }

        private async void addToCartBtn_Click(object sender, EventArgs e)
        {
            if (productComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a product.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!int.TryParse(quantityTextBox.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Enter a valid quantity.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string selectedProductName = productComboBox.SelectedItem.ToString()!;
            var selectedProduct = productList.FirstOrDefault(p => p.PName == selectedProductName);

            if (selectedProduct == null)
            {
                MessageBox.Show("Selected product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (quantity > selectedProduct.Stock_quantity)
            {
                MessageBox.Show($"Insufficient stock. Available: {selectedProduct.Stock_quantity}", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existing = saleItemsList.FirstOrDefault(i => i.ProductId == selectedProduct.Idproducts);
            if (existing != null && existing.Quantity + quantity > selectedProduct.Stock_quantity)
            {
                MessageBox.Show($"Cannot add more than available stock ({selectedProduct.Stock_quantity}).", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var vatDAO = AppServices.GetRequiredService<VatDAO>();
            decimal vatPercentage = await vatDAO.GetVatPercentageAsync(selectedProduct.VatCategory);

            if (existing != null)
            {
                existing.Quantity += quantity;
                RefreshGrid();
            }
            else
            {
                var item = new SaleItem
                {
                    ProductId = selectedProduct.Idproducts,
                    ProductName = selectedProduct.PName,
                    Price = selectedProduct.Price,
                    SellingPrice = selectedProduct.SellingPrice,
                    Quantity = quantity,
                    VatCategory = selectedProduct.VatCategory,
                    VatPercentage = vatPercentage
                };
                saleItemsList.Add(item);
                saleGridView.Rows.Add(item.ProductName, item.SellingPrice.ToString("F2"), item.Quantity, item.VatAmount, item.TotalPrice.ToString("F2"));
            }

            UpdateTotals();
            quantityTextBox.Clear();
        }

        private void RefreshGrid()
        {
            saleGridView.Rows.Clear();
            foreach (var item in saleItemsList)
            {
                saleGridView.Rows.Add(item.ProductName, item.SellingPrice.ToString("F2"), item.Quantity, item.VatAmount, item.TotalPrice.ToString("F2"));
            }
        }

        private void UpdateTotals()
        {
            subTotalLabel.Text = saleItemsList.Sum(i => i.Subtotal).ToString("F2");
            vatLabel.Text = saleItemsList.Sum(i => i.VatAmount).ToString("F2");
            totalLabel.Text = saleItemsList.Sum(i => i.TotalPrice).ToString("C");
        }

        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string? selectedProductName = productComboBox.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedProductName)) return;

            var selectedProduct = productList.FirstOrDefault(p => p.PName == selectedProductName);
            if (selectedProduct != null)
            {
                unitPriceTextBox.Text = selectedProduct.SellingPrice.ToString("F2");
            }
        }

        private void backToDashBtn_Click(object sender, EventArgs e)
        {
            var form = FindForm() as DashboardForm;
            form?.LoadPage(new Dashboard());
        }
    }
}
