using Pos_system.Core;
using Pos_system.Models;
using Pos_system.Services;
using Pos_system.Views.Dialogs;
using System.Drawing.Printing;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class SalesHistoryPage : UserControl
    {
        private readonly SalesHistoryDAO _dao = new();
        private List<SalesHistoryRow> _rows = new();
        private StoreSettings? _store;
        private string _fallbackStoreName = "POS Store";

        public SalesHistoryPage()
        {
            InitializeComponent();

            branchLbl.Text = $"Branch: {AppContext.CurrentBranch?.BranchName ?? "—"}";

            ConfigureGrid();

            dtFrom.Value = DateTime.Today;
            dtTo.Value = DateTime.Today;

            Load += SalesHistoryPage_Load;

            searchBox.KeyDown += SearchBox_KeyDown;
        }

        private async void SearchBox_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;

                await SearchAsync();
            }
        }

        private void ConfigureGrid()
        {
            salesGrid.AutoGenerateColumns = false;
            salesGrid.Columns.Clear();

            salesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ReceiptNumber",
                DataPropertyName = "ReceiptNumber",
                HeaderText = "Receipt #",
                Width = 150
            });
            salesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SaleDate",
                DataPropertyName = "SaleDate",
                HeaderText = "Date & Time",
                Width = 200,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd MMM yyyy HH:mm" }
            });
            salesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CashierName",
                DataPropertyName = "CashierName",
                HeaderText = "Cashier",
                Width = 150
            });
            salesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ItemCount",
                DataPropertyName = "ItemCount",
                HeaderText = "Items",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });
            salesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalQuantity",
                DataPropertyName = "TotalQuantity",
                HeaderText = "Units",
                Width = 80,
                DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft }
            });
            salesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PaymentMethod",
                DataPropertyName = "PaymentMethod",
                HeaderText = "Payment",
                Width = 130
            });
            salesGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalAmount",
                DataPropertyName = "TotalAmount",
                HeaderText = "Total",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                }
            });
        }

        private async void SalesHistoryPage_Load(object? sender, EventArgs e)
        {
            await LoadStoreAsync();
            await LoadCashiersAsync();
            LoadPaymentMethods();
            await SearchAsync();
        }

        private async Task LoadStoreAsync()
        {
            try
            {
                _store = await AppServices.GetRequiredService<StoreSettingsService>().GetStoreSettings(AppContext.CurrentBranchId);
                if (!string.IsNullOrWhiteSpace(_store?.Storename))
                    _fallbackStoreName = _store.Storename;
            }
            catch
            {
                _store = null;
            }
        }

        private async Task LoadCashiersAsync()
        {
            try
            {
                var cashiers = await _dao.GetCashiersAsync();
                cashierCombo.DataSource = cashiers;
                cashierCombo.DisplayMember = "Username";
                cashierCombo.ValueMember = "Id";
                cashierCombo.SelectedIndex = 0;
            }
            catch
            {
                cashierCombo.Items.Clear();
            }
        }

        private void LoadPaymentMethods()
        {
            paymentCombo.Items.Clear();
            paymentCombo.Items.AddRange(new object[] { "All Methods", "Cash", "Mpesa", "Card" });
            paymentCombo.SelectedIndex = 0;
        }

        private async void SearchBtn_Click(object? sender, EventArgs e)
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            try
            {
                int? saleId = null;
                var receiptText = searchBox.Text.Trim();
                if (!string.IsNullOrWhiteSpace(receiptText))
                {
                    var digits = new string(receiptText.Where(char.IsDigit).ToArray());
                    if (int.TryParse(digits, out int parsed)) saleId = parsed;
                    else
                    {
                        MessageBox.Show("Receipt number must contain digits.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                int? cashierId = null;
                if (cashierCombo.SelectedItem is CashierOption cashier && cashier.Id > 0)
                    cashierId = cashier.Id;

                string? payment = null;
                if (paymentCombo.SelectedItem is string pm && pm != "All Methods")
                    payment = pm;

                _rows = await _dao.SearchAsync(dtFrom.Value, dtTo.Value, cashierId, payment, saleId);

                salesGrid.DataSource = null;
                salesGrid.DataSource = _rows;

                resultCountLbl.Text = _rows.Count == 1 ? "1 sale" : $"{_rows.Count} sales";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching sales: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ReprintBtn_Click(object? sender, EventArgs e)
        {
            if (salesGrid.CurrentRow?.DataBoundItem is not SalesHistoryRow row)
            {
                MessageBox.Show("Select a sale first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var sale = await _dao.LoadFullSaleAsync(row.SaleId);
                if (sale == null)
                {
                    MessageBox.Show("Could not load the sale.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var printer = new ReceiptPrinter();
                var doc = printer.BuildDocument(sale, _store, _fallbackStoreName);

                using var preview = new PrintPreviewDialog{ Document = doc, Width = 900, Height = 700, StartPosition = FormStartPosition.CenterParent };
                preview.ShowDialog(FindForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reprinting receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void VoidBtn_Click(object? sender, EventArgs e)
        {
            if (salesGrid.CurrentRow?.DataBoundItem is not SalesHistoryRow row)
            {
                MessageBox.Show("Select a sale first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var auth = new ManagerAuthDialog("Sale void requires manager authorization.");
            if (auth.ShowDialog(FindForm()) != DialogResult.OK || auth.ApprovedManager == null)
                return;

            using var reasonDialog = new Form
            {
                Text = "Reason for Void",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(480, 200),
                MaximizeBox = false,
                MinimizeBox = false
            };

            var label = new Label
            {
                Text = "Why is this sale being voided?",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold)
            };
            var input = new TextBox
            {
                Location = new Point(20, 50),
                Width = 440,
                Multiline = true,
                Height = 80,
                Font = new Font("Segoe UI", 10F)
            };
            var okBtn = new Button
            {
                Text = "Void Sale",
                Location = new Point(260, 145),
                Size = new Size(100, 34),
                DialogResult = DialogResult.OK,
                BackColor = Color.FromArgb(220, 38, 38),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            okBtn.FlatAppearance.BorderSize = 0;
            var cancelDlgBtn = new Button
            {
                Text = "Cancel",
                Location = new Point(370, 145),
                Size = new Size(90, 34),
                DialogResult = DialogResult.Cancel
            };

            reasonDialog.Controls.AddRange(new Control[] { label, input, okBtn, cancelDlgBtn });
            reasonDialog.AcceptButton = okBtn;
            reasonDialog.CancelButton = cancelDlgBtn;

            if (reasonDialog.ShowDialog(FindForm()) != DialogResult.OK) return;

            var reason = input.Text.Trim();
            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("A reason is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (success, message) = await new CreateSaleDAO().VoidSaleAsync(row.SaleId, auth.ApprovedManager.Id, reason);

            if (success)
            {
                MessageBox.Show(message, "Voided", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await SearchAsync();
            }
            else
            {
                MessageBox.Show(message, "Void Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackBtn_Click(object? sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }
    }
}