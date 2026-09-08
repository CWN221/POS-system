
using Pos_system.Views.Pages;

namespace Pos_system
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();
        }

        public void LoadPage(UserControl page)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(page);
            page.Dock = DockStyle.Fill;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            LoadPage(new Dashboard());
        }
    }
}
