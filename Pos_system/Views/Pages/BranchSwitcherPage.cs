using Pos_system.Models;
using Pos_system.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class BranchSwitcherPage : UserControl
    {
        private readonly StoreSettingsService _branchService = new();

        public BranchSwitcherPage()
        {
            InitializeComponent();
        }


        public async Task LoadAllowedBranchesAsync()
        {
            var allBranches = await _branchService.GetAllBranches();
            var allowed = allBranches
                .Where(b => AppContext.CanAccessBranch(b.Id))
                .ToList();
            BranchesComboBox.DataSource = allowed;
            lblCurrentBranch.Text = $"Current Branch: {AppContext.CurrentBranch}";
        }

        
        public Task SwitchBranchAsync(StoreBranch branch)
        {
            AppContext.SetBranch(branch);

            lblCurrentBranch.Text = $"Current Branch: {branch}";
            MessageBox.Show("Branch switched.");
            return Task.CompletedTask;
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

        private async void SwitchBtn_Click(object sender, EventArgs e)
        {
            if (BranchesComboBox.SelectedItem is not StoreBranch selectedBranch)
                return;

            await SwitchBranchAsync(selectedBranch);
        }


        private void CancelBtn_Click(object sender, EventArgs e)
        {
            BranchesComboBox.SelectedIndex = -1;
        }

        private async void BranchSwitcherPage_Load(object sender, EventArgs e)
        {
            await LoadAllowedBranchesAsync();
        }
    }
}
