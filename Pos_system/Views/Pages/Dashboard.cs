using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using Pos_system.Core;
using Pos_system.Models;
using Pos_system.Properties;
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
        private Guna.UI2.WinForms.Guna2Button? _dashboardNavBtn;
        private readonly List<Guna.UI2.WinForms.Guna2Button> _navButtons = new();
        private DataGridView? _lowStockGrid;

        public Dashboard()
        {
            InitializeComponent();

            if (!SessionManager.IsLoggedIn)
            {
                MessageBox.Show("You must be logged in to access the dashboard.");
                return;
            }

            _lowStockGrid = this.Controls.Find("lowStockGrid", true).FirstOrDefault() as DataGridView;

            ApplyRoleBasedNavigation();
            ReorganizeSidebarNavigation();
            CheckCloudAvailability();

            WelcomeUserLbl();
            LoadDashboardMetrics();
            LoadLatestTransactions();
            LoadTopProducts();
            _ = InitializeChartAsync();

            AppContext.OnBranchChanged += OnBranchChangedHandler;
            Panel1Sidebar.Resize += (_, _) => PositionLogoutButton();
            Disposed += (_, _) => AppContext.OnBranchChanged -= OnBranchChangedHandler;

            SetActiveNav(_dashboardNavBtn);
        }

        private void ApplyRoleBasedNavigation()
        {
            var user = SessionManager.CurrentUser!;

            InventoryNavBtn.Visible = user.CanManageInventory;
            ReportsNavBtn.Visible = user.CanViewReports;
            SettingsNavBtn.Visible = user.CanManageSettings;
            UsersBtn.Visible = user.CanManageUsers;
            BranchNavBtn.Visible = user.Role == UserRole.Admin;
            SalesHistoryNavBtn.Visible = user.CanViewReports;
            SuppliersNavBtn.Visible = user.CanManageInventory;
            SalesNavBtn.Visible = user.CanMakeSales;
            RefundsNavBtn.Visible = user.CanProcessReturns;
            CloudReportsNavBtn.Visible = user.Role == UserRole.Admin;
            AuditLogNavBtn.Visible = user.Role == UserRole.Admin;
            StoreCreditNavBtn.Visible = user.CanMakeSales;
        }

        private void ReorganizeSidebarNavigation()
        {
            _dashboardNavBtn ??= CreateNavButton("Dashboard", Resources.dashboard_png, DashboardNavBtn_Click);

            _navButtons.Clear();

            var groups = new List<List<Guna.UI2.WinForms.Guna2Button>>
            {
                new() { _dashboardNavBtn, SalesNavBtn },
                new() { InventoryNavBtn, RefundsNavBtn, StoreCreditNavBtn },
                new() { ReportsNavBtn, CloudReportsNavBtn, SalesHistoryNavBtn },
                new() { BranchNavBtn, SuppliersNavBtn, UsersBtn, AuditLogNavBtn, SettingsNavBtn }
            };

            int currentY = 160;
            const int buttonHeight = 48;
            const int buttonGap = 4;
            const int groupGap = 24;

            foreach (var group in groups)
            {
                var visibleButtons = group.Where(b => b.Visible).ToList();
                if (visibleButtons.Count == 0) continue;

                foreach (var btn in visibleButtons)
                {
                    btn.Location = new Point(14, currentY);
                    btn.Size = new Size(202, buttonHeight);
                    _navButtons.Add(btn);
                    currentY += buttonHeight + buttonGap;
                }

                currentY += groupGap;
            }

            PositionLogoutButton();
        }

        private void CheckCloudAvailability()
        {
            if (SessionManager.CurrentUser?.Role != UserRole.Admin) return;

            try
            {
                if (Services.CloudConfig.IsEnabled)
                {
                    bool reachable = Database.CloudConnectionFactory.TestConnection();
                    CloudReportsNavBtn.Enabled = reachable;
                    CloudReportsNavBtn.ForeColor = reachable ? Color.FromArgb(71, 85, 105) : Color.FromArgb(148, 163, 184);
                }
                else
                {
                    CloudReportsNavBtn.Enabled = false;
                    CloudReportsNavBtn.ForeColor = Color.FromArgb(148, 163, 184);
                }
            }
            catch
            {
                CloudReportsNavBtn.Enabled = false;
                CloudReportsNavBtn.ForeColor = Color.FromArgb(148, 163, 184);
            }

            var tip = new ToolTip();
            tip.SetToolTip(CloudReportsNavBtn, CloudReportsNavBtn.Enabled ? "View cross-branch reports from the cloud" : "Cloud database is unreachable");
        }

        private Guna.UI2.WinForms.Guna2Button CreateNavButton(string text, Image? icon, EventHandler onClick)
        {
            var btn = new Guna.UI2.WinForms.Guna2Button
            {
                BorderRadius = 8,
                CheckedState = {
                    FillColor = Color.FromArgb(239, 246, 255),
                    ForeColor = Color.FromArgb(37, 99, 235)
                },
                FillColor = Color.Transparent,
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                ForeColor = Color.FromArgb(71, 85, 105),
                HoverState = {
                    FillColor = Color.FromArgb(248, 250, 252),
                    ForeColor = Color.FromArgb(15, 23, 42)
                },
                Image = icon,
                ImageAlign = HorizontalAlignment.Left,
                ImageOffset = new Point(14, 0),
                MinimumSize = new Size(180, 48),
                Size = new Size(202, 48),
                Text = text,
                TextAlign = HorizontalAlignment.Left,
                TextOffset = new Point(14, 0)
            };
            btn.Click += onClick;
            Panel1Sidebar.Controls.Add(btn);
            return btn;
        }

        private void PositionLogoutButton()
        {
            LogoutNavBtn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LogoutNavBtn.Location = new Point(14, Panel1Sidebar.ClientSize.Height - LogoutNavBtn.Height - 20);
        }

        private void SetActiveNav(Guna.UI2.WinForms.Guna2Button? activeBtn)
        {
            foreach (var btn in _navButtons)
                btn.Checked = btn == activeBtn;
        }

        private string GetBranchText() =>
            AppContext.CurrentBranch != null ? $"Branch: {AppContext.CurrentBranch.BranchName}" : "Branch: Not selected";

        private void OnBranchChangedHandler()
        {
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                BeginInvoke(new Action(OnBranchChangedHandler));
                return;
            }

            branchChipLbl.Text = GetBranchText();

            LoadDashboardMetrics();
            LoadLatestTransactions();
            LoadTopProducts();
            _ = InitializeChartAsync();
        }

        public void WelcomeUserLbl()
        {
            var user = SessionManager.CurrentUser;
            WelcomeLabel.Text = $"Welcome, {user?.Username ?? "User"}";

            userNameLbl.Text = user?.Username ?? "user";
            userInitialLbl.Text = (user?.Username ?? "U").Substring(0, 1).ToUpper();

            branchChipLbl.Text = GetBranchText();
            dateLbl.Text = DateTime.Now.ToString("ddd, dd MMM yyyy");
        }

        public async Task InitializeChartAsync()
        {
            try
            {
                var series = await _chartView.LoadMonthlySalesSeriesAsync();
                var labels = await _chartView.LoadMonthLabelsAsync();

                if (IsDisposed) return;

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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Chart load failed: {ex}");
            }
        }

        private async Task LoadLowStockAlertAsync()
        {
            if (lblLowStockInline == null || !SessionManager.CurrentUser!.CanManageInventory)
                return;

            try
            {
                var productDao = AppServices.GetRequiredService<ProductDAO>();
                var lowStock = await productDao.GetLowStockProducts();

                if (IsDisposed) return;

                lblLowStockInline.Text = lowStock.Count == 0 ? "" : $"{lowStock.Count} product(s) low on stock";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Low stock load failed: {ex}");
            }
        }

        private void LoadLatestTransactions()
        {
            var dao = AppServices.GetRequiredService<TransactionDAO>();
            transactionDAOBindingSource.DataSource = dao.GetTransactionsByCurrentBranch(limit: 10);
            dataGridView1.DataSource = transactionDAOBindingSource;

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.RowHeadersVisible = false;

            if (dataGridView1.Columns.Contains("TotalAmount"))
            {
                dataGridView1.Columns["TotalAmount"].DefaultCellStyle.Format = "C2";
                dataGridView1.Columns["TotalAmount"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            if (dataGridView1.Columns.Contains("TransactionDate"))
            {
                dataGridView1.Columns["TransactionDate"].HeaderText = "Date & Time";
            }

            dataGridView1.ClearSelection();
            dataGridView1.CurrentCell = null;
        }

        private void LoadTopProducts()
        {
            try
            {
                var dao = AppServices.GetRequiredService<TransactionDAO>();
                var top = dao.GetTopSellingProductsToday(limit: 10);

                topProductsList.Items.Clear();
                if (top.Count == 0)
                {
                    topProductsList.Items.Add("No sales yet today");
                    return;
                }

                int rank = 1;
                foreach (var p in top)
                {
                    string formattedText = $"{rank}. {p.Name.PadRight(5)} --> {p.QuantitySold}";
                    topProductsList.Items.Add(formattedText);
                    rank++;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Top products load failed: {ex}");
                topProductsList.Items.Clear();
                topProductsList.Items.Add("Unavailable");
            }
        }

        private void DashboardNavBtn_Click(object? sender, EventArgs e)
        {
            SetActiveNav(_dashboardNavBtn);
            NavigateTo(new Dashboard());
        }

        private void InventoryNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanManageInventory)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetActiveNav(InventoryNavBtn);
            NavigateTo(new Inventory());
        }

        private void SalesNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanMakeSales)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetActiveNav(SalesNavBtn);
            NavigateTo(new Sales());
        }

        private void SalesHistoryNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanViewReports)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetActiveNav(SalesHistoryNavBtn);
            NavigateTo(new SalesHistoryPage());
        }

        private void ReportsNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanViewReports)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetActiveNav(ReportsNavBtn);
            NavigateTo(new ReportsPage());
        }

        private void SuppliersNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanManageInventory)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetActiveNav(SuppliersNavBtn);
            NavigateTo(new SuppliersPage());
        }

        private void RefundsNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanProcessReturns)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetActiveNav(RefundsNavBtn);
            NavigateTo(new ReturnsRefundsPage());
        }

        private void StoreCreditNavBtn_Click(object sender, EventArgs e)
        {
            SetActiveNav(StoreCreditNavBtn);
            NavigateTo(new StoreCreditPage());
        }

        private void SettingsNavBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanManageSettings)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetActiveNav(SettingsNavBtn);
            NavigateTo(new Settings());
        }

        private void CloudReportsNavBtn_Click(object sender, EventArgs e)
        {
            if (SessionManager.CurrentUser?.Role != UserRole.Admin)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetActiveNav(CloudReportsNavBtn);
            NavigateTo(new CloudReportsPage());
        }

        private void AuditLogNavBtn_Click(object sender, EventArgs e)
        {
            if (SessionManager.CurrentUser!.Role != UserRole.Admin)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetActiveNav(AuditLogNavBtn);
            NavigateTo(new AuditLogPage());
        }

        private void UsersBtn_Click(object sender, EventArgs e)
        {
            if (!SessionManager.CurrentUser!.CanManageUsers)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetActiveNav(UsersBtn);
            NavigateTo(new UsersPage());
        }

        private void BranchNavBtn_Click(object sender, EventArgs e)
        {
            if (SessionManager.CurrentUser?.Role != UserRole.Admin)
            {
                MessageBox.Show("Access denied.", "Unauthorized", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SetActiveNav(BranchNavBtn);
            NavigateTo(new BranchSwitcherPage());
        }

        private void NavigateTo(UserControl page)
        {
            var form = FindForm() as DashboardForm;
            form?.LoadPage(page);
        }

        private async void LoadDashboardMetrics()
        {
            try
            {
                var salesDao = AppServices.GetRequiredService<CreateSaleDAO>();
                var productDao = AppServices.GetRequiredService<ProductDAO>();
                var returnsService = AppServices.GetRequiredService<ReturnsService>();
                var culture = CultureInfo.GetCultureInfo("en-KE");

                totalSalesLabel.Text = (await salesDao.GetTodaySales()).ToString("C", culture);
                totalStockLabel.Text = productDao.GetTotalStockQuantity().ToString();
                totalTxnsLbl.Text = (await salesDao.GetTodayTransactionCount()).ToString();

                try
                {
                    decimal refundsToday = await returnsService.GetTodayRefundTotal();
                    totalRefundsLabel.Text = refundsToday.ToString("C", culture);
                }
                catch
                {
                    totalRefundsLabel.Text = "KSh 0.00";
                }

                if (SessionManager.CurrentUser!.CanManageInventory)
                {
                    var lowStock = await productDao.GetLowStockProducts();

                    totalCriticalStockLabel.Text = lowStock.Count.ToString();

                    if (_lowStockGrid != null)
                    {
                        _lowStockGrid.DataSource = null;
                        _lowStockGrid.DataSource = lowStock.Select(p => new
                        {
                            Product = p.PName,
                            Qty = p.Stock_quantity
                        }).ToList();

                        if (_lowStockGrid.Columns.Contains("Qty"))
                        {
                            _lowStockGrid.Columns["Qty"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            _lowStockGrid.Columns["Qty"].DefaultCellStyle.ForeColor = Color.FromArgb(220, 38, 38);
                            _lowStockGrid.Columns["Qty"].DefaultCellStyle.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
                        }
                        if (_lowStockGrid.Columns.Contains("Product"))
                        {
                            _lowStockGrid.Columns["Product"].DefaultCellStyle.Font = new Font("Segoe UI", 10F);
                        }

                        _lowStockGrid.ClearSelection();
                    }

                    await LoadLowStockAlertAsync();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Dashboard metrics load failed: {ex}");
            }
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

            try
            {
                var report = await service.GenerateZReportAsync(branchId, DateTime.Now);
                var store = await storeService.GetStoreSettings(branchId);

                string path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"ZReport_{branchName}_{DateTime.Now:yyyyMMdd}.pdf");

                pdfService.GeneratePdf(report, path, store);
                MessageBox.Show($"Z Report saved to:\n{path}", "Z Report", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Day Already Closed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating Z Report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}