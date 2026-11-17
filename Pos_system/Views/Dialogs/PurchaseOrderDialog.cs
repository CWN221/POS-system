using Pos_system.Core;
using Pos_system.Models;
using Pos_system.Services;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Views.Dialogs
{
    public partial class PurchaseOrderDialog : Form
    {
        private readonly List<Supplier> _suppliers;
        private readonly PurchaseOrderDAO _poDao = new();
        private readonly ProductDAO _productDao = new();

        private PurchaseOrder _po;
        private bool _isExisting;
        private bool _isLocked;

        private List<Product> _allProducts = new();
        private readonly List<PurchaseOrderItem> _items = new();

        public PurchaseOrderDialog(List<Supplier> suppliers, PurchaseOrder? existingPo)
        {
            InitializeComponent();

            _suppliers = suppliers;
            _isExisting = existingPo != null;

            if (_isExisting && existingPo != null)
            {
                _po = _poDao.GetById(existingPo.Id) ?? existingPo;
                _isLocked = _po.Status == "RECEIVED" || _po.Status == "CANCELLED";
            }
            else
            {
                _po = new PurchaseOrder
                {
                    PoNumber = _poDao.GeneratePoNumber(AppContext.CurrentBranchId),
                    BranchId = AppContext.CurrentBranchId,
                    OrderDate = DateTime.Today,
                    ExpectedDate = DateTime.Today.AddDays(7),
                    Status = "DRAFT"
                };
            }

            ConfigureItemsGrid();
            BindSupplierCombo();
            BindFieldsFromPo();
            BindItemsToGrid();

            Shown += async (_, _) => await LoadProductsAsync();

            if (_isLocked)
                LockForm();
        }

        private void ConfigureItemsGrid()
        {
            itemsGrid.AutoGenerateColumns = false;
            itemsGrid.Columns.Clear();

            itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ProductId",
                DataPropertyName = "ProductId",
                HeaderText = "Product ID",
                Visible = false
            });

            var productCol = new DataGridViewComboBoxColumn
            {
                Name = "ProductName",
                DataPropertyName = "ProductName",
                HeaderText = "Product",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                FillWeight = 40,
                FlatStyle = FlatStyle.Flat
            };
            itemsGrid.Columns.Add(productCol);

            itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "QuantityOrdered",
                DataPropertyName = "QuantityOrdered",
                HeaderText = "Quantity",
                Width = 110,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });

            itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UnitCost",
                DataPropertyName = "UnitCost",
                HeaderText = "Unit Cost",
                Width = 130,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "LineTotal",
                HeaderText = "Line Total",
                Width = 140,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "C2",
                    Alignment = DataGridViewContentAlignment.MiddleRight
                }
            });

            itemsGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "QuantityReceived",
                DataPropertyName = "QuantityReceived",
                HeaderText = "Received",
                Width = 100,
                ReadOnly = true,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            });
        }

        private async Task LoadProductsAsync()
        {
            try
            {
                _allProducts = await _productDao.GetAllProducts();

                if (itemsGrid.Columns["ProductName"] is DataGridViewComboBoxColumn combo)
                {
                    combo.DisplayMember = "PName";
                    combo.ValueMember = "Idproducts";
                    combo.DataSource = _allProducts;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindSupplierCombo()
        {
            supplierCombo.DataSource = null;
            supplierCombo.DataSource = _suppliers;
            supplierCombo.DisplayMember = "Name";
            supplierCombo.ValueMember = "Id";
        }

        private void BindFieldsFromPo()
        {
            headerLbl.Text = _isExisting ? $"Purchase Order — {_po.PoNumber}" : "New Purchase Order";
            poNumberLbl.Text = _po.PoNumber;

            if (_po.SupplierId > 0)
                supplierCombo.SelectedValue = _po.SupplierId;

            orderDatePicker.Value = _po.OrderDate;
            expectedDatePicker.Value = _po.ExpectedDate ?? DateTime.Today.AddDays(7);
            notesBox.Text = _po.Notes ?? "";

            statusCombo.SelectedItem = _po.Status;
            if (statusCombo.SelectedIndex < 0) statusCombo.SelectedIndex = 0;
        }

        private void BindItemsToGrid()
        {
            _items.Clear();
            foreach (var item in _po.Items)
                _items.Add(item);

            RefreshItemsGrid();
        }

        private void RefreshItemsGrid()
        {
            itemsGrid.Rows.Clear();

            foreach (var item in _items)
            {
                int idx = itemsGrid.Rows.Add();
                var row = itemsGrid.Rows[idx];
                row.Cells["ProductId"].Value = item.ProductId;
                row.Cells["ProductName"].Value = item.ProductId;
                row.Cells["QuantityOrdered"].Value = item.QuantityOrdered;
                row.Cells["UnitCost"].Value = item.UnitCost;
                row.Cells["LineTotal"].Value = item.QuantityOrdered * item.UnitCost;
                row.Cells["QuantityReceived"].Value = item.QuantityReceived;

                if (_isLocked || item.QuantityReceived > 0)
                {
                    row.Cells["ProductName"].ReadOnly = true;
                    row.Cells["QuantityOrdered"].ReadOnly = true;
                    row.Cells["UnitCost"].ReadOnly = true;
                }
            }

            UpdateTotal();
        }

        private void UpdateTotal()
        {
            decimal total = _items.Sum(i => i.QuantityOrdered * i.UnitCost);
            totalValueLbl.Text = total.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("en-KE"));
        }

        private void AddItemBtn_Click(object? sender, EventArgs e)
        {
            if (_isLocked) return;

            if (_allProducts.Count == 0)
            {
                MessageBox.Show("No products available.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var firstProduct = _allProducts.First();

            _items.Add(new PurchaseOrderItem
            {
                ProductId = firstProduct.Idproducts,
                ProductName = firstProduct.PName,
                QuantityOrdered = 1,
                UnitCost = firstProduct.Price,
                QuantityReceived = 0
            });

            RefreshItemsGrid();
        }

        private void RemoveItemBtn_Click(object? sender, EventArgs e)
        {
            if (_isLocked) return;

            if (itemsGrid.CurrentRow == null)
            {
                MessageBox.Show("Select a row to remove.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idx = itemsGrid.CurrentRow.Index;
            if (idx < 0 || idx >= _items.Count) return;

            var item = _items[idx];
            if (item.QuantityReceived > 0)
            {
                MessageBox.Show("Cannot remove an item that has already been partially or fully received.",
                    "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _items.RemoveAt(idx);
            RefreshItemsGrid();
        }

        private void ItemsGrid_CellEndEdit(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _items.Count) return;

            var row = itemsGrid.Rows[e.RowIndex];
            var item = _items[e.RowIndex];

            try
            {
                var productIdCell = row.Cells["ProductName"].Value;
                if (productIdCell != null && int.TryParse(productIdCell.ToString(), out int productId))
                {
                    var product = _allProducts.FirstOrDefault(p => p.Idproducts == productId);
                    if (product != null && product.Idproducts != item.ProductId)
                    {
                        item.ProductId = product.Idproducts;
                        item.ProductName = product.PName;
                        item.UnitCost = product.Price;
                        row.Cells["UnitCost"].Value = item.UnitCost;
                    }
                }

                if (int.TryParse(row.Cells["QuantityOrdered"].Value?.ToString(), out int qty) && qty > 0)
                    item.QuantityOrdered = qty;
                else
                    row.Cells["QuantityOrdered"].Value = item.QuantityOrdered;

                if (decimal.TryParse(row.Cells["UnitCost"].Value?.ToString(), out decimal cost) && cost >= 0)
                    item.UnitCost = cost;
                else
                    row.Cells["UnitCost"].Value = item.UnitCost;

                row.Cells["LineTotal"].Value = item.QuantityOrdered * item.UnitCost;
                UpdateTotal();
            }
            catch
            {
                RefreshItemsGrid();
            }
        }

        private async void SaveDraftBtn_Click(object? sender, EventArgs e)
        {
            await SaveOrderAsync("DRAFT", requireSupplier: false);
        }

        private async void MarkOrderedBtn_Click(object? sender, EventArgs e)
        {
            await SaveOrderAsync("ORDERED", requireSupplier: true);
        }

        private async Task SaveOrderAsync(string newStatus, bool requireSupplier)
        {
            if (_isLocked)
            {
                MessageBox.Show("This purchase order is locked and cannot be edited.",
                    "Locked", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_items.Count == 0)
            {
                MessageBox.Show("Add at least one item to this purchase order.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (requireSupplier && supplierCombo.SelectedItem is not Supplier)
            {
                MessageBox.Show("Select a supplier before marking as ordered.",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _po.SupplierId = supplierCombo.SelectedValue is int sid ? sid : 0;
            _po.OrderDate = orderDatePicker.Value.Date;
            _po.ExpectedDate = expectedDatePicker.Value.Date;
            _po.Notes = string.IsNullOrWhiteSpace(notesBox.Text) ? null : notesBox.Text.Trim();
            _po.Status = newStatus;
            _po.Items = _items;

            if (_po.SupplierId == 0)
            {
                MessageBox.Show("Select a supplier for this purchase order.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_poDao.Save(_po, out string message))
            {
                MessageBox.Show(message, "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CancelBtn_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LockForm()
        {
            supplierCombo.Enabled = false;
            orderDatePicker.Enabled = false;
            expectedDatePicker.Enabled = false;
            notesBox.ReadOnly = true;
            addItemBtn.Enabled = false;
            removeItemBtn.Enabled = false;
            itemsGrid.ReadOnly = true;
            statusCombo.Enabled = false;
            saveDraftBtn.Enabled = false;
            markOrderedBtn.Enabled = false;
        }
    }
}