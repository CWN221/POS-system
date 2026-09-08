/*
 * LoginPage.cs
 * 
 */
using Pos_system.Services;
using Pos_system.ViewModels;

namespace Pos_system.Views.Pages
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            // Handle Enter key press
            this.KeyPreview = true;
            this.KeyDown += LoginPage_KeyDown;
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            await PerformLogin();
        }

        private async Task PerformLogin()
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(UsernameTextBox.Text))
                {
                    MessageBox.Show("Please enter your username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    UsernameTextBox.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(PasswordTextBox.Text))
                {
                    MessageBox.Show("Please enter your password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    PasswordTextBox.Focus();
                    return;
                }

                // Disable controls during login
                SetControlsEnabled(false);

                var auth = new LoginViewModel
                {
                    Username = UsernameTextBox.Text.Trim(),
                    Password = PasswordTextBox.Text
                };

                bool loggedIn = await auth.UserLogin();

                if (loggedIn)
                {
                    var successUser = SessionManager.CurrentUser;
                    MessageBox.Show($"Welcome, {successUser?.Username}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    PasswordTextBox.Text = string.Empty;
                    PasswordTextBox.Focus();
                    SetControlsEnabled(true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"System error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetControlsEnabled(true);
            }
        }

        private void SetControlsEnabled(bool enabled)
        {
            UsernameTextBox.Enabled = enabled;
            PasswordTextBox.Enabled = enabled;
            LoginBtn.Enabled = enabled;
            ShowPasswordCheckBox.Enabled = enabled;

            if (!enabled)
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void CheckBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("Please contact your system administrator to reset your password.", "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void LoginPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoginBtn_Click(sender, e);
            }
        }

        private void LoginPage_Load(object sender, EventArgs e)
        {
            // Set focus to username field
            UsernameTextBox.Focus();
        }
        
    }
}