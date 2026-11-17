using Pos_system.Core;
using Pos_system.Models;
using Pos_system.Services;
using Pos_system.ViewModels;

namespace Pos_system.Views.Pages
{
    public partial class UsersPage : UserControl
    {
        private int? _editingUserId;

        public UsersPage()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();
            AuthorizationGuard.RequireManageUsers();

            EnableDoubleBuffering(_usersGrid);

            _roleCombo.Items.AddRange(new object[] { "Admin", "Manager", "Cashier" });
            _statusCombo.SelectedIndex = 0;

            _usersGrid.CellClick += UsersGrid_CellClick;

            Load += (_, _) => BeginInvoke(new Action(async () => await LoadDataAsync()));
        }

        private static void EnableDoubleBuffering(Control control)
        {
            typeof(Control)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)?.SetValue(control, true, null);
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }

        private async Task LoadDataAsync()
        {
            try
            {
                var branches = await AppServices.GetRequiredService<StoreSettingsService>().GetAllBranches();
                _branchCombo.DisplayMember = "BranchName";
                _branchCombo.ValueMember = "Id";
                _branchCombo.DataSource = branches.ToList();

                var users = await AppServices.GetRequiredService<GetUsers>().GetAllUsers();

                _usersGrid.SuspendLayout();

                _usersGrid.DataSource = users;

                if (_usersGrid.Columns.Contains("PasswordHash"))
                    _usersGrid.Columns["PasswordHash"].Visible = false;
                if (_usersGrid.Columns.Contains("Id"))
                    _usersGrid.Columns["Id"].Visible = false;

                _usersGrid.ClearSelection();
                _usersGrid.CurrentCell = null;

                _usersGrid.ResumeLayout();

                userCountLbl.Text = users.Count == 1 ? "1 user" : $"{users.Count} users";

                ResetForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UsersGrid_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (_usersGrid.Rows[e.RowIndex].DataBoundItem is not UserModel user) return;

            _editingUserId = user.Id;
            _formTitle.Text = $"Edit User — {user.Username}";
            _usernameBox.Text = user.Username;
            _passwordBox.Clear();
            _roleCombo.SelectedItem = user.Role.ToString();
            _statusCombo.SelectedItem = user.IsActive ? "Active" : "Inactive";

            if (_branchCombo.DataSource != null)
                _branchCombo.SelectedValue = user.BranchId;

            _saveBtn.Text = "Update User";
            _deleteBtn.Visible = true;
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            ResetForm();
            _usersGrid.ClearSelection();
            _usersGrid.CurrentCell = null;
        }

        private void ResetForm()
        {
            _editingUserId = null;
            _formTitle.Text = "Add New User";
            _usernameBox.Clear();
            _passwordBox.Clear();
            _roleCombo.SelectedIndex = -1;
            _statusCombo.SelectedIndex = 0;

            if (_branchCombo.Items.Count > 0) _branchCombo.SelectedIndex = 0;

            _saveBtn.Text = "Save User";
            _deleteBtn.Visible = false;
        }

        private async void saveBtn_Click(object sender, EventArgs e)
        {
            await SaveUserAsync();
        }

        private async Task SaveUserAsync()
        {
            if (string.IsNullOrWhiteSpace(_usernameBox.Text) || _roleCombo.SelectedItem == null || _branchCombo.SelectedValue == null)
            {
                MessageBox.Show("Fill in username, role and branch.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (_editingUserId == null)
                {
                    if (string.IsNullOrWhiteSpace(_passwordBox.Text))
                    {
                        MessageBox.Show("Password is required for new users.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    await new LoginViewModel
                    {
                        Username = _usernameBox.Text.Trim(),
                        Password = _passwordBox.Text,
                        Role = Enum.Parse<UserRole>(_roleCombo.SelectedItem.ToString()!),
                        BranchId = Convert.ToInt32(_branchCombo.SelectedValue)
                    }.UserSignup();
                }
                else
                {
                    var result = await AppServices.GetRequiredService<UserRepository>().UpdateUser(new UserModel
                    {
                        Id = _editingUserId.Value,
                        Username = _usernameBox.Text.Trim(),
                        Role = Enum.Parse<UserRole>(_roleCombo.SelectedItem.ToString()!),
                        IsActive = _statusCombo.SelectedItem?.ToString() == "Active",
                        BranchId = Convert.ToInt32(_branchCombo.SelectedValue),
                        PasswordHash = string.Empty
                    });
                    MessageBox.Show(result);
                }

                await LoadDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void deleteBtn_Click(object sender, EventArgs e)
        {
            await DeleteUserAsync();
        }

        private async Task DeleteUserAsync()
        {
            if (_editingUserId == null) return;
            if (MessageBox.Show("Delete this user?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            try
            {
                MessageBox.Show(await AppServices.GetRequiredService<UserRepository>().DeleteUser(_editingUserId.Value));
                await LoadDataAsync();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}