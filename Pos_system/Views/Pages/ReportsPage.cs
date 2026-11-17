using Pos_system.Models;
using Pos_system.Services;
using System.Data;
using System.Globalization;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class ReportsPage : UserControl
    {
        private readonly ReportsService reports = new();
        private List<SalesReport> currentReport = new();

        public ReportsPage()
        {
            InitializeComponent();

            currentBranchLbl.Text = $"Branch: {AppContext.CurrentBranch?.BranchName ?? "—"}";
        }

        private void BackToDashboardBtn_Click(object sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == VatTabPage)
                _ = LoadVatTabAsync();
            else if (TabControl.SelectedTab == BranchTabPage)
                _ = LoadBranchComparisonTabAsync();
        }

        private DataGridView GetActiveReportGrid()
        {
            if (TabControl.SelectedTab == VatTabPage) return VatDataGridView;
            if (TabControl.SelectedTab == BranchTabPage) return BranchGridView;
            return SalesDataGridView;
        }

        private string GetActiveReportName()
        {
            if (TabControl.SelectedTab == VatTabPage) return "VAT Report";
            if (TabControl.SelectedTab == BranchTabPage) return "Branch Comparison";
            return "Sales Report";
        }

        private static DataTable GridToDataTable(DataGridView grid)
        {
            var table = new DataTable();

            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (!col.Visible) continue;

                var propType = col.ValueType ?? typeof(string);
                var dtCol = new DataColumn(col.HeaderText ?? col.Name, propType);
                table.Columns.Add(dtCol);
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;

                var dr = table.NewRow();
                int i = 0;

                foreach (DataGridViewColumn col in grid.Columns)
                {
                    if (!col.Visible) continue;

                    var cellValue = row.Cells[col.Index].Value ?? DBNull.Value;
                    dr[i] = cellValue;
                    i++;
                }

                table.Rows.Add(dr);
            }

            return table;
        }

        private async void CsvExportBtn_Click(object sender, EventArgs e)
        {
            var grid = GetActiveReportGrid();
            if (grid.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to export. Run the report first.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = $"{GetActiveReportName().Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (sfd.ShowDialog(FindForm()) != DialogResult.OK) return;

            try
            {
                var table = GridToDataTable(grid);
                await ReportExportService.ExportDataTableToCsvAsync(table, sfd.FileName);
                MessageBox.Show($"Exported to:\n{sfd.FileName}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed: {ex.Message}", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ExcelExportBtn_Click(object sender, EventArgs e)
        {
            var grid = GetActiveReportGrid();
            if (grid.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to export. Run the report first.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"{GetActiveReportName().Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
            };

            if (sfd.ShowDialog(FindForm()) != DialogResult.OK) return;

            try
            {
                var table = GridToDataTable(grid);
                await ReportExportService.ExportDataTableToExcelAsync(table, sfd.FileName, GetActiveReportName());
                MessageBox.Show($"Exported to:\n{sfd.FileName}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed: {ex.Message}", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void PdfExportBtn_Click(object sender, EventArgs e)
        {
            if (currentReport == null || currentReport.Count == 0)
            {
                MessageBox.Show("There is no data to export. Run the report first.", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "PDF (*.pdf)|*.pdf",
                FileName = $"Sales_Report_{DateTime.Now:yyyyMMdd_HHmm}.pdf"
            };

            if (sfd.ShowDialog(FindForm()) != DialogResult.OK) return;

            var store = await new StoreSettingsService().GetStoreSettings(AppContext.CurrentBranchId);

            try
            {
                await new ZReportPdfService().GenerateSalesReportPdfAsync(sfd.FileName, currentReport, store, dtFrom.Value, dtTo.Value);
                MessageBox.Show($"Exported to:\n{sfd.FileName}", "Export Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed: {ex.Message}", "Export",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ClipboardExportBtn_Click(object sender, EventArgs e)
        {
            var grid = GetActiveReportGrid();
            if (grid.Rows.Count == 0)
            {
                MessageBox.Show("There is no data to copy. Run the report first.", "Copy",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var table = GridToDataTable(grid);
                await ReportExportService.CopyDataTableToClipboardAsync(table);
                MessageBox.Show("Copied. Paste into Excel or any spreadsheet.", "Copied",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Copy failed: {ex.Message}", "Copy",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void RunReportBtn_Click(object sender, EventArgs e)
        {
            try
            {
                int? branchId = chkAllBranches.Checked ? null : AppContext.CurrentBranchId;

                currentReport = await reports.LoadSalesReportAsync(dtFrom.Value, dtTo.Value, branchId);

                SalesDataGridView.DataSource = null;
                SalesDataGridView.Columns.Clear();
                SalesDataGridView.AutoGenerateColumns = true;
                SalesDataGridView.ColumnHeadersVisible = true;
                SalesDataGridView.DataSource = currentReport;

                if (SalesDataGridView.Columns.Contains("Date"))
                    SalesDataGridView.Columns["Date"].HeaderText = "Date";
                if (SalesDataGridView.Columns.Contains("TotalSales"))
                {
                    SalesDataGridView.Columns["TotalSales"].HeaderText = "Total Sales (KES)";
                    SalesDataGridView.Columns["TotalSales"].DefaultCellStyle.Format = "C2";
                }
                if (SalesDataGridView.Columns.Contains("TotalProfit"))
                {
                    SalesDataGridView.Columns["TotalProfit"].HeaderText = "Total Profit (KES)";
                    SalesDataGridView.Columns["TotalProfit"].DefaultCellStyle.Format = "C2";
                }
                if (SalesDataGridView.Columns.Contains("TotalVat"))
                {
                    SalesDataGridView.Columns["TotalVat"].HeaderText = "VAT (KES)";
                    SalesDataGridView.Columns["TotalVat"].DefaultCellStyle.Format = "C2";
                }
                if (SalesDataGridView.Columns.Contains("TotalTransactions"))
                    SalesDataGridView.Columns["TotalTransactions"].HeaderText = "Transactions";

                decimal sales = currentReport.Sum(x => x.TotalSales);
                decimal vat = currentReport.Sum(x => x.TotalVat);
                int txns = currentReport.Sum(x => x.TotalTransactions);

                var culture = CultureInfo.GetCultureInfo("en-KE");
                totalSalesValue.Text = sales.ToString("C", culture);
                totalVatValue.Text = vat.ToString("C", culture);
                txnValue.Text = txns.ToString("N0");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error running report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadVatTabAsync()
        {
            try
            {
                int? branchId = chkAllBranches.Checked ? null : AppContext.CurrentBranchId;
                var vatData = await reports.LoadVatReportAsync(dtFrom.Value, dtTo.Value, branchId);

                VatDataGridView.DataSource = null;
                VatDataGridView.Columns.Clear();
                VatDataGridView.AutoGenerateColumns = true;
                VatDataGridView.DataSource = vatData;

                if (VatDataGridView.Columns.Contains("Date"))
                    VatDataGridView.Columns["Date"].HeaderText = "Date";
                if (VatDataGridView.Columns.Contains("VatCategory"))
                    VatDataGridView.Columns["VatCategory"].HeaderText = "VAT Category";
                if (VatDataGridView.Columns.Contains("VatPercentage"))
                    VatDataGridView.Columns["VatPercentage"].HeaderText = "VAT %";
                if (VatDataGridView.Columns.Contains("TaxableAmount"))
                {
                    VatDataGridView.Columns["TaxableAmount"].HeaderText = "Taxable Amount (KES)";
                    VatDataGridView.Columns["TaxableAmount"].DefaultCellStyle.Format = "C2";
                }
                if (VatDataGridView.Columns.Contains("VatAmount"))
                {
                    VatDataGridView.Columns["VatAmount"].HeaderText = "VAT Amount (KES)";
                    VatDataGridView.Columns["VatAmount"].DefaultCellStyle.Format = "C2";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading VAT report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadBranchComparisonTabAsync()
        {
            try
            {
                BranchGridView.DataSource = null;
                BranchGridView.Columns.Clear();
                BranchGridView.AutoGenerateColumns = true;
                BranchGridView.DataSource = await reports.LoadBranchComparisonAsync(dtFrom.Value, dtTo.Value);

                if (BranchGridView.Columns.Contains("BranchId"))
                    BranchGridView.Columns["BranchId"].HeaderText = "Branch Id";
                if (BranchGridView.Columns.Contains("BranchName"))
                    BranchGridView.Columns["BranchName"].HeaderText = "Branch";
                if (BranchGridView.Columns.Contains("TotalSales"))
                {
                    BranchGridView.Columns["TotalSales"].HeaderText = "Total Sales";
                    BranchGridView.Columns["TotalSales"].DefaultCellStyle.Format = "C2";
                }
                if (BranchGridView.Columns.Contains("TotalVat"))
                {
                    BranchGridView.Columns["TotalVat"].HeaderText = "Total VAT";
                    BranchGridView.Columns["TotalVat"].DefaultCellStyle.Format = "C2";
                }
                if (BranchGridView.Columns.Contains("TotalProfit"))
                {
                    BranchGridView.Columns["TotalProfit"].HeaderText = "Total Profit";
                    BranchGridView.Columns["TotalProfit"].DefaultCellStyle.Format = "C2";
                }
                if (BranchGridView.Columns.Contains("TotalTransactions"))
                    BranchGridView.Columns["TotalTransactions"].HeaderText = "Total Transactions";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading branch comparison: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}