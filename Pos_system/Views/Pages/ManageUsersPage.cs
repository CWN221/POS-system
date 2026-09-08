/*
 * 
 * ManageUsersPage.cs
 * 
 * For managing users in the POS e.g staff, manager, to update/delete user records from the system
 * 
 */

using Pos_system.Models;
using Pos_system.Services;


namespace Pos_system.Views.Pages
{
    public partial class ManageUsersPage : UserControl
    {
        public ManageUsersPage()
        {
            InitializeComponent();
            _ = LoadUsers();
        }


        // Load list of users in the system
        private async Task LoadUsers()
        {
            GetUsers getUsers = new();
            var users = await getUsers.GetAllUsers();

            usersDataGridView.AutoGenerateColumns = true;
            usersDataGridView.DataSource = users;

        }


        // Update user records button
        private async void UpdateBtn_Click(object sender, EventArgs e)
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


        // Delete selected user records button
        private async void DeleteBtn_Click(object sender, EventArgs e)
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
    }
}
