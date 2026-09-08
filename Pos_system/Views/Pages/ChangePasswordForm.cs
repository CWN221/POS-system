using Pos_system.Core;
using Pos_system.Services;

namespace Pos_system.Views.Pages
{
    public class ChangePasswordForm : Form
    {
        private readonly int _userId;
        private readonly bool _forceChange;
        private readonly TextBox _currentPassword = new() { UseSystemPasswordChar = true, Width = 280 };
        private readonly TextBox _newPassword = new() { UseSystemPasswordChar = true, Width = 280 };
        private readonly TextBox _confirmPassword = new() { UseSystemPasswordChar = true, Width = 280 };

        public ChangePasswordForm(int userId, bool forceChange = false)
        {
            _userId = userId;
            _forceChange = forceChange;

            Text = forceChange ? "Change Default Password" : "Change Password";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(420, 280);

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                ColumnCount = 2,
                RowCount = 5
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            if (forceChange)
            {
                layout.Controls.Add(new Label { Text = "You must set a new password before continuing.", AutoSize = true, ForeColor = Color.DarkRed }, 0, 0);
                layout.SetColumnSpan(layout.GetControlFromPosition(0, 0)!, 2);
            }

            AddRow(layout, 1, "Current Password", _currentPassword);
            AddRow(layout, 2, "New Password", _newPassword);
            AddRow(layout, 3, "Confirm Password", _confirmPassword);

            var saveBtn = new Button { Text = "Save", DialogResult = DialogResult.None, Width = 100 };
            saveBtn.Click += async (_, _) => await SaveAsync();
            layout.Controls.Add(saveBtn, 1, 4);

            Controls.Add(layout);
        }

        private static void AddRow(TableLayoutPanel layout, int row, string label, Control control)
        {
            layout.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, 0, row);
            layout.Controls.Add(control, 1, row);
        }

        private async Task SaveAsync()
        {
            if (_newPassword.Text != _confirmPassword.Text)
            {
                MessageBox.Show("New passwords do not match.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var auth = AppServices.GetRequiredService<Authorization>();
            var result = await auth.ChangePassword(_userId, _currentPassword.Text, _newPassword.Text);

            if (result.Contains("successfully"))
            {
                MessageBox.Show(result, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
