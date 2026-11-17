using Pos_system.Core;
using Pos_system.Models;
using Pos_system.Services;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class BranchSwitcherPage : UserControl
    {
        private readonly StoreSettingsService _branchService = new();

        public BranchSwitcherPage()
        {
            InitializeComponent();
            BranchesComboBox.DisplayMember = "BranchName";
            BranchesComboBox.ValueMember = "Id";
        }

        private async void BranchSwitcherPage_Load(object sender, EventArgs e)
        {
            await LoadAllowedBranchesAsync();
        }

        public async Task LoadAllowedBranchesAsync()
        {
            try
            {
                var allBranches = await _branchService.GetAllBranches();
                var allowed = allBranches.Where(b => AppContext.CanAccessBranch(b.Id)).ToList();

                BranchesComboBox.DataSource = allowed;

                string currentName = AppContext.CurrentBranch?.BranchName ?? "Not selected";
                lblCurrentBranch.Text = currentName;
                currentBranchHeaderLbl.Text = $"Current: {currentName}";

                userValue.Text = SessionManager.CurrentUser?.Username ?? "—";
                RoleDisplayLbl.Text = SessionManager.CurrentUser?.Role.ToString() ?? "—";

                if (allowed.Count == 0)
                {
                    AllowedBranchesLbl.Text = "No branches available.";
                }
                else
                {
                    AllowedBranchesLbl.Text = string.Join(Environment.NewLine,
                        allowed.Select(b => $"•  {b.BranchName}" + (string.IsNullOrWhiteSpace(b.Location) ? "" : $"  —  {b.Location}")));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading branches: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public Task SwitchBranchAsync(StoreBranch branch)
        {
            AppContext.SetBranch(branch);

            lblCurrentBranch.Text = branch.BranchName;
            currentBranchHeaderLbl.Text = $"Current: {branch.BranchName}";

            MessageBox.Show($"Branch switched to \"{branch.BranchName}\".", "Branch Switched", MessageBoxButtons.OK, MessageBoxIcon.Information);

            return Task.CompletedTask;
        }

        private void BackToDashboardBtn_Click(object sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }

        private async void SwitchBtn_Click(object sender, EventArgs e)
        {
            if (BranchesComboBox.SelectedItem is not StoreBranch selectedBranch)
            {
                MessageBox.Show("Select a branch to switch to.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (selectedBranch.Id == AppContext.CurrentBranchId)
            {
                MessageBox.Show("You are already on this branch.", "No Change", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            await SwitchBranchAsync(selectedBranch);
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            BranchesComboBox.SelectedIndex = -1;
        }
    }
}