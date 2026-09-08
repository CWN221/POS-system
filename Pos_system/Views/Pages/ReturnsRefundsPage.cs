using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.Models;
using Pos_system.Services;
using System.Data;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class ReturnsRefundsPage : UserControl
    {
        private DataTable? _saleItemsTable;
        private int _loadedSaleId;

        public ReturnsRefundsPage()
        {
            InitializeComponent();
            ConfigureGrid();
            var year = DateTime.Now.Year;
            rcpLbl.Text = $"RCP-{year}-";
        }

        private void ConfigureGrid()
        {
            gridSaleItems.AutoGenerateColumns = false;
            gridSaleItems.Columns.Clear();

            gridSaleItems.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Select", HeaderText = "Select", Width = 60 });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "id", DataPropertyName = "id", HeaderText = "ID", Visible = false });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "product_id", DataPropertyName = "product_id", HeaderText = "Product ID", Visible = false });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "product_name", DataPropertyName = "product_name", HeaderText = "Product", Width = 250 });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "quantity", DataPropertyName = "quantity", HeaderText = "Sold Qty", Width = 80, ReadOnly = true });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "unit_price", DataPropertyName = "unit_price", HeaderText = "Unit Price", Width = 100, ReadOnly = true });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "qty_to_return", DataPropertyName = "qty_to_return", HeaderText = "Return Qty", Width = 90 });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "total_price", DataPropertyName = "total_price", HeaderText = "Line Total", Width = 100, ReadOnly = true });
        }

        private async void FindReceiptBtn_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtReceiptNo.Text.Trim(), out int saleId))
            {
                MessageBox.Show("Enter a valid receipt/sale number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var conn = DBConnection.GetConnection();
            const string query = @"SELECT s.id, s.sale_date, s.total_amount, s.BranchId
                                   FROM sales s WHERE s.id = @id AND s.BranchId = @branchId";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", saleId);
            cmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);

            using var reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                _loadedSaleId = reader.GetInt32("id");
                DateOutputLbl.Text = reader.GetDateTime("sale_date").ToString("g");
                totalPayOutputLbl.Text = reader.GetDecimal("total_amount").ToString("C");
                await LoadSaleItems(_loadedSaleId);
            }
            else
            {
                MessageBox.Show("Sale not found for the current branch.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private async Task LoadSaleItems(int saleId)
        {
            using var conn = DBConnection.GetConnection();
            const string query = @"SELECT id, product_id, product_name, quantity,
                                          ROUND(total_price / NULLIF(quantity, 0), 2) AS unit_price, total_price
                                   FROM sale_items WHERE sale_id = @id";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", saleId);

            var adapter = new NpgsqlDataAdapter(cmd);
            _saleItemsTable = new DataTable();
            adapter.Fill(_saleItemsTable);
            _saleItemsTable.Columns.Add("qty_to_return", typeof(int));

            foreach (DataRow row in _saleItemsTable.Rows)
                row["qty_to_return"] = row["quantity"];

            gridSaleItems.DataSource = _saleItemsTable;
        }

        private async void ProcessReturnBtn_Click(object sender, EventArgs e)
        {
            if (_loadedSaleId <= 0)
            {
                MessageBox.Show("Find a receipt first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var service = AppServices.GetRequiredService<ReturnsService>();
            var request = new ReturnRequest
            {
                ReceiptNo = $"RCP-{DateTime.Now.Year}-{_loadedSaleId}",
                SaleId = _loadedSaleId,
                Reason = ReasonComboBox.Text,
                RefundType = rbCash.Checked ? "Cash" : rbStoreCredit.Checked ? "StoreCredit" : "Exchange",
                RequestedBy = AppContext.CurrentUserId,
                BranchId = AppContext.CurrentBranchId,
                Items = new List<ReturnItemRequest>()
            };

            foreach (DataGridViewRow row in gridSaleItems.Rows)
            {
                if (row.IsNewRow) continue;

                var selected = row.Cells["Select"].Value as bool? ?? false;
                if (!selected) continue;

                int soldQty = Convert.ToInt32(row.Cells["quantity"].Value);
                int qtyToReturn = int.TryParse(row.Cells["qty_to_return"].Value?.ToString(), out int q) ? q : soldQty;

                if (qtyToReturn <= 0 || qtyToReturn > soldQty)
                {
                    MessageBox.Show($"Invalid return quantity for {row.Cells["product_name"].Value}.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal unitPrice = Convert.ToDecimal(row.Cells["unit_price"].Value);
                bool restock = MessageBox.Show(
                    $"Restock {row.Cells["product_name"].Value}?",
                    "Restock Option",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) == DialogResult.Yes;

                request.Items.Add(new ReturnItemRequest
                {
                    SaleItemId = Convert.ToInt32(row.Cells["id"].Value),
                    ProductId = Convert.ToInt32(row.Cells["product_id"].Value),
                    QtyToReturn = qtyToReturn,
                    LineAmount = Math.Round(unitPrice * qtyToReturn, 2),
                    Restock = restock
                });
            }

            if (request.Items.Count == 0)
            {
                MessageBox.Show("Select at least one item to return.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = await service.ProcessReturn(request);

            if (result.Approved)
            {
                RefundPreviewOutputLbl.Text = $"Refund: {result.RefundAmount:C}";
                MessageBox.Show(result.Message, "Return Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(result.Message, "Return Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackToDashboardBtn_Click(object sender, EventArgs e)
        {
            var form = FindForm() as DashboardForm;
            form?.LoadPage(new Dashboard());
        }
    }
}
