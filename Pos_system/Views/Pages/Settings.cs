using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pos_system.Services;
using Pos_system.Models;

namespace Pos_system.Views.Pages
{
    public partial class Settings : UserControl
    {
        public Settings()
        {
            InitializeComponent();
            _ = LoadUsers();
        }


        // Load list of users in the system
        private async Task LoadUsers()
        {
            GetUsers getUsers = new GetUsers();
            var users = await getUsers.GetAllUsers();

            usersDataGridView.AutoGenerateColumns = true;
            usersDataGridView.DataSource = users;
            
        }


        // Back to dashboard button click event handler to navigate back to the dashboard page
        private void backToDashboardBtn_Click(object sender, EventArgs e)
        {
            var dashboardPage = new Dashboard();

            var dashboardForm = this.FindForm() as DashboardForm;
            if (dashboardForm != null)
            {
                dashboardForm.LoadPage(dashboardPage);
            }
        }


        // Save button click event handler to save store settings
        private async void storeSaveBtn_Click(object sender, EventArgs e)
        {
            StoreSettings storeSettings = new StoreSettings
            {
                Storename = storenameTextBox.Text,
                Address = addressTextBox.Text,
                Contact = contactTextBox.Text

            };

            StoreSettingsService storeSettingsService = new StoreSettingsService();

            bool success = await storeSettingsService.SaveStoreSettings(storeSettings);

            if (success)
            {
                MessageBox.Show("Store settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Failed to save store settings. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Cancel button click event handler to clear the input fields
        private void storeCancelBtn_Click(object sender, EventArgs e)
        {
            storenameTextBox.Clear();
            addressTextBox.Clear();
            contactTextBox.Clear();
        }



        /*
         * 
         * Save button click event handler to save user settings. 
         * New user is created and added to the system by calling the Signup method of the Authorization service.
         * Handled from Authorization service (Authorization.cs)
         * 
         */
        private async void saveBtn_Click(object sender, EventArgs e)
        {
            string username = usernameTextBox.Text.Trim();
            string password = passwordTextBox.Text;
            string role = roleComboBox.SelectedItem?.ToString() ?? "User";

            // validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter a username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Authorization auth = new Authorization();
            string result = await auth.Signup(username, password, role);

            MessageBox.Show(result, "User Registration", MessageBoxButtons.OK, result.Contains("Successful") ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            
            if (result.Contains("Successful"))
            {
                usernameTextBox.Clear();
                passwordTextBox.Clear();
                roleComboBox.SelectedIndex = -1;
                
                await LoadUsers();
            }
        }



        // Delete button click event handler to delete selected user
        private async void deleteBtn_Click(object sender, EventArgs e)
        {
            UserModel? selectedUser = usersDataGridView.CurrentRow?.DataBoundItem as UserModel;

            if (selectedUser == null) return;

            // Show confirmation dialog before deleting user
            var confirm = MessageBox.Show($"Are you sure you want to delete user '{selectedUser.Username}'?", "Confirm delete", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            UserRepository userRepository = new UserRepository();
            string result = await userRepository.DeleteUser(selectedUser.Id);

            MessageBox.Show(result, "Delete User", MessageBoxButtons.OK, result.Contains("successfully") ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            await LoadUsers();
        }



        // Update button click event handler to update selected user
        private async void updateBtn_Click(object sender, EventArgs e)
        {
            
            UserModel? selectedUser = usersDataGridView.CurrentRow?.DataBoundItem as UserModel;

            if (selectedUser == null) return;

            // show confirmation dialog before updating user
            var confirm = MessageBox.Show($"Are you sure you want to update user '{selectedUser.Username}'?", "Confirm update", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            UserRepository userRepository = new UserRepository();
            string result = await userRepository.UpdateUser(selectedUser);
            MessageBox.Show(result, "Update User", MessageBoxButtons.OK, result.Contains("updated") ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            await LoadUsers();

        }
    }
}
