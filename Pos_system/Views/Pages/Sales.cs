using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pos_system.Services;
using Pos_system.Models;
using System.Drawing.Text;
using System.Drawing.Printing;
using Pos_system.Properties;
using System.Globalization;

namespace Pos_system.Views.Pages
{
    public partial class Sales : UserControl
    {

        private List<SaleItem> saleItemsList = new List<SaleItem>();
        private List<Product> productList = new List<Product>();
        private int itemCounter = 0;
        private Sale lastCompletedSale;
        private PrintDocument printDocument = new PrintDocument();

        public Sales()
        {
            InitializeComponent();
            LoadProducts();
        }


        private async Task LoadProducts()
        {
            ProductDAO productDAO = new ProductDAO();
            productList = await productDAO.GetAllProducts();

            productComboBox.Items.Clear();
            productComboBox.Items.AddRange(productList.Select(p => p.PName).ToArray());
        }



        private async void completeSaleBtn(object sender, EventArgs e)
        {


            // Validation
            if (saleItemsList.Count == 0)
            {
                MessageBox.Show("No item in the sale.", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prevent multiple cliks
            //completeSaleBtn.Enabled = false;


            try
            {
                // Sale object
                Sale newSale = new Sale
                {
                    SaleDate = DateTime.Now,
                    //TotalAmount = saleItemsList.Sum(SaleItem => SaleItem.TotalPrice),
                    Items = new List<SaleItem>(saleItemsList)
                };


                CreateSaleDAO saleDAO = new CreateSaleDAO();
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

                    printDocument.PrintPage += new PrintPageEventHandler(PrintReceiptPage);

                    int baseHeight = 300;
                    int itemHeight = lastCompletedSale.Items.Count * 30;
                    int totalHeight = baseHeight + itemHeight;

                    printDocument.DefaultPageSettings.PaperSize = new PaperSize("Receipt", 415, totalHeight);
                    printDocument.DefaultPageSettings.Margins = new Margins(5, 5, 5, 5);


                    // Print directly OR show preview
                    await Task.Run(() =>
                    {
                        PrintPreviewDialog preview = new PrintPreviewDialog
                        {
                            Document = printDocument
                        };
                        preview.ShowDialog();
                    });                    
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

            CultureInfo culture = new CultureInfo("en-KE");
            CultureInfo.CurrentCulture = culture;

            if (lastCompletedSale == null) return;

            Graphics g = e.Graphics;
            Font font = new Font("Courier New", 10);
            float lineHeight = font.GetHeight(g) + 4;
            float x = 10;
            float y = 10;

            // Header
            g.DrawString("GoodMart Supermarket", new Font("Courier New", 12, FontStyle.Bold), Brushes.Black, x, y);
            y += lineHeight * 2;

            g.DrawString("Date: " + lastCompletedSale.SaleDate.ToString("g"), font, Brushes.Black, x, y);
            y += lineHeight;


            g.DrawString("----------------------------------------", font, Brushes.Black, x, y);
            y += lineHeight;

            // Table header
            g.DrawString("Item           Qty   Price   Total", font, Brushes.Black, x, y);
            y += lineHeight;

            g.DrawString("----------------------------------------", font, Brushes.Black, x, y);
            y += lineHeight;

            // Items
            foreach (var item in lastCompletedSale.Items)
            {
                string line = $"{item.ProductName.PadRight(14)} {item.Quantity,3}  {item.Price,6:C}  {item.TotalPrice,6:C}";
                g.DrawString(line, font, Brushes.Black, x, y);
                y += lineHeight;
            }

            g.DrawString("----------------------------------------", font, Brushes.Black, x, y);
            y += lineHeight;

            // Totals
            decimal subtotal = lastCompletedSale.Items.Sum(i => i.Price * i.Quantity);
            decimal vat = lastCompletedSale.Items.Sum(i => i.VatAmount);
            decimal total = lastCompletedSale.TotalAmount;            

            g.DrawString($"Subtotal: {subtotal,30:C}", font, Brushes.Black, x, y); y += lineHeight;
            g.DrawString($"VAT:      {vat,30:C}", font, Brushes.Black, x, y); y += lineHeight;
            g.DrawString($"Total:    {total,30:C}", new Font("Courier New", 10, FontStyle.Bold), Brushes.Black, x, y); y += lineHeight;

            y += lineHeight * 2;
            g.DrawString("Thank you for your shopping with us!", font, Brushes.Black, x, y);
        }


        // Add to cart btn
        private async void addToCartBtn_Click(object sender, EventArgs e)
        {

            // validations
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


            string selectedProductName = productComboBox.SelectedItem?.ToString();
            var selectedProduct = productList.FirstOrDefault(p => p.PName == selectedProductName);

            if (selectedProduct == null)
            {
                MessageBox.Show("Selected product not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            // Get VAT %
            VatDAO vatDAO = new VatDAO();
            decimal vatPercentage = await vatDAO.GetVatPercentageAsync(selectedProduct.VatCategory);

            SaleItem item = new SaleItem
            {
                ProductId = selectedProduct.Idproducts,
                ProductName = selectedProduct.PName,
                Price = selectedProduct.Price,
                Quantity = quantity,
                VatPercentage = vatPercentage
            };

            saleItemsList.Add(item);

            saleGridView.Rows.Add(
                item.ProductName,
                item.Price.ToString("F2"),
                item.Quantity,
                item.VatAmount,
                item.TotalPrice.ToString("F2")
             );

            // --- Update Totals ---
            // SubTotal Label
            decimal subTotal = saleItemsList.Sum(i => i.Price * i.Quantity);
            subTotalLabel.Text = subTotal.ToString("F2");

            // VAT Label
            decimal vatTotal = saleItemsList.Sum(i => i.VatAmount);
            vatLabel.Text = vatTotal.ToString("F2");

            // Total Label
            decimal total = saleItemsList.Sum(i => i.TotalPrice);
            totalLabel.Text = total.ToString("F2");

            quantityTextBox.Clear();
        }



        private void productComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedProductName = productComboBox.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(selectedProductName)) return;

            var selectedProduct = productList.FirstOrDefault(p => p.PName == selectedProductName);

            if (selectedProduct != null)
            {
                unitPriceTextBox.Text = selectedProduct.Price.ToString("F2");
            }
        }



        // Dashboard button
        private void backToDashBtn_Click(object sender, EventArgs e)
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
