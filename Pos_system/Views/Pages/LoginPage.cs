/*
 * LoginPage.cs
 * 
 */
using Pos_system.Core;
using Pos_system.Services;
using Pos_system.ViewModels;

namespace Pos_system.Views.Pages
{
    public partial class LoginPage : Form
    {
        private bool _isLoggingIn = false;

        public LoginPage()
        {
            InitializeComponent();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            this.KeyPreview = true;
            this.KeyDown += LoginPage_KeyDown;
            this.Shown += (s, e) => UsernameTextBox.Focus();
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            await PerformLogin();
        }

        private async Task PerformLogin()
        {
            if (_isLoggingIn) return;

            var username = UsernameTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(username))
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

            try
            {
                _isLoggingIn = true;
                SetControlsEnabled(false);

                var authorization = new Authorization();
                var (isLocked, lockedUntil) = await authorization.IsLockedOutAsync(username);

                if (isLocked && lockedUntil.HasValue)
                {
                    var remaining = lockedUntil.Value - DateTime.Now;
                    var minutes = Math.Max(1, (int)Math.Ceiling(remaining.TotalMinutes));
                    MessageBox.Show($"This account is temporarily locked due to too many failed login attempts.\n\nTry again in {minutes} minute(s).", "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var auth = new LoginViewModel { Username = username, Password = PasswordTextBox.Text};

                bool loggedIn = await auth.UserLogin();

                if (loggedIn)
                {
                    await authorization.ClearFailedLoginsAsync(username);

                    var user = SessionManager.CurrentUser;
                    MessageBox.Show($"Welcome, {user?.Username}!", "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    var (attempts, nowLocked, lockUntil) = await authorization.RecordFailedLoginAsync(username);

                    try
                    {
                        await AppServices.GetRequiredService<AuditService>().LogAsync("LOGIN_FAILED", "user", username, $"Failed attempt #{attempts}");
                    }
                    catch { }

                    if (nowLocked && lockUntil.HasValue)
                    {
                        try
                        {
                            await AppServices.GetRequiredService<AuditService>().LogAsync("LOGIN_LOCKED", "user", username, $"Account locked until {lockUntil.Value:HH:mm}");
                        }
                        catch { }

                        MessageBox.Show($"Too many failed attempts. Account locked for {Authorization.LockoutDuration.TotalMinutes:N0} minutes.", "Account Locked", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        var remaining = Authorization.MaxFailedAttempts - attempts;
                        MessageBox.Show($"Invalid username or password. {remaining} attempt(s) remaining before lockout.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    PasswordTextBox.Text = string.Empty;
                    PasswordTextBox.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"System error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isLoggingIn = false;
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

        private void ShowPasswordCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (ShowPasswordCheckBox.Checked)
            {
                PasswordTextBox.UseSystemPasswordChar = false;
                PasswordTextBox.PasswordChar = '\0';
            }
            else
            {
                PasswordTextBox.UseSystemPasswordChar = true;
                PasswordTextBox.PasswordChar = '●';
            }
        }

        private async void LoginPage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !_isLoggingIn)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                await PerformLogin();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 1;
            const int HTCAPTION = 2;

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
            {
                var pos = PointToClient(new Point(m.LParam.ToInt32()));
                if (pos.Y <= 60)
                    m.Result = (IntPtr)HTCAPTION;
            }
        }
    }
}