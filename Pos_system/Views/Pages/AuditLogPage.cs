using Pos_system.Core;
using Pos_system.Services;
using System.Globalization;
using System.Text;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class AuditLogPage : UserControl
    {
        private readonly AuditLogDAO _dao = new();
        private List<AuditLogRow> _rows = new();

        public AuditLogPage()
        {
            InitializeComponent();

            branchLbl.Text = $"Branch: {AppContext.CurrentBranch?.BranchName ?? "—"}";

            ConfigureGrid();

            dtFrom.Value = DateTime.Today.AddDays(-7);
            dtTo.Value = DateTime.Today;

            Load += AuditLogPage_Load;
        }

        private void ConfigureGrid()
        {
            logsGrid.AutoGenerateColumns = false;
            logsGrid.Columns.Clear();

            logsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedAt",
                DataPropertyName = "CreatedAt",
                HeaderText = "Timestamp",
                Width = 170,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd MMM yyyy HH:mm:ss" }
            });
            logsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                DataPropertyName = "Username",
                HeaderText = "User",
                Width = 140
            });
            logsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Action",
                DataPropertyName = "Action",
                HeaderText = "Action",
                Width = 180
            });
            logsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EntityType",
                DataPropertyName = "EntityType",
                HeaderText = "Entity",
                Width = 120
            });
            logsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "EntityId",
                DataPropertyName = "EntityId",
                HeaderText = "Entity ID",
                Width = 120
            });
            logsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Details",
                DataPropertyName = "Details",
                HeaderText = "Details",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
        }

        private async void AuditLogPage_Load(object? sender, EventArgs e)
        {
            await LoadFilterOptionsAsync();
            await SearchAsync();
        }

        private async Task LoadFilterOptionsAsync()
        {
            try
            {
                var users = _dao.GetActiveUsers(AppContext.CurrentBranchId);
                userCombo.DataSource = users;
                userCombo.DisplayMember = "Username";
                userCombo.ValueMember = "Id";
                userCombo.SelectedIndex = 0;

                var actions = new List<string> { "All Actions" };
                actions.AddRange(_dao.GetDistinctActions(AppContext.CurrentBranchId));
                actionCombo.Items.Clear();
                actionCombo.Items.AddRange(actions.ToArray());
                actionCombo.SelectedIndex = 0;

                var entities = new List<string> { "All Entities" };
                entities.AddRange(_dao.GetDistinctEntityTypes(AppContext.CurrentBranchId));
                entityCombo.Items.Clear();
                entityCombo.Items.AddRange(entities.ToArray());
                entityCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Filter load failed: {ex}");
            }
        }

        private async void SearchBtn_Click(object? sender, EventArgs e)
        {
            await SearchAsync();
        }

        private async Task SearchAsync()
        {
            try
            {
                int? userId = null;
                if (userCombo.SelectedItem is AuditUserOption u && u.Id > 0)
                    userId = u.Id;

                string? action = null;
                if (actionCombo.SelectedItem is string a && a != "All Actions")
                    action = a;

                string? entityType = null;
                if (entityCombo.SelectedItem is string et && et != "All Entities")
                    entityType = et;

                _rows = _dao.Search(
                    dtFrom.Value,
                    dtTo.Value,
                    userId,
                    action,
                    entityType,
                    AppContext.CurrentBranchId);

                logsGrid.DataSource = null;
                logsGrid.DataSource = _rows;

                resultCountLbl.Text = _rows.Count == 1 ? "1 entry" : $"{_rows.Count} entries";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading activity log: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ExportBtn_Click(object? sender, EventArgs e)
        {
            if (_rows.Count == 0)
            {
                MessageBox.Show("Nothing to export. Run a search first.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = $"AuditLog_{DateTime.Now:yyyyMMdd_HHmm}.csv"
            };

            if (sfd.ShowDialog(FindForm()) != DialogResult.OK) return;

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine("Timestamp,User,Action,Entity Type,Entity ID,Details");

                foreach (var row in _rows)
                {
                    sb.AppendLine(string.Join(",",
                        Escape(row.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")),
                        Escape(row.Username),
                        Escape(row.Action),
                        Escape(row.EntityType),
                        Escape(row.EntityId),
                        Escape(row.Details)));
                }

                await File.WriteAllTextAsync(sfd.FileName, sb.ToString(), Encoding.UTF8);
                MessageBox.Show($"Exported to:\n{sfd.FileName}", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value)) return "";
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
                return "\"" + value.Replace("\"", "\"\"") + "\"";
            return value;
        }

        private void BackBtn_Click(object? sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }
    }
}