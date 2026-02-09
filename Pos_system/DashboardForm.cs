
using Pos_system.Views.Pages;

namespace Pos_system
{
    public partial class DashboardForm : Form
    {       

        public DashboardForm()
        {
            InitializeComponent();
        }

        public void LoadPage(UserControl Page)
        {
            panelMain.Controls.Clear();
            panelMain.Controls.Add(Page);
            Page.Dock = DockStyle.Fill;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            // Load the Dashboard page when the form starts
            LoadPage(new Dashboard());
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
