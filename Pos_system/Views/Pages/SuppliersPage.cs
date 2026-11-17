using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.Models;
using Pos_system.Services;
using Pos_system.Views.Dialogs;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Pages
{
    public partial class SuppliersPage : UserControl
    {
        private readonly SupplierDAO _supplierDao = new();
        private readonly PurchaseOrderDAO _poDao = new();
        private readonly GoodsReceiptDAO _receiptDao = new();

        private int? _editingSupplierId;
        private List<PurchaseOrder> _purchaseOrders = new();

        public SuppliersPage()
        {
            InitializeComponent();

            ConfigureSuppliersGrid();
            ConfigurePoGrid();
            ConfigureReceiveGrid();

            Load += SuppliersPage_Load;
        }

        private async void SuppliersPage_Load(object? sender, EventArgs e)
        {
            await LoadSuppliersAsync();
            await LoadPurchaseOrdersAsync();
            await LoadReceiveDropdownAsync();
        }

        private void ConfigureSuppliersGrid()
        {
            suppliersGrid.AutoGenerateColumns = false;
            suppliersGrid.Columns.Clear();

            suppliersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 60
            });
            suppliersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Name",
                DataPropertyName = "Name",
                HeaderText = "Supplier",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 40
            });
            suppliersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ContactPerson",
                DataPropertyName = "ContactPerson",
                HeaderText = "Contact",
                Width = 140
            });
            suppliersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Phone",
                DataPropertyName = "Phone",
                HeaderText = "Phone",
                Width = 130
            });
            suppliersGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Email",
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 200
            });
            suppliersGrid.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                DataPropertyName = "IsActive",
                HeaderText = "Active",
                Width = 70
            });
        }

        private void ConfigurePoGrid()
        {
            poGrid.AutoGenerateColumns = false;
            poGrid.Columns.Clear();

            poGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Visible = false
            });
            poGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "PoNumber",
                DataPropertyName = "PoNumber",
                HeaderText = "PO Number",
                Width = 140
            });
            poGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SupplierName",
                DataPropertyName = "SupplierName",
                HeaderText = "Supplier",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 30
            });
            poGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 110
            });
            poGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "OrderDate",
                DataPropertyName = "OrderDate",
                HeaderText = "Order Date",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd MMM yyyy" }
            });
            poGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ExpectedDate",
                DataPropertyName = "ExpectedDate",
                HeaderText = "Expected",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd MMM yyyy" }
            });
            poGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TotalValue",
                DataPropertyName = "TotalValue",
                HeaderText = "Total Value",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            poGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "CreatedByName",
                DataPropertyName = "CreatedByName",
                HeaderText = "Created By",
                Width = 130
            });
        }

        private void ConfigureReceiveGrid()
        {
            receiveGrid.AutoGenerateColumns = false;
            receiveGrid.Columns.Clear();

            receiveGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "Item ID",
                Visible = false
            });
            receiveGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductId",
                DataPropertyName = "ProductId",
                HeaderText = "Product ID",
                Visible = false
            });
            receiveGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductName",
                DataPropertyName = "ProductName",
                HeaderText = "Product",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 40,
                ReadOnly = true
            });
            receiveGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "QuantityOrdered",
                DataPropertyName = "QuantityOrdered",
                HeaderText = "Ordered",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
            receiveGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "QuantityReceived",
                DataPropertyName = "QuantityReceived",
                HeaderText = "Already Received",
                Width = 140,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
            receiveGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitCost",
                DataPropertyName = "UnitCost",
                HeaderText = "Unit Cost",
                Width = 110,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });
            receiveGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ReceiveNow",
                HeaderText = "Receiving Now",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
        }


        private async Task LoadSuppliersAsync()
        {
            try
            {
                var suppliers = _supplierDao.GetAll(AppContext.CurrentBranchId);
                suppliersGrid.DataSource = null;
                suppliersGrid.DataSource = suppliers;
                suppliersGrid.ClearSelection();
                suppliersGrid.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading suppliers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SuppliersGrid_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (suppliersGrid.Rows[e.RowIndex].DataBoundItem is not Supplier s) return;

            _editingSupplierId = s.Id;
            supplierFormTitle.Text = $"Editing: {s.Name}";
            supplierNameBox.Text = s.Name ?? "";
            supplierContactBox.Text = s.ContactPerson ?? "";
            supplierPhoneBox.Text = s.Phone ?? "";
            supplierEmailBox.Text = s.Email ?? "";
            supplierAddressBox.Text = s.Address ?? "";
            supplierPinBox.Text = s.KraPin ?? "";
            supplierActiveCheck.Checked = s.IsActive;
            saveSupplierBtn.Text = "Update Supplier";
        }

        private void SaveSupplierBtn_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(supplierNameBox.Text))
            {
                MessageBox.Show("Supplier name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                supplierNameBox.Focus();
                return;
            }

            var supplier = new Supplier
            {
                Id = _editingSupplierId ?? 0,
                Name = supplierNameBox.Text.Trim(),
                ContactPerson = string.IsNullOrWhiteSpace(supplierContactBox.Text) ? null : supplierContactBox.Text.Trim(),
                Phone = string.IsNullOrWhiteSpace(supplierPhoneBox.Text) ? null : supplierPhoneBox.Text.Trim(),
                Email = string.IsNullOrWhiteSpace(supplierEmailBox.Text) ? null : supplierEmailBox.Text.Trim(),
                Address = string.IsNullOrWhiteSpace(supplierAddressBox.Text) ? null : supplierAddressBox.Text.Trim(),
                KraPin = string.IsNullOrWhiteSpace(supplierPinBox.Text) ? null : supplierPinBox.Text.Trim(),
                IsActive = supplierActiveCheck.Checked,
                BranchId = AppContext.CurrentBranchId
            };

            if (_supplierDao.Save(supplier, out string message))
            {
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearSupplierForm();
                _ = LoadSuppliersAsync();
            }
            else
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearSupplierBtn_Click(object? sender, EventArgs e)
        {
            ClearSupplierForm();
        }

        private void ClearSupplierForm()
        {
            _editingSupplierId = null;
            supplierFormTitle.Text = "New Supplier";
            supplierNameBox.Clear();
            supplierContactBox.Clear();
            supplierPhoneBox.Clear();
            supplierEmailBox.Clear();
            supplierAddressBox.Clear();
            supplierPinBox.Clear();
            supplierActiveCheck.Checked = true;
            saveSupplierBtn.Text = "Save Supplier";

            suppliersGrid.ClearSelection();
            suppliersGrid.CurrentCell = null;
        }


        private async Task LoadPurchaseOrdersAsync()
        {
            try
            {
                _purchaseOrders = _poDao.GetAll(AppContext.CurrentBranchId);
                poGrid.DataSource = null;
                poGrid.DataSource = _purchaseOrders;
                poGrid.ClearSelection();
                poGrid.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading purchase orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NewPoBtn_Click(object? sender, EventArgs e)
        {
            var suppliers = _supplierDao.GetActive(AppContext.CurrentBranchId);
            if (suppliers.Count == 0)
            {
                MessageBox.Show("Add at least one active supplier before creating a purchase order.", "No Suppliers", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var dialog = new PurchaseOrderDialog(suppliers, null);
            if (dialog.ShowDialog(FindForm()) == DialogResult.OK)
            {
                _ = LoadPurchaseOrdersAsync();
                _ = LoadReceiveDropdownAsync();
            }
        }

        private void ViewPoBtn_Click(object? sender, EventArgs e)
        {
            if (poGrid.CurrentRow?.DataBoundItem is not PurchaseOrder po)
            {
                MessageBox.Show("Select a purchase order first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var suppliers = _supplierDao.GetActive(AppContext.CurrentBranchId);
            using var dialog = new PurchaseOrderDialog(suppliers, po);
            if (dialog.ShowDialog(FindForm()) == DialogResult.OK)
            {
                _ = LoadPurchaseOrdersAsync();
                _ = LoadReceiveDropdownAsync();
            }
        }


        private async Task LoadReceiveDropdownAsync()
        {
            try
            {
                var openPos = _poDao.GetAll(AppContext.CurrentBranchId).Where(p => p.Status == "ORDERED" || p.Status == "PARTIAL").ToList();

                receivePoCombo.DataSource = null;
                receivePoCombo.DataSource = openPos;
                receivePoCombo.DisplayMember = "PoNumber";
                receivePoCombo.ValueMember = "Id";

                if (openPos.Count > 0)
                {
                    receivePoCombo.SelectedIndex = 0;
                }
                else
                {
                    receiveGrid.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading open purchase orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceivePoCombo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (receivePoCombo.SelectedItem is not PurchaseOrder po)
            {
                receiveGrid.DataSource = null;
                return;
            }

            var detailedPo = _poDao.GetById(po.Id);
            if (detailedPo == null)
            {
                receiveGrid.DataSource = null;
                return;
            }

            var outstandingItems = detailedPo.Items.Where(i => i.QuantityOutstanding > 0).ToList();

            receiveGrid.DataSource = null;
            receiveGrid.DataSource = outstandingItems;
            receiveGrid.ClearSelection();

            foreach (DataGridViewRow row in receiveGrid.Rows)
            {
                row.Cells["ReceiveNow"].Value = 0;
            }
        }

        private async void ConfirmReceiptBtn_Click(object? sender, EventArgs e)
        {
            if (receivePoCombo.SelectedItem is not PurchaseOrder po)
            {
                MessageBox.Show("Select a purchase order first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var itemsToReceive = new List<(int PoItemId, int ProductId, int QtyReceived, decimal UnitCost)>();

            foreach (DataGridViewRow row in receiveGrid.Rows)
            {
                if (row.IsNewRow) continue;

                var poItem = row.DataBoundItem as PurchaseOrderItem;
                if (poItem == null) continue;

                var receiveCell = row.Cells["ReceiveNow"].Value;
                int qty = 0;

                if (receiveCell != null)
                    int.TryParse(receiveCell.ToString(), out qty);

                if (qty <= 0) continue;

                if (qty > poItem.QuantityOutstanding)
                {
                    MessageBox.Show(
                        $"Cannot receive {qty} of {poItem.ProductName}. Only {poItem.QuantityOutstanding} outstanding.",
                        "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                itemsToReceive.Add((poItem.Id, poItem.ProductId, qty, poItem.UnitCost));
            }

            if (itemsToReceive.Count == 0)
            {
                MessageBox.Show("Enter at least one 'Receiving Now' quantity greater than zero.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Confirm receipt of {itemsToReceive.Sum(i => i.QtyReceived)} units across {itemsToReceive.Count} item(s)?",
                "Confirm Goods Received", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            var (success, message) = await _receiptDao.ReceiveGoodsAsync(
                po.Id,
                itemsToReceive,
                string.IsNullOrWhiteSpace(receiveNotesBox.Text) ? null : receiveNotesBox.Text.Trim(),
                AppContext.CurrentBranchId);

            if (success)
            {
                MessageBox.Show(message, "Goods Received", MessageBoxButtons.OK, MessageBoxIcon.Information);
                receiveNotesBox.Clear();
                await LoadPurchaseOrdersAsync();
                await LoadReceiveDropdownAsync();
            }
            else
            {
                MessageBox.Show(message, "Receive Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BackBtn_Click(object? sender, EventArgs e)
        {
            (FindForm() as DashboardForm)?.LoadPage(new Dashboard());
        }
    }
}