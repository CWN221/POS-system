using Pos_system.Core;
using Pos_system.Services;
using System.Globalization;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class CloudReportsPage : UserControl
    {
        private readonly CloudReportsService _cloudService = new();

        public CloudReportsPage()
        {
            InitializeComponent();

            dtFrom.Value = DateTime.Today.AddDays(-7);
            dtTo.Value = DateTime.Today;

            ConfigureGrids();

            Load += async (_, _) => await InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            if (!CloudConfig.IsEnabled)
            {
                statusLbl.Text = "Cloud sync disabled";
                statusLbl.ForeColor = Color.FromArgb(220, 38, 38);
                DisableActions("Cloud sync is disabled in .env.");
                return;
            }

            bool reachable = Database.CloudConnectionFactory.TestConnection();
            if (!reachable)
            {
                statusLbl.Text = "Cloud unreachable";
                statusLbl.ForeColor = Color.FromArgb(220, 38, 38);
                DisableActions("Cloud database is not reachable.");
                return;
            }

            statusLbl.Text = $"Connected: {CloudConfig.Provider}";
            statusLbl.ForeColor = Color.FromArgb(22, 163, 74);

            await LoadBranchOptionsAsync();
        }

        private void DisableActions(string message)
        {
            runBtn.Enabled = false;
            salesGrid.Enabled = false;
            vatGrid.Enabled = false;
            branchGrid.Enabled = false;
            MessageBox.Show(message, "Cloud Offline", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ConfigureGrids()
        {
            ConfigureGrid(salesGrid, new[]
            {
                ("BranchName", "Branch", 140, null),
                ("Date", "Date", 110, "dd MMM yyyy"),
                ("TransactionCount", "Transactions", 120, "N0"),
                ("TotalSales", "Total Sales (KES)", 160, "C2")
            });

            ConfigureGrid(vatGrid, new[]
            {
                ("BranchName", "Branch", 140, null),
                ("Date", "Date", 110, "dd MMM yyyy"),
                ("VatCategory", "VAT Category", 160, null),
                ("VatPercentage", "VAT %", 80, "N2"),
                ("TaxableAmount", "Taxable (KES)", 140, "C2"),
                ("VatAmount", "VAT (KES)", 140, "C2")
            });

            ConfigureGrid(branchGrid, new[]
            {
                ("BranchName", "Branch", 180, null),
                ("Location", "Location", 180, null),
                ("TransactionCount", "Transactions", 120, "N0"),
                ("TotalSales", "Total Sales", 160, "C2"),
                ("TotalVat", "Total VAT", 140, "C2"),
                ("TotalProfit", "Total Profit", 140, "C2")
            });
        }

        private void ConfigureGrid(Guna.UI2.WinForms.Guna2DataGridView grid,
            (string prop, string header, int width, string? format)[] columns)
        {
            grid.AutoGenerateColumns = false;
            grid.Columns.Clear();

            foreach (var (prop, header, width, format) in columns)
            {
                var col = new DataGridViewTextBoxColumn
                {
                    Name = prop,
                    DataPropertyName = prop,
                    HeaderText = header,
                    Width = width
                };

                if (!string.IsNullOrEmpty(format))
                    col.DefaultCellStyle.Format = format;

                if (prop != "BranchName" && prop != "Location" && prop != "Date" && prop != "VatCategory")
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                grid.Columns.Add(col);
            }
        }

        private async Task LoadBranchOptionsAsync()
        {
            try
            {
                var branches = await _cloudService.LoadBranchesAsync();

                branchCombo.DisplayMember = "Name";
                branchCombo.ValueMember = "Id";
                branchCombo.DataSource = branches;
                branchCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load branches: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackBtn_Click(object sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }

        private async void RunBtn_Click(object sender, EventArgs e)
        {
            if (!CloudConfig.IsEnabled)
            {
                MessageBox.Show("Cloud sync is disabled in .env.", "Cloud Offline",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? branchId = null;
            if (branchCombo.SelectedItem is CloudBranchOption opt && opt.Id.HasValue)
                branchId = opt.Id.Value;

            var from = dtFrom.Value;
            var to = dtTo.Value;

            try
            {
                runBtn.Enabled = false;
                runBtn.Text = "Loading...";

                var culture = CultureInfo.GetCultureInfo("en-KE");

                var kpis = await _cloudService.LoadKpisAsync(from, to, branchId);
                kpiSalesValue.Text = kpis.sales.ToString("C", culture);
                kpiVatValue.Text = kpis.vat.ToString("C", culture);
                kpiTxnValue.Text = kpis.txns.ToString("N0");
                kpiRefundValue.Text = kpis.refunds.ToString("C", culture);

                var sales = await _cloudService.LoadCrossBranchSalesAsync(from, to, branchId);
                salesGrid.DataSource = null;
                salesGrid.DataSource = sales;

                var vat = await _cloudService.LoadCrossBranchVatAsync(from, to, branchId);
                vatGrid.DataSource = null;
                vatGrid.DataSource = vat;

                var branches = await _cloudService.LoadBranchComparisonAsync(from, to);
                branchGrid.DataSource = null;
                branchGrid.DataSource = branches;

                statusLbl.Text = $"Loaded {sales.Count} sales rows · {DateTime.Now:HH:mm}";
                statusLbl.ForeColor = Color.FromArgb(22, 163, 74);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load cloud report: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                runBtn.Enabled = true;
                runBtn.Text = "Create Cloud Report";
            }
        }
    }
}