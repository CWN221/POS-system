using Pos_system.Core;
using Pos_system.Services;
using System.Globalization;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class StoreCreditPage : UserControl
    {
        private readonly StoreCreditDAO _creditDao = new();

        public StoreCreditPage()
        {
            InitializeComponent();
            branchLbl.Text = $"Branch: {AppContext.CurrentBranch?.BranchName ?? "—"}";
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            historyGrid.AutoGenerateColumns = false;
            historyGrid.Columns.Clear();

            historyGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedAt",
                DataPropertyName = "CreatedAt",
                HeaderText = "Date",
                Width = 170,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd MMM yyyy HH:mm" }
            });
            historyGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MovementType",
                DataPropertyName = "MovementType",
                HeaderText = "Type",
                Width = 120
            });
            historyGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Amount",
                DataPropertyName = "Amount",
                HeaderText = "Amount",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            historyGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ReferenceId",
                DataPropertyName = "ReferenceId",
                HeaderText = "Reference",
                Width = 110
            });
            historyGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                DataPropertyName = "Username",
                HeaderText = "Cashier",
                Width = 140
            });
            historyGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Notes",
                DataPropertyName = "Notes",
                HeaderText = "Notes",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private void BackBtn_Click(object? sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }

        private async void LookupBtn_Click(object? sender, EventArgs e)
        {
            var phone = phoneBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Enter a phone number first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                phoneBox.Focus();
                return;
            }

            try
            {
                var balance = await _creditDao.GetBalanceAsync(phone);
                var history = await _creditDao.GetHistoryAsync(phone);

                var culture = CultureInfo.GetCultureInfo("en-KE");
                balanceValue.Text = balance.ToString("C", culture);
                balanceValue.ForeColor = balance > 0 ? Color.FromArgb(22, 163, 74) : Color.FromArgb(220, 38, 38);

                historyGrid.DataSource = null;
                historyGrid.DataSource = history;
                entryCountLbl.Text = history.Count == 1 ? "1 entry" : $"{history.Count} entries";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lookup failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}