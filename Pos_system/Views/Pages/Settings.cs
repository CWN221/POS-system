using Pos_system.Core;
using Pos_system.Models;
using Pos_system.Services;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class Settings : UserControl
    {
        private int? _editingBranchId;
        private bool _suppressBranchComboEvent = false;

        private readonly AuditLogDAO _auditDao = new();
        private List<AuditLogRow> _auditRows = new();

        public Settings()
        {
            InitializeComponent();
            AuthorizationGuard.RequireManageSettings();

            settingsTabControl.TabPages.Remove(Users);

            BranchGridView.CellClick += BranchGridView_CellClick;

            ConfigureAuditGrid();

            Load += async (_, _) =>
            {
                await LoadBranchesForSettings();
                await LoadBranchGridAsync();
                await LoadAuditFilterOptionsAsync();
                await SearchAuditAsync();
            };
        }
    

        private async Task LoadBranchesForSettings()
        {
            try
            {
                var service = AppServices.GetRequiredService<StoreSettingsService>();
                var branches = await service.GetAllBranches();

                if (branches.Count == 0)
                {
                    BranchComboBox.DataSource = null;
                    return;
                }

                _suppressBranchComboEvent = true;

                BranchComboBox.DisplayMember = "BranchName";
                BranchComboBox.ValueMember = "Id";
                BranchComboBox.DataSource = branches.ToList();

                var current = branches.FirstOrDefault(b => b.Id == AppContext.CurrentBranchId) ?? branches.First();
                BranchComboBox.SelectedItem = current;

                _suppressBranchComboEvent = false;

                await LoadStoreDetails(current.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading branches: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadStoreDetails(int branchId)
        {
            try
            {
                var store = await AppServices.GetRequiredService<StoreSettingsService>().GetStoreSettings(branchId);

                if (store != null)
                {
                    storeNameValue.Text = string.IsNullOrWhiteSpace(store.Storename) ? "—" : store.Storename;
                    storeAddressValue.Text = string.IsNullOrWhiteSpace(store.Address) ? "—" : store.Address;
                    storeContactValue.Text = string.IsNullOrWhiteSpace(store.Contact) ? "—" : store.Contact;
                    storeContact2Value.Text = string.IsNullOrWhiteSpace(store.Contact2) ? "—" : store.Contact2;
                    storeEmailValue.Text = string.IsNullOrWhiteSpace(store.Email) ? "—" : store.Email;
                    storePinValue.Text = string.IsNullOrWhiteSpace(store.BusinessPin) ? "—" : store.BusinessPin;

                    storenameTextBox.Text = store.Storename ?? string.Empty;
                    addressTextBox.Text = store.Address ?? string.Empty;
                    contactTextBox.Text = store.Contact ?? string.Empty;
                    Contact2TextBox.Text = store.Contact2 ?? string.Empty;
                    EmailTextBox.Text = store.Email ?? string.Empty;
                    PinTextBox.Text = store.BusinessPin ?? string.Empty;
                }
                else
                {
                    storeNameValue.Text = storeAddressValue.Text = "—";
                    storeContactValue.Text = storeContact2Value.Text = "—";
                    storeEmailValue.Text = storePinValue.Text = "—";

                    storenameTextBox.Clear();
                    addressTextBox.Clear();
                    contactTextBox.Clear();
                    Contact2TextBox.Clear();
                    EmailTextBox.Clear();
                    PinTextBox.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading store details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackToDashboardBtn_Click(object sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }

        private async void StoreSaveBtn_Click(object sender, EventArgs e)
        {
            if (BranchComboBox.SelectedItem is not StoreBranch branch) return;

            var storeSettings = new StoreSettings
            {
                BranchId = branch.Id,
                Storename = storenameTextBox.Text.Trim(),
                Address = addressTextBox.Text.Trim(),
                Contact = contactTextBox.Text.Trim(),
                Contact2 = Contact2TextBox.Text.Trim(),
                Email = EmailTextBox.Text.Trim(),
                BusinessPin = PinTextBox.Text.Trim()
            };

            try
            {
                bool success = await AppServices.GetRequiredService<StoreSettingsService>()
                    .SaveStoreSettings(storeSettings);

                if (success)
                {
                    await LoadStoreDetails(branch.Id);
                    MessageBox.Show("Store settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to save store settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving store settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StoreCancelBtn_Click(object sender, EventArgs e)
        {
            if (BranchComboBox.SelectedItem is StoreBranch branch)
                _ = LoadStoreDetails(branch.Id);
        }

        private async void BranchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressBranchComboEvent) return;

            if (BranchComboBox.SelectedItem is StoreBranch selectedBranch)
                await LoadStoreDetails(selectedBranch.Id);
        }
 

        private async Task LoadBranchGridAsync()
        {
            try
            {
                var branches = await AppServices.GetRequiredService<StoreSettingsService>().GetAllBranches();

                BranchGridView.DataSource = null;
                BranchGridView.Columns.Clear();
                BranchGridView.AutoGenerateColumns = true;
                BranchGridView.DataSource = branches;

                if (BranchGridView.Columns.Contains("Id"))
                    BranchGridView.Columns["Id"].HeaderText = "ID";
                if (BranchGridView.Columns.Contains("BranchName"))
                    BranchGridView.Columns["BranchName"].HeaderText = "Branch Name";
                if (BranchGridView.Columns.Contains("Location"))
                    BranchGridView.Columns["Location"].HeaderText = "Location";

                BranchGridView.ClearSelection();
                BranchGridView.CurrentCell = null;

                branchCountLbl.Text = branches.Count == 1 ? "1 branch" : $"{branches.Count} branches";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading branch list: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BranchGridView_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (BranchGridView.Rows[e.RowIndex].DataBoundItem is not StoreBranch branch) return;

            _editingBranchId = branch.Id;
            branchHeaderLbl.Text = $"Editing: {branch.BranchName}";
            BranchNameTextBox.Text = branch.BranchName ?? string.Empty;
            BranchLocationTextBox.Text = branch.Location ?? string.Empty;

            BranchSaveBtn.Text = "Update Branch";
            BranchDeleteBtn.Visible = true;
            BranchNameTextBox.Focus();
        }

        private async void BranchSaveBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(BranchNameTextBox.Text))
            {
                MessageBox.Show("Please enter a branch name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BranchNameTextBox.Focus();
                return;
            }

            var branch = new StoreBranch
            {
                Id = _editingBranchId ?? 0,
                BranchName = BranchNameTextBox.Text.Trim(),
                Location = BranchLocationTextBox.Text.Trim()
            };

            try
            {
                var service = AppServices.GetRequiredService<StoreSettingsService>();
                bool success;

                if (_editingBranchId.HasValue)
                    success = await service.UpdateBranch(branch);
                else
                    success = await service.SaveBranch(branch);

                if (success)
                {
                    MessageBox.Show(_editingBranchId.HasValue ? "Branch updated successfully." : "Branch saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearBranchForm();
                    await LoadBranchGridAsync();
                    await LoadBranchesForSettings();
                }
                else
                {
                    MessageBox.Show("Failed to save branch.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving branch: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BranchClearBtn_Click(object sender, EventArgs e)
        {
            ClearBranchForm();
        }

        private void ClearBranchForm()
        {
            _editingBranchId = null;
            branchHeaderLbl.Text = "Add New Branch";
            BranchNameTextBox.Clear();
            BranchLocationTextBox.Clear();

            BranchSaveBtn.Text = "Save Branch";
            BranchDeleteBtn.Visible = false;

            BranchGridView.ClearSelection();
            BranchGridView.CurrentCell = null;
        }

        private async void BranchDeleteBtn_Click(object sender, EventArgs e)
        {
            if (!_editingBranchId.HasValue)
            {
                MessageBox.Show("Select a branch from the list first.", "No Branch Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int branchId = _editingBranchId.Value;
            string branchName = BranchNameTextBox.Text.Trim();

            if (MessageBox.Show(
                    $"Delete branch \"{branchName}\"?\n\nThis cannot be undone. If this branch has any sales, users or products, the delete will be blocked.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                var service = AppServices.GetRequiredService<StoreSettingsService>();
                var result = await service.DeleteBranch(branchId);

                if (result.success)
                {
                    MessageBox.Show(result.message, "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearBranchForm();
                    await LoadBranchGridAsync();
                    await LoadBranchesForSettings();
                }
                else
                {
                    MessageBox.Show(result.message, "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting branch: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        

        private void ConfigureAuditGrid()
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

        private async Task LoadAuditFilterOptionsAsync()
        {
            try
            {
                var users = _auditDao.GetActiveUsers(AppContext.CurrentBranchId);
                logsUserCombo.DataSource = users;
                logsUserCombo.DisplayMember = "Username";
                logsUserCombo.ValueMember = "Id";
                logsUserCombo.SelectedIndex = 0;

                var actions = new List<string> { "All Actions" };
                actions.AddRange(_auditDao.GetDistinctActions(AppContext.CurrentBranchId));
                logsActionCombo.Items.Clear();
                logsActionCombo.Items.AddRange(actions.ToArray());
                logsActionCombo.SelectedIndex = 0;

                var entities = new List<string> { "All Entities" };
                entities.AddRange(_auditDao.GetDistinctEntityTypes(AppContext.CurrentBranchId));
                logsEntityCombo.Items.Clear();
                logsEntityCombo.Items.AddRange(entities.ToArray());
                logsEntityCombo.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Audit filter load failed: {ex}");
            }
        }

        private async void RefreshLogsBtn_Click(object sender, EventArgs e)
        {
            await SearchAuditAsync();
        }

        private async void LogsSearchBtn_Click(object sender, EventArgs e)
        {
            await SearchAuditAsync();
        }

        private async Task SearchAuditAsync()
        {
            try
            {
                int? userId = null;
                if (logsUserCombo.SelectedItem is AuditUserOption u && u.Id > 0)
                    userId = u.Id;

                string? action = null;
                if (logsActionCombo.SelectedItem is string a && a != "All Actions")
                    action = a;

                string? entityType = null;
                if (logsEntityCombo.SelectedItem is string et && et != "All Entities")
                    entityType = et;

                var from = logsDtFrom.Value;
                var to = logsDtTo.Value;

                var rows = await Task.Run(() => _auditDao.Search(
                    from, to, userId, action, entityType, AppContext.CurrentBranchId));

                _auditRows = rows;

                logsGrid.DataSource = null;
                logsGrid.DataSource = _auditRows;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading audit log: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void ExportLogsBtn_Click(object sender, EventArgs e)
        {
            if (_auditRows.Count == 0)
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

                foreach (var row in _auditRows)
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
    }
}