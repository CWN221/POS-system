using Pos_system.Core;
using Pos_system.logs;
using Pos_system.Models;
using Pos_system.Services;
using Pos_system.ViewModels;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class Settings : UserControl
    {
        private ListBox? _logsListBox;

        public Settings()
        {
            InitializeComponent();
            AuthorizationGuard.RequireManageSettings();

            SetupLogsViewer();
            _ = LoadBranchesForSettings();
            _ = LoadUsers();
        }

        private async Task LoadUsers()
        {
            var users = await AppServices.GetRequiredService<GetUsers>().GetAllUsers();
            usersDataGridView.AutoGenerateColumns = true;
            usersDataGridView.DataSource = users;
        }

        private void SetupLogsViewer()
        {
            _logsListBox = new ListBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Consolas", 9F),
                IntegralHeight = false
            };
            Logs.Controls.Clear();
            Logs.Controls.Add(_logsListBox);
            RefreshLogs();
        }

        private void RefreshLogs()
        {
            if (_logsListBox == null) return;
            _logsListBox.Items.Clear();
            foreach (var line in Logger.ReadRecentLogs())
                _logsListBox.Items.Add(line);
        }

        private async Task LoadBranchesForSettings()
        {
            var service = AppServices.GetRequiredService<StoreSettingsService>();
            var branches = await service.GetAllBranches();

            if (branches.Count == 0)
            {
                MessageBox.Show("No branches found. Please add a branch first.");
                return;
            }

            BranchComboBox.DataSource = branches;
            var current = branches.FirstOrDefault(b => b.Id == AppContext.CurrentBranchId) ?? branches.First();
            BranchComboBox.SelectedItem = current;
            await LoadStoreDetails(current.Id);
        }

        private void BackToDashboardBtn_Click(object sender, EventArgs e)
        {
            var form = FindForm() as DashboardForm;
            form?.LoadPage(new Dashboard());
        }

        private async void StoreSaveBtn_Click(object sender, EventArgs e)
        {
            if (BranchComboBox.SelectedItem is not StoreBranch branch) return;

            var storeSettings = new StoreSettings
            {
                BranchId = branch.Id,
                Storename = storenameTextBox.Text,
                Address = addressTextBox.Text,
                Contact = contactTextBox.Text,
                Contact2 = Contact2TextBox.Text,
                Email = EmailTextBox.Text,
                BusinessPin = PinTextBox.Text
            };

            var storeSettingsService = AppServices.GetRequiredService<StoreSettingsService>();
            bool success = await storeSettingsService.SaveStoreSettings(storeSettings);

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

        private void StoreCancelBtn_Click(object sender, EventArgs e)
        {
            storenameTextBox.Clear();
            addressTextBox.Clear();
            contactTextBox.Clear();
            Contact2TextBox.Clear();
            EmailTextBox.Clear();
            PinTextBox.Clear();
        }

        private async void SaveBtn_Click(object sender, EventArgs e)
        {
            if (BranchComboBox.SelectedItem is not StoreBranch branch)
            {
                MessageBox.Show("Select a branch for the new user.");
                return;
            }

            var signup = new LoginViewModel
            {
                Username = usernameTextBox.Text.Trim(),
                Password = passwordTextBox.Text.Trim(),
                Role = (UserRole)Enum.Parse(typeof(UserRole), roleComboBox.SelectedItem!.ToString()!),
                BranchId = branch.Id
            };

            if (string.IsNullOrWhiteSpace(signup.Username) || string.IsNullOrWhiteSpace(signup.Password))
            {
                MessageBox.Show("Please enter a username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            await signup.UserSignup();
            usernameTextBox.Clear();
            passwordTextBox.Clear();
            roleComboBox.SelectedIndex = -1;
            await LoadUsers();
        }

        private async void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.CurrentRow?.DataBoundItem is not UserModel selectedUser) return;

            if (MessageBox.Show($"Delete user '{selectedUser.Username}'?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var result = await AppServices.GetRequiredService<UserRepository>().DeleteUser(selectedUser.Id);
            MessageBox.Show(result);
        }

        private async void UpdateBtn_Click(object sender, EventArgs e)
        {
            if (usersDataGridView.CurrentRow?.DataBoundItem is not UserModel selectedUser) return;

            if (MessageBox.Show($"Update user '{selectedUser.Username}'?", "Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            var result = await AppServices.GetRequiredService<UserRepository>().UpdateUser(selectedUser);
            MessageBox.Show(result);
        }

        private async void BranchSaveBtn_Click(object sender, EventArgs e)
        {
            var branch = new StoreBranch
            {
                BranchName = BranchNameTextBox.Text,
                Location = BranchLocationTextBox.Text
            };

            bool success = await AppServices.GetRequiredService<StoreSettingsService>().SaveBranch(branch);

            if (success)
            {
                MessageBox.Show("Branch saved successfully.");
                BranchNameTextBox.Clear();
                BranchLocationTextBox.Clear();
                await LoadBranchesForSettings();
            }
            else
            {
                MessageBox.Show("Failed to save branch.");
            }
        }

        private void BranchClearBtn_Click(object sender, EventArgs e)
        {
            BranchNameTextBox.Clear();
            BranchLocationTextBox.Clear();
        }

        private async void BranchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BranchComboBox.SelectedItem is StoreBranch selectedBranch)
                await LoadStoreDetails(selectedBranch.Id);
        }

        private async Task LoadStoreDetails(int branchId)
        {
            var store = await AppServices.GetRequiredService<StoreSettingsService>().GetStoreSettings(branchId);

            if (store != null)
            {
                StorenameLabelOut.Text = store.Storename;
                AddressLabelOut.Text = store.Address;
                ContactLabelOut.Text = store.Contact;
                Contact2LabelOut.Text = store.Contact2;
                EmailLabelOut.Text = store.Email;
                PinLabelOut.Text = store.BusinessPin;

                storenameTextBox.Text = store.Storename;
                addressTextBox.Text = store.Address;
                contactTextBox.Text = store.Contact;
                Contact2TextBox.Text = store.Contact2;
                EmailTextBox.Text = store.Email;
                PinTextBox.Text = store.BusinessPin;
            }
            else
            {
                StorenameLabelOut.Text = AddressLabelOut.Text = ContactLabelOut.Text = "";
                Contact2LabelOut.Text = EmailLabelOut.Text = PinLabelOut.Text = "";
            }
        }
    }
}
