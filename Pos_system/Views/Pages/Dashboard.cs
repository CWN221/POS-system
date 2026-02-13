using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pos_system.Services;
using Pos_system.Models;
using System.Transactions;
using System.Globalization;

namespace Pos_system.Views.Pages
{
    public partial class Dashboard : UserControl
    {
        public Dashboard()
        {
            InitializeComponent();
            LoadTotalSales();
            LoadTotalProducts();
            LoadLatestTransactions();
        }

        private void LoadLatestTransactions()
        {
            TransactionDAO dao = new TransactionDAO();
            List<SaleTransaction> latestTransactions = dao.GetTransactions();

            transactionDAOBindingSource.DataSource = latestTransactions;

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = transactionDAOBindingSource;
        }


        // Load the inventory page when the inventory button is clicked in the dashboard navigation panel
        private void InventoryNavBtn_Click(object sender, EventArgs e)
        {
            var inventoryPage = new Inventory();

            // Get the parent form (which is DashboardForm)
            var dashboardForm = this.FindForm() as DashboardForm;

            if (dashboardForm != null)
            {
                dashboardForm.LoadPage(inventoryPage);
            }
        }


        // Load the sales page when the sales button is clicked in the dashboard navigation panel
        private void salesNavBtn_Click(object sender, EventArgs e)
        {
            var salesPage = new Sales();

            var dashboardForm = this.FindForm() as DashboardForm;

            if (dashboardForm != null)
            {
                dashboardForm.LoadPage(salesPage);
            }
        }


        // Load total sales and display it in the mainpage dashboard
        private async void LoadTotalSales()
        {
            CreateSaleDAO salesDAO = new CreateSaleDAO();
            decimal totalSales = await salesDAO.GetTotalSales();

            CultureInfo culture = new CultureInfo("en-KE");
            string formattedSales = totalSales.ToString("C", culture);

            totalSalesLabel.Text = formattedSales;
        }



        // Load total products in stock and display it in the mainpage dashboard
        private void LoadTotalProducts()
        {
            ProductDAO productsDAO = new ProductDAO();
            int totalProducts = productsDAO.GetTotalStockQuantity();

            string formattedProducts = totalProducts.ToString();

            totalStockLabel.Text = formattedProducts;
        }


        // Load the reports page when the reports button is clicked in the dashboard navigation panel
        private void reportsNavBtn_Click(object sender, EventArgs e)
        {

        }


        // Load the settings page when the settings button is clicked in the dashboard navigation panel
        private void settingsNavBtn_Click(object sender, EventArgs e)
        {
            var settingsPage = new Settings();

            var dashboardForm = this.FindForm() as DashboardForm;

            if (dashboardForm != null)
            {
                dashboardForm.LoadPage(settingsPage);
            }
        }

        private void logoutNavBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to logout?", "Confirm logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SessionManager.Logout();
                this.FindForm()?.Close();                
                
            }


        }
    }
}
