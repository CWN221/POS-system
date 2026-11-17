using Npgsql;
using Pos_system.Database;
using Pos_system.Models;
using BCrypt.Net;

namespace Pos_system.Views.Dialogs
{
    public partial class ManagerAuthDialog : Form
    {
        public UserModel? ApprovedManager { get; private set; }

        public ManagerAuthDialog(string reason = "Manager authorization required")
        {
            InitializeComponent();
            subtitleLbl.Text = reason;
            ActiveControl = usernameBox;
            this.KeyPreview = true;
            this.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    ConfirmBtn_Click(null, EventArgs.Empty);
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            };
        }

        private void ConfirmBtn_Click(object? sender, EventArgs e)
        {
            errorLbl.Visible = false;
            errorLbl.Text = "";

            var username = usernameBox.Text.Trim();
            var password = passwordBox.Text;

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowError("Enter the manager's username.");
                usernameBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ShowError("Enter the manager's password.");
                passwordBox.Focus();
                return;
            }

            var manager = LookupAndVerify(username, password);
            if (manager == null)
            {
                ShowError("Invalid credentials or insufficient permissions.");
                passwordBox.Clear();
                passwordBox.Focus();
                return;
            }

            ApprovedManager = manager;
            DialogResult = DialogResult.OK;
            Close();
        }

        private UserModel? LookupAndVerify(string username, string password)
        {
            try
            {
                using var conn = DBConnection.GetConnection();
                const string query = @"
                    SELECT Id, Username, PasswordHash, Role, IsActive, BranchId, MustChangePassword
                    FROM users
                    WHERE Username = @username";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                using var reader = cmd.ExecuteReader();
                if (!reader.Read()) return null;

                var isActive = reader.GetBoolean(reader.GetOrdinal("IsActive"));
                if (!isActive) return null;

                var roleStr = reader.GetString(reader.GetOrdinal("Role"));
                if (!Enum.TryParse<UserRole>(roleStr, out var role)) return null;

                if (role != UserRole.Admin && role != UserRole.Manager)
                    return null;

                var hash = reader.GetString(reader.GetOrdinal("PasswordHash"));
                if (!BCrypt.Net.BCrypt.Verify(password, hash)) return null;

                return new UserModel
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    PasswordHash = "",
                    Role = role,
                    IsActive = isActive,
                    BranchId = reader.GetInt32(reader.GetOrdinal("BranchId")),
                    MustChangePassword = reader.GetBoolean(reader.GetOrdinal("MustChangePassword"))
                };
            }
            catch
            {
                return null;
            }
        }

        private void ShowError(string message)
        {
            errorLbl.Text = message;
            errorLbl.Visible = true;
        }

        private void CancelBtn_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}