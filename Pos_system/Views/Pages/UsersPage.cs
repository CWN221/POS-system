using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Pos_system.Core;

namespace Pos_system.Views.Pages
{
    public partial class UsersPage : UserControl
    {
        public UsersPage()
        {
            InitializeComponent();
            AuthorizationGuard.RequireManageUsers();
        }

        private void AddUserBtn_Click(object sender, EventArgs e)
        {
            var page = new AddUserPage();

            var form = this.FindForm() as DashboardForm;
            form?.LoadPage(page);
        }

        private void ManageUsersBtn_Click(object sender, EventArgs e)
        {
            var page = new ManageUsersPage();

            var form = this.FindForm() as DashboardForm;
            form?.LoadPage(page);
        }
    }
}
