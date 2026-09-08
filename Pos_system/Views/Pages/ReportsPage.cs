using Pos_system.Models;
using Pos_system.Services;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using AppContext = Pos_system.Core.AppContext;
using ClosedXML.Excel;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Paragraph = iText.Layout.Element.Paragraph;
using Table = iText.Layout.Element.Table;
using iText.Layout.Borders;
using Image = iText.Layout.Element.Image;
using iText.IO.Image;

namespace Pos_system.Views.Pages
{
    public partial class ReportsPage : UserControl
    {

        private readonly ReportsService reports = new();
        private List<SalesReport> currentReport = new();
        public ReportsPage()
        {
            InitializeComponent();
        }


        // Handle tab change to load data when tab is selected
        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedTab == VatTabPage)
            {
                LoadVatTab();
            }
            else if (TabControl.SelectedTab == BranchTabPage)
            {
                LoadBranchComparisonTab();
            }
        }


        // Export to CSV button click event handler
        private async void CsvExportBtn_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog { Filter = "CSV (*.csv)|*.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await ExportCurrentReportToCsvAsync(sfd.FileName, currentReport);
                    MessageBox.Show("Export successful!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export failed: " + ex.Message);
                }
            }
        }


        // Export to Excel button click event handler
        private async void ExcelExportBtn_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog { Filter = "Excel (*.xlsx)|*.xlsx" };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    await ExportToExcelAsync(sfd.FileName, currentReport);
                    MessageBox.Show("Report exported to Excel successfully.", "Export successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export failed: " + ex.Message);
                }
            }
        }


        // Export to PDF button click event handler
        private async void PdfExportBtn_Click(object sender, EventArgs e)
        {
            using var sfd = new SaveFileDialog { Filter = "PDF (*.pdf)|*.pdf" };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                var store = await new StoreSettingsService().GetStoreSettings(AppContext.CurrentBranchId);
                try
                {
                    await ExportToPdfAsync(sfd.FileName, currentReport, store, dtFrom.Value, dtTo.Value);
                    MessageBox.Show("Report exported to PDF successfully.", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Export failed: " + ex.Message);
                }
            }
        }


        // Run Report button click event handler
        private async void RunReportBtn_Click(object sender, EventArgs e)
        {

            int? branchId = chkAllBranches.Checked ? null : AppContext.CurrentBranchId;

            currentReport = await reports.LoadSalesReportAsync(dtFrom.Value, dtTo.Value, branchId);

            SalesDataGridView.DataSource = null;
            SalesDataGridView.Columns.Clear();
            SalesDataGridView.AutoGenerateColumns = true;
            SalesDataGridView.ColumnHeadersVisible = true;

            // Binding data
            SalesDataGridView.DataSource = currentReport;

            // Headers
            SalesDataGridView.Columns["Date"].HeaderText = "Date";
            SalesDataGridView.Columns["TotalSales"].HeaderText = "Total Sales (KES)";
            SalesDataGridView.Columns["TotalProfit"].HeaderText = "Total Profit (KES)";
            SalesDataGridView.Columns["TotalVat"].HeaderText = "VAT (KES)";
            SalesDataGridView.Columns["TotalTransactions"].HeaderText = "Transactions";

            // Formatting currency to Ksh
            SalesDataGridView.Columns["TotalSales"].DefaultCellStyle.Format = "C2";
            SalesDataGridView.Columns["TotalProfit"].DefaultCellStyle.Format = "C2";
            SalesDataGridView.Columns["TotalVat"].DefaultCellStyle.Format = "C2";

            using (Font font = new Font("Arial", 11, FontStyle.Bold))
            {
                SalesDataGridView.Columns["TotalSales"].DefaultCellStyle.Font = font;
            }
        }


        // Load VAT Tab data
        private async void LoadVatTab()
        {
            int? branchId = chkAllBranches.Checked ? null : AppContext.CurrentBranchId;

            var vatData = await reports.LoadVatReportAsync(dtFrom.Value, dtTo.Value, branchId);

            VatDataGridView.AutoGenerateColumns = true;
            VatDataGridView.DataSource = vatData;

            // Headers
            VatDataGridView.Columns["Date"].HeaderText = "Date";
            VatDataGridView.Columns["VatCategory"].HeaderText = "VAT Category";
            VatDataGridView.Columns["VatPercentage"].HeaderText = "VAT %";
            VatDataGridView.Columns["TaxableAmount"].HeaderText = "Taxable Amount (KES)";
            VatDataGridView.Columns["VatAmount"].HeaderText = "VAT Amount (KES)";

            // Formatting
            VatDataGridView.Columns["TaxableAmount"].DefaultCellStyle.Format = "C2";
            VatDataGridView.Columns["VatAmount"].DefaultCellStyle.Format = "C2";
        }


        // Load Branch Comparison Tab data
        private async void LoadBranchComparisonTab()
        {
            BranchGridView.AutoGenerateColumns = true;
            BranchGridView.DataSource = await reports.LoadBranchComparisonAsync(dtFrom.Value, dtTo.Value);

            // Headers
            BranchGridView.Columns["BranchId"].HeaderText = "Branch Id";
            BranchGridView.Columns["BranchName"].HeaderText = "Branch";
            BranchGridView.Columns["TotalSales"].HeaderText = "Total Sales";
            BranchGridView.Columns["TotalVat"].HeaderText = "Total VAT";
            BranchGridView.Columns["TotalProfit"].HeaderText = "Total Profit";
            BranchGridView.Columns["TotalTransactions"].HeaderText = "Total Transactions";


            BranchGridView.Columns["TotalSales"].DefaultCellStyle.Format = "C2";
            BranchGridView.Columns["TotalVat"].DefaultCellStyle.Format = "C2";
            BranchGridView.Columns["TotalProfit"].DefaultCellStyle.Format = "C2";
        }


        // Exporting report data to CSV
        public async Task ExportCurrentReportToCsvAsync(string filePath, List<SalesReport> data)
        {
            if (data == null || data.Count == 0)
                throw new Exception("No data to export.");

            var sb = new StringBuilder();
            sb.AppendLine("Date,Total Sales,Total VAT,Total Transactions");

            foreach (var row in data)
            {
                sb.AppendLine($"{row.Date:dd-MM-yyyy},{row.TotalSales:F2},{row.TotalVat:F2},{row.TotalTransactions}");
            }

            await File.WriteAllTextAsync(filePath, sb.ToString());
        }


        // Exporting report data to Excel using ClosedXML Library
        public async Task ExportToExcelAsync(string filepath, List<SalesReport> data)
        {
            if (data == null || data.Count == 0)
                throw new Exception("No data to export.");

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sales Report");

            // Header
            worksheet.Cell(1, 1).Value = "Date";
            worksheet.Cell(1, 2).Value = "Total Sales";
            worksheet.Cell(1, 3).Value = "Total VAT";
            worksheet.Cell(1, 4).Value = "Total Transactions";

            // Data
            for (int i = 0; i < data.Count; i++)
            {
                // Start from row 2 since row 1 is the header
                var row = i + 2;

                worksheet.Cell(row, 1).Value = data[i].Date;
                worksheet.Cell(row, 2).Value = data[i].TotalSales;
                worksheet.Cell(row, 3).Value = data[i].TotalVat;
                worksheet.Cell(row, 4).Value = data[i].TotalTransactions;
            }

            // Fromatting
            worksheet.Column(1).Style.DateFormat.Format = "dd-MM-yyyy";
            worksheet.Column(2).Style.NumberFormat.Format = "#,##0.00";
            worksheet.Column(3).Style.NumberFormat.Format = "#,##0.00";

            worksheet.Columns().AdjustToContents();

            await Task.Run(() => workbook.SaveAs(filepath));
        }


        // Export report data to PDF using iText7 Library
        public async Task ExportToPdfAsync(string filepath, List<SalesReport> data, StoreSettings store, DateTime from, DateTime to)
        {
            if (store == null)
            {
                MessageBox.Show("Store settings not found.");
                return;
            }

            if (data == null || data.Count == 0)
                throw new Exception("No data to export.");

            await Task.Run(() =>
            {
                using var writer = new PdfWriter(filepath);
                using var pdf = new PdfDocument(writer);
                using var document = new Document(pdf);

                /* 
                 * 
                 * Logo + header
                 * 
                 */

                var headerTable = new Table(2).UseAllAvailableWidth();

                // Check if  Logo exists
                if (store.LogoPath != null && File.Exists(store.LogoPath))
                {
                    var logo = new Image(ImageDataFactory.Create(store.LogoPath)).ScaleToFit(80, 80);
                    headerTable.AddCell(new Cell().Add(logo).SetBorder(Border.NO_BORDER));
                }
                else
                {
                    headerTable.AddCell(new Cell().SetBorder(Border.NO_BORDER));
                }

                // Store details
                var storeInfo = new Paragraph().Add(store.Storename + "\n").Add(store.Address + "\n").Add(store.Contact + "\n").SetFontSize(10);
                headerTable.AddCell(new Cell().Add(storeInfo).SetBorder(Border.NO_BORDER).SetTextAlignment(TextAlignment.RIGHT));
                document.Add(headerTable);
                document.Add(new Paragraph("\n"));


                /*
                 * 
                 * Reports title
                 * 
                 */
                document.Add(new Paragraph("Sales Report").SetTextAlignment(TextAlignment.CENTER).SetFontSize(14));
                document.Add(new Paragraph($"Period: {from:dd-MM-yyyy} to {to:dd-MM-yyyy}")).SetTextAlignment(TextAlignment.CENTER);
                document.Add(new Paragraph($"Generated on: {DateTime.Now:dd-MM-yyyy HH:mm}"));
                document.Add(new Paragraph("\n"));


                /*
                 * 
                 * Table
                 * 
                 */
                var table = new Table(4).UseAllAvailableWidth();

                // Row Headers
                table.AddHeaderCell("Date");
                table.AddHeaderCell("Total Sales");
                table.AddHeaderCell("Total VAT");
                table.AddHeaderCell("Total Transactions");

                foreach (var row in data)
                {
                    table.AddCell(row.Date.ToString("dd-MM-yyyy"));
                    table.AddCell(row.TotalSales.ToString("N2"));
                    table.AddCell(row.TotalVat.ToString("N2"));
                    table.AddCell(row.TotalTransactions.ToString());
                }

                /* 
                 * 
                 * Totals Row
                 * 
                 */
                var totalSales = data.Sum(x => x.TotalSales);
                var totalVat = data.Sum(x => x.TotalVat);
                var totalTransactions = data.Sum(x => x.TotalTransactions);

                table.AddCell("TOTAL");
                table.AddCell(totalSales.ToString("N2"));
                table.AddCell(totalVat.ToString("N2"));
                table.AddCell(totalTransactions.ToString());

                document.Add(table);
            });

        }

        private void BackToDashboardBtn_Click(object sender, EventArgs e)
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
