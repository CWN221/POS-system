using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.Models;
using Pos_system.Services;
using Pos_system.Views.Dialogs;
using System.Globalization;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class Sales : UserControl
    {
        private List<SaleItem> _cart = new();
        private List<Product> _products = new();
        private Sale? _lastSale;
        private string _storeName = "POS Store";
        private StoreSettings? _store;
        private string _storeCreditPhone = "";
        private decimal _storeCreditApplied = 0m;

        private readonly System.Windows.Forms.Timer _searchDebounceTimer = new();
        private CancellationTokenSource? _renderCts;

        public Sales()
        {
            InitializeComponent();
            _paymentCombo.SelectedIndex = 0;

            _searchDebounceTimer.Interval = 300;
            _searchDebounceTimer.Tick += async (_, _) =>
            {
                _searchDebounceTimer.Stop();
                await LoadProductsAsync();
            };

            _ = InitializeAsync();
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }

        private void _searchBox_TextChanged(object sender, EventArgs e)
        {
            _searchDebounceTimer.Stop();
            _searchDebounceTimer.Start();
        }

        private async void _categoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadProductsAsync();
        }

        private void qtyMinusBtn_Click(object sender, EventArgs e)
        {
            AdjustCartQuantity(-1);
        }

        private void qtyPlusBtn_Click(object sender, EventArgs e)
        {
            AdjustCartQuantity(1);
        }

        private void removeBtn_Click(object sender, EventArgs e)
        {
            if (_cartGrid.CurrentRow == null) return;
            int idx = _cartGrid.CurrentRow.Index;
            if (idx >= 0 && idx < _cart.Count)
            {
                _cart.RemoveAt(idx);
                RefreshCart();
            }
        }

        private async Task InitializeAsync()
        {
            try
            {
                _store = await AppServices.GetRequiredService<StoreSettingsService>().GetStoreSettings(AppContext.CurrentBranchId);

                if (!string.IsNullOrWhiteSpace(_store?.Storename))
                    _storeName = _store.Storename;

                await LoadCategoriesAsync();
                await LoadProductsAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Sales init failed: {ex}");
            }
        }

        private async Task LoadCategoriesAsync()
        {
            try
            {
                var categories = await AppServices.GetRequiredService<ProductDAO>().GetActiveCategoriesAsync();
                _categoryFilter.Items.Clear();
                _categoryFilter.Items.Add("All Categories");
                _categoryFilter.Items.AddRange(categories.ToArray());
                _categoryFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Category load failed: {ex}");
            }
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                _products = await AppServices.GetRequiredService<ProductDAO>().GetAllProducts();
                RenderProducts();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Product load failed: {ex}");
            }
        }

        private async void RenderProducts()
        {
            _renderCts?.Cancel();
            _renderCts = new CancellationTokenSource();
            var token = _renderCts.Token;

            _productPanel.SuspendLayout();
            _productPanel.Controls.Clear();
            _productPanel.ResumeLayout();

            var search = _searchBox.Text.Trim().ToLowerInvariant();
            var cat = _categoryFilter.SelectedItem?.ToString();

            var filtered = _products.Where(p => p.Stock_quantity > 0 &&
                (string.IsNullOrEmpty(search) || p.PName.ToLowerInvariant().Contains(search)) &&
                (cat == null || cat == "All Categories" || p.PCategory == cat)).ToList();

            if (filtered.Count == 0)
            {
                var empty = new Label
                {
                    Text = "No products match your search.",
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.FromArgb(148, 163, 184),
                    AutoSize = false,
                    Size = new Size(_productPanel.ClientSize.Width - 20, 60),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(0, 20, 0, 0)
                };
                _productPanel.Controls.Add(empty);
                return;
            }

            int batchSize = 30;
            for (int i = 0; i < filtered.Count; i += batchSize)
            {
                if (token.IsCancellationRequested) break;

                var batch = filtered.Skip(i).Take(batchSize).ToList();

                try
                {
                    await Task.Delay(10, token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                if (token.IsCancellationRequested) break;

                _productPanel.SuspendLayout();
                foreach (var p in batch)
                {
                    _productPanel.Controls.Add(BuildProductCard(p));
                }
                _productPanel.ResumeLayout();
            }
        }

        private Panel BuildProductCard(Product p)
        {
            var card = new Panel
            {
                Size = new Size(180, 130),
                Margin = new Padding(6),
                BackColor = Color.FromArgb(248, 250, 252),
                Cursor = Cursors.Hand
            };
            card.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using var pen = new Pen(Color.FromArgb(226, 232, 240), 1);
                e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
            };

            var nameLbl = new Label
            {
                Text = p.PName,
                Location = new Point(10, 10),
                Size = new Size(160, 40),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42)
            };

            var priceLbl = new Label
            {
                Text = p.SellingPrice.ToString("C", CultureInfo.GetCultureInfo("en-KE")),
                Location = new Point(10, 52),
                AutoSize = true,
                ForeColor = Color.FromArgb(22, 163, 74),
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold)
            };

            var stockLbl = new Label
            {
                Text = $"Stock: {p.Stock_quantity}",
                Location = new Point(10, 78),
                AutoSize = true,
                ForeColor = Color.FromArgb(100, 116, 139),
                Font = new Font("Segoe UI", 8F)
            };

            var addBtn = new Button
            {
                Text = "+ Add",
                Location = new Point(10, 98),
                Size = new Size(160, 26),
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            addBtn.FlatAppearance.BorderSize = 0;
            addBtn.Click += async (_, _) => await AddProductToCartAsync(p, 1);

            card.Controls.Add(nameLbl);
            card.Controls.Add(priceLbl);
            card.Controls.Add(stockLbl);
            card.Controls.Add(addBtn);

            card.Click += async (_, _) => await AddProductToCartAsync(p, 1);
            nameLbl.Click += async (_, _) => await AddProductToCartAsync(p, 1);
            priceLbl.Click += async (_, _) => await AddProductToCartAsync(p, 1);
            stockLbl.Click += async (_, _) => await AddProductToCartAsync(p, 1);

            return card;
        }

        private async Task AddProductToCartAsync(Product product, int qty)
        {
            var existing = _cart.FirstOrDefault(i => i.ProductId == product.Idproducts);
            int newQty = (existing?.Quantity ?? 0) + qty;
            if (newQty > product.Stock_quantity)
            {
                MessageBox.Show($"Only {product.Stock_quantity} in stock.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var vatPct = await AppServices.GetRequiredService<VatDAO>().GetVatPercentageAsync(product.VatCategory);

            if (existing != null)
            {
                existing.Quantity = newQty;
            }
            else
            {
                _cart.Add(new SaleItem
                {
                    ProductId = product.Idproducts,
                    ProductName = product.PName,
                    Price = product.Price,
                    SellingPrice = product.SellingPrice,
                    Quantity = qty,
                    VatCategory = product.VatCategory,
                    VatPercentage = vatPct
                });
            }

            RefreshCart();
        }

        private void RefreshCart()
        {
            _cartGrid.Rows.Clear();
            foreach (var item in _cart)
            {
                _cartGrid.Rows.Add(
                    item.ProductName,
                    item.Quantity,
                    item.SellingPrice.ToString("C", CultureInfo.GetCultureInfo("en-KE")),
                    item.TotalPrice.ToString("C", CultureInfo.GetCultureInfo("en-KE")));
            }

            _emptyCartLabel.Visible = _cart.Count == 0;
            _cartGrid.Visible = _cart.Count > 0;
            cartCountLbl.Text = _cart.Count == 0 ? "0 items" : $"{_cart.Count} item{(_cart.Count == 1 ? "" : "s")}";

            var culture = CultureInfo.GetCultureInfo("en-KE");
            decimal net = _cart.Sum(i => i.Subtotal);
            decimal vat = _cart.Sum(i => i.VatAmount);
            decimal total = _cart.Sum(i => i.TotalPrice);

            _netLabel.Text = net.ToString("C", culture);
            _vatLabel.Text = vat.ToString("C", culture);
            _totalLabel.Text = total.ToString("C", culture);
        }

        private void AdjustCartQuantity(int delta)
        {
            if (_cartGrid.CurrentRow == null) return;
            int idx = _cartGrid.CurrentRow.Index;
            if (idx < 0 || idx >= _cart.Count) return;

            var item = _cart[idx];
            var product = _products.FirstOrDefault(p => p.Idproducts == item.ProductId);
            int newQty = item.Quantity + delta;

            if (newQty <= 0)
            {
                _cart.RemoveAt(idx);
            }
            else if (product != null && newQty > product.Stock_quantity)
            {
                MessageBox.Show($"Only {product.Stock_quantity} in stock.", "Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                item.Quantity = newQty;
            }

            RefreshCart();
        }

        private async void checkoutBtn_Click(object sender, EventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Cart is empty.", "Sale", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal totalDue = _cart.Sum(i => i.TotalPrice);

            if (totalDue <= 0)
            {
                MessageBox.Show("Sale total is invalid. Please review the cart.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _storeCreditPhone = "";
            _storeCreditApplied = 0m;

            string initialMethod = _paymentCombo.SelectedItem?.ToString() ?? "Cash";

            if (initialMethod == "Store Credit")
            {
                var (ok, phone, amount) = await PromptForStoreCreditAsync(totalDue);
                if (!ok) return;

                _storeCreditPhone = phone;
                _storeCreditApplied = amount;
            }

            using var tender = new TenderDialog(totalDue, initialMethod);
            if (tender.ShowDialog(FindForm()) != DialogResult.OK)
                return;

            if (tender.SelectedMethod == "Store Credit" && _storeCreditApplied <= 0)
            {
                var (ok, phone, amount) = await PromptForStoreCreditAsync(totalDue);
                if (!ok) return;

                _storeCreditPhone = phone;
                _storeCreditApplied = amount;
            }
            else if (tender.SelectedMethod != "Store Credit" && _storeCreditApplied > 0)
            {
                _storeCreditPhone = "";
                _storeCreditApplied = 0m;
            }

            decimal remainingDue = totalDue - _storeCreditApplied;

            string mpesaStatus = "None";
            if (tender.SelectedMethod == "Mpesa" && tender.MpesaInitiated
                && !string.IsNullOrWhiteSpace(tender.MpesaCheckoutRequestId))
            {
                using var conn = DBConnection.GetConnection();
                const string sql = "SELECT status FROM mpesa_stk_push_log WHERE checkout_request_id = @id";
                using var cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", tender.MpesaCheckoutRequestId);
                var s = await cmd.ExecuteScalarAsync();
                mpesaStatus = s?.ToString() ?? "Pending";
            }

            var sale = new Sale
            {
                SaleDate = DateTime.Now,
                PaymentMethod = tender.SelectedMethod,
                Items = new List<SaleItem>(_cart),
                AmountTendered = tender.AmountTendered,
                ChangeGiven = tender.ChangeGiven,
                StoreCreditUsed = _storeCreditApplied,
                CustomerPhone = string.IsNullOrWhiteSpace(_storeCreditPhone) ? null : _storeCreditPhone,
                MpesaStatus = mpesaStatus,
                Payments = new List<Payment>
        {
            new Payment
            {
                Method = tender.SelectedMethod,
                Amount = remainingDue,
                Reference = tender.Reference,
                MpesaCheckoutRequestId = tender.MpesaInitiated ? tender.MpesaCheckoutRequestId : null
            }
        }
            };

            var result = await AppServices.GetRequiredService<CreateSaleDAO>().CreateSale(sale);
            if (result != "Sale complete")
            {
                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (tender.MpesaInitiated && !string.IsNullOrWhiteSpace(tender.MpesaCheckoutRequestId))
            {
                try
                {
                    using var conn = DBConnection.GetConnection();
                    const string sql = "UPDATE mpesa_stk_push_log SET sale_id = @sid WHERE checkout_request_id = @id";
                    using var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@sid", sale.Id);
                    cmd.Parameters.AddWithValue("@id", tender.MpesaCheckoutRequestId);
                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"mpesa_stk_push_log backfill failed: {ex.Message}");
                }
            }

            if (_storeCreditApplied > 0 && !string.IsNullOrWhiteSpace(_storeCreditPhone))
            {
                try
                {
                    var creditDao = AppServices.GetRequiredService<StoreCreditDAO>();
                    var (ok, msg) = await creditDao.RedeemCreditAsync(
                        _storeCreditPhone, _storeCreditApplied, sale.Id);

                    if (!ok)
                    {
                        MessageBox.Show($"Sale completed but store credit redemption failed: {msg}\n\nPlease reconcile manually.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sale completed but store credit redemption threw an error: {ex.Message}\n\nPlease reconcile manually.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            _lastSale = sale;

            var printer = new ReceiptPrinter();
            var doc = printer.BuildDocument(sale, _store, _storeName);
            using var preview = new PrintPreviewDialog { Document = doc, Width = 900, Height = 700, StartPosition = FormStartPosition.CenterParent };
            preview.ShowDialog(FindForm());

            _cart.Clear();
            _storeCreditPhone = "";
            _storeCreditApplied = 0m;
            RefreshCart();

            await LoadProductsAsync();
        }

        private async Task<(bool ok, string phone, decimal amount)> PromptForStoreCreditAsync(decimal saleTotal)
        {
            using var dialog = new Form
            {
                Text = "Store Credit",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(500, 300),
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.White,
                Font = new Font("Segoe UI", 10F)
            };

            var headingLbl = new Label
            {
                Text = "Store Credit Redeem",
                Location = new Point(20, 16),
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42)
            };

            var promptLbl = new Label
            {
                Text = "Customer Phone Number",
                Location = new Point(20, 56),
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(51, 65, 85)
            };

            var phoneBox = new TextBox
            {
                Location = new Point(20, 82),
                Width = 420,
                Font = new Font("Segoe UI", 11F)
            };

            var lookupBtn = new Button
            {
                Text = "Check Balance",
                Location = new Point(20, 120),
                Size = new Size(160, 36),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(239, 246, 255),
                ForeColor = Color.FromArgb(37, 99, 235),
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            lookupBtn.FlatAppearance.BorderColor = Color.FromArgb(37, 99, 235);

            var balanceCaption = new Label
            {
                Text = "Available Balance",
                Location = new Point(20, 172),
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 116, 139)
            };

            var balanceLbl = new Label
            {
                Text = "—",
                Location = new Point(20, 192),
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(22, 163, 74)
            };

            var saleTotalLbl = new Label
            {
                Text = $"Sale Total: {saleTotal:C}",
                Location = new Point(240, 196),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(100, 116, 139)
            };

            var okBtn = new Button
            {
                Text = "Apply Credit",
                Location = new Point(270, 246),
                Size = new Size(120, 36),
                BackColor = Color.FromArgb(37, 99, 235),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                DialogResult = DialogResult.OK,
                Enabled = false,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            okBtn.FlatAppearance.BorderSize = 0;

            var cancelBtn = new Button
            {
                Text = "Cancel",
                Location = new Point(396, 246),
                Size = new Size(80, 36),
                DialogResult = DialogResult.Cancel,
                FlatStyle = FlatStyle.Flat
            };

            decimal currentBalance = 0m;

            lookupBtn.Click += async (_, _) =>
            {
                var phone = phoneBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(phone))
                {
                    MessageBox.Show("Enter the phone number first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    lookupBtn.Enabled = false;
                    lookupBtn.Text = "Checking...";

                    var creditDao = AppServices.GetRequiredService<StoreCreditDAO>();
                    currentBalance = await creditDao.GetBalanceAsync(phone);

                    balanceLbl.Text = currentBalance.ToString("C", CultureInfo.GetCultureInfo("en-KE"));
                    balanceLbl.ForeColor = currentBalance > 0 ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);

                    if (currentBalance <= 0)
                    {
                        okBtn.Enabled = false;
                        MessageBox.Show("No store credit available for this phone number.", "No Balance", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (currentBalance < saleTotal)
                    {
                        okBtn.Enabled = false;
                        MessageBox.Show(
                            $"Customer has {currentBalance:C} in store credit, but the sale is {saleTotal:C}.\n\n" +
                            "Partial store credit + cash payment is not currently supported. " +
                            "Reduce the cart or use a different payment method.",
                            "Insufficient Credit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        okBtn.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lookup failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    lookupBtn.Enabled = true;
                    lookupBtn.Text = "Check Balance";
                }
            };

            dialog.Controls.AddRange(new Control[] {
                headingLbl, promptLbl, phoneBox, lookupBtn,
                balanceCaption, balanceLbl, saleTotalLbl,
                okBtn, cancelBtn
            });
            dialog.AcceptButton = lookupBtn;
            dialog.CancelButton = cancelBtn;

            if (dialog.ShowDialog(FindForm()) != DialogResult.OK)
                return (false, "", 0m);

            if (currentBalance < saleTotal)
            {
                MessageBox.Show("Customer does not have enough store credit for this sale.", "Insufficient Credit", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return (false, "", 0m);
            }

            return (true, phoneBox.Text.Trim(), saleTotal);
        }
    }
}