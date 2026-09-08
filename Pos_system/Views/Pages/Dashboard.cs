using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using Pos_system.Core;
using Pos_system.Models;
using Pos_system.Services;
using Pos_system.ViewModels;
using SkiaSharp;
using System.Globalization;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class Dashboard : UserControl
    {
        private readonly SalesChartView _chartView = new();
        private Guna.UI2.WinForms.Guna2HtmlLabel? _branchLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel? _lowStockLabel;
        private Guna.UI2.WinForms.Guna2Button? _returnsNavBtn;

        public Dashboard()
        {
            InitializeComponent();

            if (!SessionManager.IsLoggedIn)
            {
                MessageBox.Show("You must be logged in to access the dashboard.");
                return;
            }

            ApplyRoleBasedNavigation();
            SetupBranchIndicator();
            SetupReturnsNavButton();
            SetupLowStockIndicator();

            WelcomeUserLbl();
            LoadTotalSales();
            LoadTotalProducts();
            LoadLatestTransactions();
            _ = InitializeChartAsync();

            AppContext.OnBranchChanged += OnBranchChangedHandler;
            Disposed += (_, _) => AppContext.OnBranchChanged -= OnBranchChangedHandler;
        }

        private void ApplyRoleBasedNavigation()
        {
            var user = SessionManager.CurrentUser!;

            InventoryNavBtn.Visible = user.CanManageInventory;
            ReportsNavBtn.Visible = user.CanViewReports;
            SettingsNavBtn.Visible = user.CanManageSettings;
            UsersBtn.Visible = user.CanManageUsers;
        }

        private void SetupBranchIndicator()
        {
            _branchLabel = new Guna.UI2.WinForms.Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(37, 99, 235),
                Location = new Point(320, 32),
                Size = new Size(400, 30),
                Text = GetBranchText()
            };
            Panel2TopNavbar.Controls.Add(_branchLabel);
        }

        private void SetupLowStockIndicator()
        {
            _lowStockLabel = new Guna.UI2.WinForms.Guna2HtmlLabel
            {
                BackColor = Color.Transparent,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(220, 38, 38),
                Location = new Point(30, 120),
                Size = new Size(600, 25),
                Text = ""
            };
            Controls.Add(_lowStockLabel);
            _ = LoadLowStockAlertAsync();
        }

        private void SetupReturnsNavButton()
        {
            if (!SessionManager.CurrentUser!.CanProcessReturns)
                return;

            _returnsNavBtn = new Guna.UI2.WinForms.Guna2Button
            {
                BorderRadius = 10,
                FillColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(14, 408),
                MinimumSize = new Size(180, 50),
                Size = new Size(200, 50),
                Text = "Returns"
            };
            _returnsNavBtn.Click += ReturnsNavBtn_Click;
            Panel1Sidebar.Controls.Add(_returnsNavBtn);
        }

        private string GetBranchText() =>
            AppContext.CurrentBranch != null
                ? $"Branch: {AppContext.CurrentBranch.BranchName}"
                : "Branch: Not selected";

        private void OnBranchChangedHandler()
        {
            if (_branchLabel != null)
                _branchLabel.Text = GetBranchText();

            LoadTotalSales();
            LoadTotalProducts();
            LoadLatestTransactions();
            _ = InitializeChartAsync();
            _ = LoadLowStockAlertAsync();
        }

        public void WelcomeUserLbl()
        {
            WelcomeLabel.Text = $"Welcome, {SessionManager.CurrentUser?.Username}";
        }

        public async Task InitializeChartAsync()
        {
            var series = await _chartView.LoadMonthlySalesSeriesAsync();
            var labels = await _chartView.LoadMonthLabelsAsync();

            cartesianChart1.Series = series;
            cartesianChart1.XAxes =
            [
                new Axis
                {
                    Labels = labels,
                    LabelsRotation = 0,
                    Name = "Months"
                }
            ];
            cartesianChart1.YAxes =
            [
                new Axis
                {
                    Name = "Sales (KES)",
                    Labeler = value => value.ToString("C0", CultureInfo.CurrentCulture)
                }
            ];
            cartesianChart1.LegendPosition = LegendPosition.Right;
        }

        private async Task LoadLowStockAlertAsync()
        {
            if (_lowStockLabel == null || !SessionManager.CurrentUser!.CanManageInventory)
                return;

            var productDao = AppServices.GetRequiredService<ProductDAO>();
            var lowStock = await productDao.GetLowStockProducts();

            _lowStockLabel.Text = lowStock.Count == 0
                ? ""
                : $"⚠ Low stock alert: {lowStock.Count} product(s) below threshold — {string.Join(", ", lowStock.Take(3).Select(p => p.PName))}{(lowStock.Count > 3 ? "..." : "")}";
        }

        private void LoadLatestTransactions()
        {
            var dao = AppServices.GetRequiredService<TransactionDAO>();
            transactionDAOBindingSource.DataSource = dao.GetTransactionsByCurrentBranch(limit: 10);
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = transactionDAOBindingSource;
        }

        private void InventoryNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanManageInventory)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NavigateTo(new Inventory());
        }

        private void SalesNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanMakeSales)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NavigateTo(new Sales());
        }

        private void ReportsNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanViewReports)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NavigateTo(new ReportsPage());
        }

        private void SettingsNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanManageSettings)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NavigateTo(new Settings());
        }

        private void UsersBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanManageUsers)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NavigateTo(new UsersPage());
        }

        private void BranchNavBtn_Click(object sender, EventArgs e) =>
            NavigateTo(new BranchSwitcherPage());

        private void ReturnsNavBtn_Click(object? sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanProcessReturns)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NavigateTo(new ReturnsRefundsPage());
        }

        private void NavigateTo(UserControl page)
        {
            var form = FindForm() as DashboardForm;
            form?.LoadPage(page);
        }

        private async void LoadTotalSales()
        {
            var salesDao = AppServices.GetRequiredService<CreateSaleDAO>();
            decimal todaySales = await salesDao.GetTodaySales();
            totalSalesLabel.Text = todaySales.ToString("C", CultureInfo.GetCultureInfo("en-KE"));
        }

        private void LoadTotalProducts()
        {
            var productsDao = AppServices.GetRequiredService<ProductDAO>();
            totalStockLabel.Text = productsDao.GetTotalStockQuantity().ToString();
        }

        private void LogoutNavBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SessionManager.Logout();
                FindForm()?.Close();
            }
        }

        private async void DownloadZReportBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanViewReports)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var service = AppServices.GetRequiredService<ZReportService>();
            var pdfService = AppServices.GetRequiredService<ZReportPdfService>();
            var storeService = AppServices.GetRequiredService<StoreSettingsService>();

            int branchId = AppContext.CurrentBranchId;
            var branchName = AppContext.CurrentBranch?.BranchName ?? branchId.ToString();
            var report = await service.GenerateZReport(DateTime.Now, branchId);
            var store = await storeService.GetStoreSettings(branchId);

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"ZReport_{branchName}_{DateTime.Now:yyyyMMdd}.pdf");

            pdfService.GeneratePdf(report, path, store);
            MessageBox.Show($"Z Report saved to:\n{path}", "Z Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
