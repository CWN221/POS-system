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
using Pos_system.Services;
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

        private void salesNavBtn_Click(object sender, EventArgs e)
        {
            var salesPage = new Sales();

            var dashboardForm = this.FindForm() as DashboardForm;

            if (dashboardForm != null)
            {
                dashboardForm.LoadPage(salesPage);
            }
        }

        private async void LoadTotalSales()
        {
            CreateSaleDAO salesDAO = new CreateSaleDAO();
            decimal totalSales = await salesDAO.GetTotalSales();

            //string formattedSales = totalSales.ToString("C2");
            CultureInfo culture = new CultureInfo("en-KE");
            string formattedSales = totalSales.ToString("C", culture);

            totalSalesLabel.Text = formattedSales;
        }


        private void LoadTotalProducts()
        {
            ProductDAO productsDAO = new ProductDAO();
            int totalProducts = productsDAO.GetTotalStockQuantity();

            string formattedProducts = totalProducts.ToString();

            totalStockLabel.Text = formattedProducts;
        }
    }
}
