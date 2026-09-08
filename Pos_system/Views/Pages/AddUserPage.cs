/*
 * 
 * AddUserPage.cs
 * 
 * Adds new user to the system and assigns roles
 * 
 */

using Pos_system.Core;
using Pos_system.Models;
using Pos_system.Services;
using Pos_system.ViewModels;

namespace Pos_system.Views.Pages
{
    public partial class AddUserPage : UserControl
    {
        public AddUserPage()
        {
            InitializeComponent();
            AuthorizationGuard.RequireManageUsers();
            _ = InitializeBranchComboBox();
        }

        private async Task InitializeBranchComboBox()
        {
            try
            {
                var branches = await AppServices.GetRequiredService<StoreSettingsService>().GetAllBranches();
                BranchComboBox.DataSource = branches;
                BranchComboBox.DisplayMember = "BranchName";
                BranchComboBox.ValueMember = "Id";
                BranchComboBox.SelectedIndex = branches.Count > 0 ? 0 : -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading branches: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            usernameTextBox.Clear();
            roleComboBox.SelectedIndex = -1;
            passwordTextBox.Clear();
            BranchComboBox.SelectedIndex = -1;
            usernameTextBox.Focus();
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            // Validate inputs
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text))
            {
                MessageBox.Show("Please enter a username.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                usernameTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(passwordTextBox.Text))
            {
                MessageBox.Show("Please enter a password.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                passwordTextBox.Focus();
                return;
            }

            if (roleComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please select a role.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                roleComboBox.Focus();
                return;
            }

            if (BranchComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a branch.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BranchComboBox.Focus();
                return;
            }

            try
            {
                // Disable controls during save
                SetControlsEnabled(false);

                var role = (UserRole)Enum.Parse(typeof(UserRole), roleComboBox.SelectedItem.ToString());
                var branchId = Convert.ToInt32(BranchComboBox.SelectedValue);

                var signup = new LoginViewModel
                {
                    Username = usernameTextBox.Text.Trim(),
                    Password = passwordTextBox.Text.Trim(),
                    Role = role,
                    BranchId = branchId
                };

                await signup.UserSignup();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating user: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetControlsEnabled(true);
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            usernameTextBox.Enabled = enabled;
            passwordTextBox.Enabled = enabled;
            roleComboBox.Enabled = enabled;
            BranchComboBox.Enabled = enabled;
            SaveBtn.Enabled = enabled;
            CancelBtn.Enabled = enabled;

            if (!enabled)
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle password visibility
            passwordTextBox.UseSystemPasswordChar = !ShowPasswordCheckBox.Checked;
            passwordTextBox.PasswordChar = ShowPasswordCheckBox.Checked ? '\0' : '●';
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