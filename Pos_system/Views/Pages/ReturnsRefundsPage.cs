using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.Models;
using Pos_system.Services;
using Pos_system.Views.Dialogs;
using System.Data;
using System.Drawing.Printing;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class ReturnsRefundsPage : UserControl
    {
        private DataTable? _saleItemsTable;
        private int _loadedSaleId;
        private int _lastReturnId;
        private ReturnResult? _lastReturnResult;
        private StoreSettings? _lastStoreSettings;
        private bool _processingReturn = false;

        public ReturnsRefundsPage()
        {
            InitializeComponent();

            branchLbl.Text = $"Branch: {AppContext.CurrentBranch?.BranchName ?? "—"}";

            ConfigureGrid();
            var year = DateTime.Now.Year;
            rcpLbl.Text = $"RCP-{year}-";

            _ = LoadStoreSettingsAsync();
        }

        private async Task LoadStoreSettingsAsync()
        {
            try { _lastStoreSettings = await AppServices.GetRequiredService<StoreSettingsService>().GetStoreSettings(AppContext.CurrentBranchId); }
            catch { _lastStoreSettings = null; }
        }

        private void ConfigureGrid()
        {
            printRtnRcptBtn.Enabled = false;
            printRtnRcptBtn.Click += PrintReturnReceiptBtn_Click;
            gridSaleItems.AutoGenerateColumns = false;
            gridSaleItems.Columns.Clear();

            var selectCol = new DataGridViewCheckBoxColumn
            {
                Name = "Select",
                HeaderText = "Select",
                Width = 60,
                FlatStyle = FlatStyle.Flat
            };
            gridSaleItems.Columns.Add(selectCol);

            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "id", DataPropertyName = "id", HeaderText = "ID", Visible = false });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "product_id", DataPropertyName = "product_id", HeaderText = "Product ID", Visible = false });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "product_name",
                DataPropertyName = "product_name",
                HeaderText = "Product",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 45
            });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "quantity", DataPropertyName = "quantity", HeaderText = "Sold Qty", Width = 90, ReadOnly = true });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "unit_price",
                DataPropertyName = "unit_price",
                HeaderText = "Unit Price",
                Width = 110,
                ReadOnly = true
            });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "qty_to_return",
                DataPropertyName = "qty_to_return",
                HeaderText = "Return Qty",
                Width = 100
            });
            gridSaleItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "total_price",
                DataPropertyName = "total_price",
                HeaderText = "Line Total",
                Width = 110,
                ReadOnly = true
            });

            gridSaleItems.Columns["unit_price"].DefaultCellStyle.Format = "C2";
            gridSaleItems.Columns["total_price"].DefaultCellStyle.Format = "C2";
            gridSaleItems.Columns["unit_price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridSaleItems.Columns["total_price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            gridSaleItems.Columns["quantity"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridSaleItems.Columns["qty_to_return"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            gridSaleItems.CurrentCellDirtyStateChanged += GridSaleItems_CurrentCellDirtyStateChanged;
            gridSaleItems.CellValueChanged += GridSaleItems_CellValueChanged;
        }

        private void GridSaleItems_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (gridSaleItems.IsCurrentCellDirty && gridSaleItems.CurrentCell is DataGridViewCheckBoxCell)
                gridSaleItems.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void GridSaleItems_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (gridSaleItems.Columns[e.ColumnIndex].Name != "Select") return;

            int selected = 0;
            foreach (DataGridViewRow row in gridSaleItems.Rows)
            {
                if (row.Cells["Select"].Value as bool? == true) selected++;
            }
            selectedCountLbl.Text = selected == 1 ? "1 item selected" : $"{selected} items selected";
        }

        private async void FindReceiptBtn_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtReceiptNo.Text.Trim(), out int saleId))
            {
                MessageBox.Show("Enter a valid receipt / sale number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtReceiptNo.Focus();
                return;
            }

            try
            {
                using var conn = DBConnection.GetConnection();
                const string query = @"SELECT s.id, s.sale_date, s.total_amount, s.payment_method,
                                              s.receipt_number,
                                              u.username AS cashier_name
                                       FROM sales s
                                       LEFT JOIN users u ON u.id = s.cashier_id
                                       WHERE s.id = @id AND s.BranchId = @branchId";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", saleId);
                cmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId);

                using var reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    _loadedSaleId = reader.GetInt32(reader.GetOrdinal("id"));
                    DateOutputLbl.Text = reader.GetDateTime(reader.GetOrdinal("sale_date")).ToString("dd MMM yyyy, hh:mm tt");
                    BranchOutputLbl.Text = AppContext.CurrentBranch?.BranchName ?? "—";
                    PaymentOutputLbl.Text = reader.IsDBNull(reader.GetOrdinal("payment_method")) ? "—" : reader.GetString(reader.GetOrdinal("payment_method"));
                    CashierOutputLbl.Text = reader.IsDBNull(reader.GetOrdinal("cashier_name")) ? "—" : reader.GetString(reader.GetOrdinal("cashier_name"));
                    totalPayOutputLbl.Text = reader.GetDecimal(reader.GetOrdinal("total_amount")).ToString("C");
                    rcpLbl.Text = reader.IsDBNull(reader.GetOrdinal("receipt_number")) ? $"RCP-{DateTime.Now.Year}-{_loadedSaleId}" : reader.GetString(reader.GetOrdinal("receipt_number"));

                    phoneTextBox.Clear();

                    var priorPhone = await TryLoadPriorReturnPhoneAsync(_loadedSaleId);
                    if (!string.IsNullOrWhiteSpace(priorPhone))
                        phoneTextBox.Text = priorPhone;

                    await LoadSaleItems(_loadedSaleId);
                }
                else
                {
                    MessageBox.Show("Sale not found for the current branch.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearLoadedSale();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error finding sale: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<string?> TryLoadPriorReturnPhoneAsync(int saleId)
        {
            try
            {
                using var conn = DBConnection.GetConnection();
                const string query = @"
                    SELECT customer_phone FROM returns
                    WHERE sale_id = @saleId AND customer_phone IS NOT NULL
                    ORDER BY id DESC LIMIT 1";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@saleId", saleId);

                var result = await cmd.ExecuteScalarAsync();
                return result == null || result == DBNull.Value ? null : result.ToString();
            }
            catch
            {
                return null;
            }
        }

        private void ClearLoadedSale()
        {
            _loadedSaleId = 0;
            DateOutputLbl.Text = "—";
            BranchOutputLbl.Text = "—";
            PaymentOutputLbl.Text = "—";
            CashierOutputLbl.Text = "—";
            totalPayOutputLbl.Text = "—";
            RefundPreviewOutputLbl.Text = "—";
            phoneTextBox.Clear();
            gridSaleItems.DataSource = null;
            selectedCountLbl.Text = "0 items selected";
            printRtnRcptBtn.Enabled = false;
            _lastReturnId = 0;
            _lastReturnResult = null;
        }

        private async Task LoadSaleItems(int saleId)
        {
            using var conn = DBConnection.GetConnection();
            const string query = @"SELECT id, product_id, product_name, quantity,
                                          ROUND(total_price / NULLIF(quantity, 0), 2) AS unit_price,
                                          total_price
                                   FROM sale_items
                                   WHERE sale_id = @id
                                   ORDER BY id";

            using var cmd = new NpgsqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", saleId);

            var adapter = new NpgsqlDataAdapter(cmd);
            _saleItemsTable = new DataTable();
            adapter.Fill(_saleItemsTable);
            _saleItemsTable.Columns.Add("qty_to_return", typeof(int));

            foreach (DataRow row in _saleItemsTable.Rows)
                row["qty_to_return"] = row["quantity"];

            gridSaleItems.DataSource = _saleItemsTable;
            selectedCountLbl.Text = "0 items selected";
            RefundPreviewOutputLbl.Text = "—";

            printRtnRcptBtn.Enabled = false;
            _lastReturnId = 0;
            _lastReturnResult = null;
        }

        private async void ProcessReturnBtn_Click(object sender, EventArgs e)
        {
            if (_processingReturn) return;
            _processingReturn = true;
            ProcessReturnBtn.Enabled = false;

            try
            {
                if (_loadedSaleId <= 0)
                {
                    MessageBox.Show("Find a receipt first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (ReasonComboBox.SelectedItem == null && string.IsNullOrWhiteSpace(ReasonComboBox.Text))
                {
                    MessageBox.Show("Please select a reason for the return.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ReasonComboBox.Focus();
                    return;
                }

                string refundType = rbCash.Checked ? "Cash" : rbStoreCredit.Checked ? "StoreCredit" : "Exchange";
                string customerPhone = phoneTextBox.Text.Trim();

                if (refundType == "StoreCredit" && string.IsNullOrWhiteSpace(customerPhone))
                {
                    MessageBox.Show("Customer phone number is required when issuing store credit.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    phoneTextBox.Focus();
                    return;
                }

                var currentUser = SessionManager.CurrentUser;
                if (currentUser == null)
                {
                    MessageBox.Show("No user session.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int approvedBy = currentUser.Id;

                if (currentUser.Role != UserRole.Admin && currentUser.Role != UserRole.Manager)
                {
                    using var authDialog = new ManagerAuthDialog("Cashier returns require manager approval.");
                    if (authDialog.ShowDialog(FindForm()) != DialogResult.OK || authDialog.ApprovedManager == null)
                    {
                        return;
                    }
                    approvedBy = authDialog.ApprovedManager.Id;
                }

                var service = AppServices.GetRequiredService<ReturnsService>();
                var request = new ReturnRequest
                {
                    ReceiptNo = $"RCP-{DateTime.Now.Year}-{_loadedSaleId}",
                    SaleId = _loadedSaleId,
                    Reason = ReasonComboBox.Text,
                    RefundType = refundType,
                    CustomerPhone = customerPhone,
                    RequestedBy = currentUser.Id,
                    ApprovedBy = approvedBy,
                    BranchId = AppContext.CurrentBranchId,
                    Items = new List<ReturnItemRequest>()
                };

                var restockPrompts = new List<(DataGridViewRow row, ReturnItemRequest item)>();

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

                    var item = new ReturnItemRequest
                    {
                        SaleItemId = Convert.ToInt32(row.Cells["id"].Value),
                        ProductId = Convert.ToInt32(row.Cells["product_id"].Value),
                        QtyToReturn = qtyToReturn,
                        LineAmount = Math.Round(unitPrice * qtyToReturn, 2),
                        Restock = false
                    };

                    restockPrompts.Add((row, item));
                }

                if (restockPrompts.Count == 0)
                {
                    MessageBox.Show("Select at least one item to return.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                foreach (var (row, item) in restockPrompts)
                {
                    item.Restock = MessageBox.Show($"Restock {row.Cells["product_name"].Value}?", "Restock Option", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
                    request.Items.Add(item);
                }

                var result = await service.ProcessReturn(request);

                if (result.Approved)
                {
                    _lastReturnId = result.ReturnId;
                    _lastReturnResult = result;

                    RefundPreviewOutputLbl.Text = $"Refund Approved: {result.RefundAmount:C}";
                    printRtnRcptBtn.Enabled = true;

                    MessageBox.Show(result.Message, "Return Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    RefundPreviewOutputLbl.Text = "Refund not approved";
                    MessageBox.Show(result.Message, "Return Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing return: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _processingReturn = false;
                ProcessReturnBtn.Enabled = true;
            }
        }

        private async void PrintReturnReceiptBtn_Click(object? sender, EventArgs e)
        {
            if (_lastReturnId <= 0 || _lastReturnResult == null)
            {
                MessageBox.Show("No return has been processed yet.", "Nothing to Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var returnData = await LoadReturnForPrintAsync(_lastReturnId);
                if (returnData == null)
                {
                    MessageBox.Show("Could not load the return details.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using var preview = new PrintPreviewDialog
                {
                    Document = BuildReturnPrintDocument(returnData),
                    Width = 900,
                    Height = 700,
                    StartPosition = FormStartPosition.CenterParent
                };
                preview.ShowDialog(FindForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing return receipt: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<ReturnPrintData?> LoadReturnForPrintAsync(int returnId)
        {
            using var conn = DBConnection.GetConnection();

            const string headerQuery = @"
                                        SELECT r.id, r.receipt_no, r.sale_id, r.reason, r.refund_type, r.refund_amount, r.created_at,
                                               r.requested_by, u1.username AS requested_by_name,
                                               r.approved_by, u2.username AS approved_by_name
                                        FROM returns r
                                        LEFT JOIN users u1 ON u1.Id = r.requested_by
                                        LEFT JOIN users u2 ON u2.Id = r.approved_by
                                        WHERE r.id = @id";

            var data = new ReturnPrintData();

            using (var cmd = new NpgsqlCommand(headerQuery, conn))
            {
                cmd.Parameters.AddWithValue("@id", returnId);
                using var reader = await cmd.ExecuteReaderAsync();
                if (!await reader.ReadAsync()) return null;

                data.ReturnId = reader.GetInt32(reader.GetOrdinal("id"));
                data.ReceiptNo = reader.IsDBNull(reader.GetOrdinal("receipt_no")) ? $"RCP-{DateTime.Now.Year}-{data.ReturnId}" : reader.GetString(reader.GetOrdinal("receipt_no"));
                data.OriginalSaleId = reader.GetInt32(reader.GetOrdinal("sale_id"));
                data.Reason = reader.IsDBNull(reader.GetOrdinal("reason")) ? "—" : reader.GetString(reader.GetOrdinal("reason"));
                data.RefundType = reader.IsDBNull(reader.GetOrdinal("refund_type")) ? "Cash" : reader.GetString(reader.GetOrdinal("refund_type"));
                data.RefundAmount = reader.GetDecimal(reader.GetOrdinal("refund_amount"));
                data.CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at"));
                data.RequestedByName = reader.IsDBNull(reader.GetOrdinal("requested_by_name")) ? "—" : reader.GetString(reader.GetOrdinal("requested_by_name"));
                data.ApprovedByName = reader.IsDBNull(reader.GetOrdinal("approved_by_name")) ? data.RequestedByName : reader.GetString(reader.GetOrdinal("approved_by_name"));
            }

            const string itemsQuery = @"
                                        SELECT ri.qty_returned, ri.line_amount, si.product_name
                                        FROM return_items ri
                                        JOIN sale_items si ON si.id = ri.sale_item_id
                                        WHERE ri.return_id = @id
                                        ORDER BY ri.id";

            using (var cmd = new NpgsqlCommand(itemsQuery, conn))
            {
                cmd.Parameters.AddWithValue("@id", returnId);
                using var reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    data.Items.Add(new ReturnPrintItem
                    {
                        ProductName = reader.IsDBNull(reader.GetOrdinal("product_name")) ? "Unknown" : reader.GetString(reader.GetOrdinal("product_name")),
                        Quantity = reader.GetInt32(reader.GetOrdinal("qty_returned")),
                        LineAmount = reader.GetDecimal(reader.GetOrdinal("line_amount"))
                    });
                }
            }

            return data;
        }

        private PrintDocument BuildReturnPrintDocument(ReturnPrintData data)
        {
            var doc = new PrintDocument();
            doc.PrintPage += (sender, e) =>
            {
                if (e.Graphics == null) return;
                DrawReturnReceipt(e.Graphics, data);
            };
            return doc;
        }

        private void DrawReturnReceipt(Graphics g, ReturnPrintData data)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            const float left = 20f;
            float y = 20f;
            const float receiptWidth = 380f;
            float rightEdge = left + receiptWidth;

            var fontTitle = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            var fontBig = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            var fontNormal = new Font("Segoe UI", 10F);
            var fontSmall = new Font("Segoe UI", 8.5F);
            var fontMono = new Font("Consolas", 10F);
            var fontMonoBold = new Font("Consolas", 10F, FontStyle.Bold);
            var fontSectionTitle = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);

            var centerFormat = new StringFormat { Alignment = StringAlignment.Center };
            var rightFormat = new StringFormat { Alignment = StringAlignment.Far };
            var leftFormat = new StringFormat { Alignment = StringAlignment.Near };

            var store = _lastStoreSettings;

            void Center(string text, Font font, float lineHeight)
            {
                g.DrawString(text, font, Brushes.Black, new RectangleF(left, y, receiptWidth, lineHeight), centerFormat);
                y += lineHeight;
            }

            void LeftRight(string leftText, string rightText, Font font, float lineHeight)
            {
                g.DrawString(leftText, font, Brushes.Black, new RectangleF(left, y, receiptWidth * 0.6f, lineHeight), leftFormat);
                g.DrawString(rightText, font, Brushes.Black, new RectangleF(left + receiptWidth * 0.6f, y, receiptWidth * 0.4f, lineHeight), rightFormat);
                y += lineHeight;
            }

            void DashedLine()
            {
                using var pen = new Pen(Color.Black, 1) { DashStyle = System.Drawing.Drawing2D.DashStyle.Dash };
                g.DrawLine(pen, left, y + 4, rightEdge, y + 4);
                y += 12;
            }

            void SolidLine()
            {
                using var pen = new Pen(Color.Black, 1);
                g.DrawLine(pen, left, y + 4, rightEdge, y + 4);
                y += 12;
            }

            Center(store?.Storename ?? "Store", fontTitle, 28);
            y += 4;

            if (!string.IsNullOrWhiteSpace(store?.Address))
                Center(store.Address, fontSmall, 16);

            var contactLine = string.Join(" | ", new[] { store?.Contact, store?.Contact2 }.Where(s => !string.IsNullOrWhiteSpace(s)));
            if (!string.IsNullOrWhiteSpace(contactLine))
                Center(contactLine, fontSmall, 16);

            if (!string.IsNullOrWhiteSpace(store?.BusinessPin))
                Center($"KRA PIN: {store.BusinessPin}", fontSmall, 16);

            var branch = AppContext.CurrentBranch?.BranchName;
            if (!string.IsNullOrWhiteSpace(branch))
                Center(branch, fontSmall, 16);

            y += 6;
            DashedLine();

            Center("RETURN RECEIPT", fontBig, 22);
            y += 4;
            DashedLine();

            LeftRight("Return #:", data.ReceiptNo, fontNormal, 18);
            LeftRight("Original Sale:", $"INV-{data.CreatedAt.Year}-{data.OriginalSaleId:D4}", fontNormal, 18);
            LeftRight("Date:", data.CreatedAt.ToString("dd MMM yyyy"), fontNormal, 18);
            LeftRight("Time:", data.CreatedAt.ToString("hh:mm tt"), fontNormal, 18);
            LeftRight("Processed By:", data.RequestedByName, fontNormal, 18);
            LeftRight("Approved By:", data.ApprovedByName, fontNormal, 18);

            y += 4;
            DashedLine();

            g.DrawString("QTY", fontSectionTitle, Brushes.Black, left, y);
            g.DrawString("ITEM", fontSectionTitle, Brushes.Black, left + 40, y);
            g.DrawString("TOTAL (KES)", fontSectionTitle, Brushes.Black, new RectangleF(left + receiptWidth * 0.55f, y, receiptWidth * 0.45f, 18), rightFormat);
            y += 20;
            DashedLine();

            foreach (var item in data.Items)
            {
                g.DrawString(item.Quantity.ToString(), fontMono, Brushes.Black, left, y);
                g.DrawString(item.ProductName, fontMono, Brushes.Black, left + 40, y);
                g.DrawString(item.LineAmount.ToString("N2"), fontMono, Brushes.Black, new RectangleF(left + receiptWidth * 0.55f, y, receiptWidth * 0.45f, 18), rightFormat);
                y += 18;
            }

            y += 2;
            DashedLine();

            LeftRight("REFUND TYPE", data.RefundType.ToUpperInvariant(), fontMono, 18);
            LeftRight("REASON", data.Reason.Length > 30 ? data.Reason.Substring(0, 30) + "..." : data.Reason, fontMono, 18);

            SolidLine();

            g.DrawString("REFUND TOTAL", fontBig, Brushes.Black, left, y);
            g.DrawString(data.RefundAmount.ToString("N2"), fontBig, Brushes.Black, new RectangleF(left + receiptWidth * 0.55f, y, receiptWidth * 0.45f, 24), rightFormat);
            y += 28;

            SolidLine();

            y += 10;
            DashedLine();

            Center("THIS IS A RETURN RECEIPT", fontSectionTitle, 20);
            Center("Thank you.", fontNormal, 18);
            y += 10;
            Center("Keep this receipt for your records.", fontSmall, 14);
        }

        private void BackToDashboardBtn_Click(object sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }
    }
}