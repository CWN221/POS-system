namespace Pos_system.Views.Pages
{
    partial class Sales
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            salesentryheaderLabel = new Label();
            productLabel = new Label();
            productComboBox = new ComboBox();
            qtyLabel = new Label();
            addToCartBtn = new Button();
            quantityTextBox = new TextBox();
            saleGridView = new DataGridView();
            productName = new DataGridViewTextBoxColumn();
            price = new DataGridViewTextBoxColumn();
            quantity = new DataGridViewTextBoxColumn();
            vatAmount = new DataGridViewTextBoxColumn();
            totalPrice = new DataGridViewTextBoxColumn();
            completesaleBtn = new Button();
            printReceiptBtn = new Button();
            sales_totalLabel = new Label();
            sales_vatLabel = new Label();
            totalLabel = new Label();
            vatLabel = new Label();
            priceLabel = new Label();
            unitPriceTextBox = new TextBox();
            backToDashBtn = new Button();
            panel1 = new Panel();
            sales_subtotalLabel = new Label();
            subTotalLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)saleGridView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // salesentryheaderLabel
            // 
            salesentryheaderLabel.AutoSize = true;
            salesentryheaderLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            salesentryheaderLabel.ForeColor = Color.FromArgb(52, 73, 94);
            salesentryheaderLabel.Location = new Point(40, 90);
            salesentryheaderLabel.Margin = new Padding(5, 0, 5, 0);
            salesentryheaderLabel.Name = "salesentryheaderLabel";
            salesentryheaderLabel.Size = new Size(194, 46);
            salesentryheaderLabel.TabIndex = 0;
            salesentryheaderLabel.Text = "Sales Entry";
            // 
            // productLabel
            // 
            productLabel.AutoSize = true;
            productLabel.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            productLabel.ForeColor = Color.FromArgb(52, 73, 94);
            productLabel.Location = new Point(40, 160);
            productLabel.Name = "productLabel";
            productLabel.Size = new Size(79, 25);
            productLabel.TabIndex = 1;
            productLabel.Text = "Product";
            // 
            // productComboBox
            // 
            productComboBox.BackColor = Color.White;
            productComboBox.Cursor = Cursors.Hand;
            productComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            productComboBox.FlatStyle = FlatStyle.Flat;
            productComboBox.Font = new Font("Segoe UI", 11F);
            productComboBox.ForeColor = Color.FromArgb(44, 62, 80);
            productComboBox.FormattingEnabled = true;
            productComboBox.Location = new Point(40, 190);
            productComboBox.Name = "productComboBox";
            productComboBox.Size = new Size(400, 33);
            productComboBox.TabIndex = 2;
            productComboBox.SelectedIndexChanged += productComboBox_SelectedIndexChanged;
            // 
            // qtyLabel
            // 
            qtyLabel.AutoSize = true;
            qtyLabel.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            qtyLabel.ForeColor = Color.FromArgb(52, 73, 94);
            qtyLabel.Location = new Point(40, 240);
            qtyLabel.Name = "qtyLabel";
            qtyLabel.Size = new Size(87, 25);
            qtyLabel.TabIndex = 3;
            qtyLabel.Text = "Quantity";
            // 
            // addToCartBtn
            // 
            addToCartBtn.BackColor = Color.FromArgb(41, 128, 185);
            addToCartBtn.Cursor = Cursors.Hand;
            addToCartBtn.FlatAppearance.BorderSize = 0;
            addToCartBtn.FlatStyle = FlatStyle.Flat;
            addToCartBtn.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            addToCartBtn.ForeColor = Color.White;
            addToCartBtn.Location = new Point(40, 470);
            addToCartBtn.Name = "addToCartBtn";
            addToCartBtn.Size = new Size(180, 45);
            addToCartBtn.TabIndex = 4;
            addToCartBtn.Text = "Add To Cart";
            addToCartBtn.UseVisualStyleBackColor = false;
            addToCartBtn.Click += addToCartBtn_Click;
            // 
            // quantityTextBox
            // 
            quantityTextBox.BorderStyle = BorderStyle.FixedSingle;
            quantityTextBox.Font = new Font("Segoe UI", 11F);
            quantityTextBox.Location = new Point(40, 270);
            quantityTextBox.Name = "quantityTextBox";
            quantityTextBox.Size = new Size(180, 32);
            quantityTextBox.TabIndex = 5;
            // 
            // saleGridView
            // 
            saleGridView.AllowUserToAddRows = false;
            saleGridView.AllowUserToDeleteRows = false;
            saleGridView.BackgroundColor = Color.White;
            saleGridView.BorderStyle = BorderStyle.None;
            saleGridView.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            saleGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            saleGridView.ColumnHeadersHeight = 45;
            saleGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            saleGridView.Columns.AddRange(new DataGridViewColumn[] { productName, price, quantity, vatAmount, totalPrice });
            saleGridView.EnableHeadersVisualStyles = false;
            saleGridView.GridColor = Color.FromArgb(236, 240, 241);
            saleGridView.Location = new Point(500, 90);
            saleGridView.Name = "saleGridView";
            saleGridView.ReadOnly = true;
            saleGridView.RowHeadersVisible = false;
            saleGridView.RowHeadersWidth = 51;
            saleGridView.RowTemplate.Height = 35;
            saleGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            saleGridView.Size = new Size(900, 420);
            saleGridView.TabIndex = 6;
            // 
            // productName
            // 
            productName.HeaderText = "Product Name";
            productName.MinimumWidth = 6;
            productName.Name = "productName";
            productName.ReadOnly = true;
            productName.Width = 350;
            // 
            // price
            // 
            price.HeaderText = "Price";
            price.MinimumWidth = 6;
            price.Name = "price";
            price.ReadOnly = true;
            price.Width = 120;
            // 
            // quantity
            // 
            quantity.HeaderText = "Quantity";
            quantity.MinimumWidth = 6;
            quantity.Name = "quantity";
            quantity.ReadOnly = true;
            // 
            // vatAmount
            // 
            vatAmount.HeaderText = "VAT Amount";
            vatAmount.MinimumWidth = 6;
            vatAmount.Name = "vatAmount";
            vatAmount.ReadOnly = true;
            vatAmount.Resizable = DataGridViewTriState.False;
            vatAmount.Width = 120;
            // 
            // totalPrice
            // 
            totalPrice.HeaderText = "Total Price";
            totalPrice.MinimumWidth = 6;
            totalPrice.Name = "totalPrice";
            totalPrice.ReadOnly = true;
            totalPrice.Width = 130;
            // 
            // completesaleBtn
            // 
            completesaleBtn.BackColor = Color.FromArgb(39, 174, 96);
            completesaleBtn.Cursor = Cursors.Hand;
            completesaleBtn.FlatAppearance.BorderSize = 0;
            completesaleBtn.FlatStyle = FlatStyle.Flat;
            completesaleBtn.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            completesaleBtn.ForeColor = Color.White;
            completesaleBtn.Location = new Point(500, 580);
            completesaleBtn.Name = "completesaleBtn";
            completesaleBtn.Size = new Size(160, 45);
            completesaleBtn.TabIndex = 7;
            completesaleBtn.Text = "Complete Sale";
            completesaleBtn.UseVisualStyleBackColor = false;
            completesaleBtn.Click += completeSaleBtn;
            // 
            // printReceiptBtn
            // 
            printReceiptBtn.BackColor = Color.FromArgb(142, 68, 173);
            printReceiptBtn.Cursor = Cursors.Hand;
            printReceiptBtn.FlatAppearance.BorderSize = 0;
            printReceiptBtn.FlatStyle = FlatStyle.Flat;
            printReceiptBtn.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            printReceiptBtn.ForeColor = Color.White;
            printReceiptBtn.Location = new Point(1180, 580);
            printReceiptBtn.Name = "printReceiptBtn";
            printReceiptBtn.Size = new Size(160, 45);
            printReceiptBtn.TabIndex = 8;
            printReceiptBtn.Text = "Print Receipt";
            printReceiptBtn.UseVisualStyleBackColor = false;
            printReceiptBtn.Visible = false;
            // 
            // sales_totalLabel
            // 
            sales_totalLabel.AutoSize = true;
            sales_totalLabel.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            sales_totalLabel.ForeColor = Color.FromArgb(231, 76, 60);
            sales_totalLabel.Location = new Point(758, 642);
            sales_totalLabel.Name = "sales_totalLabel";
            sales_totalLabel.Size = new Size(61, 30);
            sales_totalLabel.TabIndex = 9;
            sales_totalLabel.Text = "Total";
            // 
            // sales_vatLabel
            // 
            sales_vatLabel.AutoSize = true;
            sales_vatLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            sales_vatLabel.ForeColor = Color.FromArgb(52, 73, 94);
            sales_vatLabel.Location = new Point(758, 592);
            sales_vatLabel.Name = "sales_vatLabel";
            sales_vatLabel.Size = new Size(47, 28);
            sales_vatLabel.TabIndex = 10;
            sales_vatLabel.Text = "VAT";
            // 
            // totalLabel
            // 
            totalLabel.AutoSize = true;
            totalLabel.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            totalLabel.ForeColor = Color.FromArgb(231, 76, 60);
            totalLabel.Location = new Point(1050, 540);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new Size(0, 30);
            totalLabel.TabIndex = 11;
            // 
            // vatLabel
            // 
            vatLabel.AutoSize = true;
            vatLabel.Font = new Font("Segoe UI", 12F);
            vatLabel.ForeColor = Color.FromArgb(52, 73, 94);
            vatLabel.Location = new Point(1050, 490);
            vatLabel.Name = "vatLabel";
            vatLabel.Size = new Size(0, 28);
            vatLabel.TabIndex = 12;
            // 
            // priceLabel
            // 
            priceLabel.AutoSize = true;
            priceLabel.Font = new Font("Segoe UI Semibold", 11F, FontStyle.Bold);
            priceLabel.ForeColor = Color.FromArgb(52, 73, 94);
            priceLabel.Location = new Point(260, 240);
            priceLabel.Name = "priceLabel";
            priceLabel.Size = new Size(54, 25);
            priceLabel.TabIndex = 13;
            priceLabel.Text = "Price";
            // 
            // unitPriceTextBox
            // 
            unitPriceTextBox.BorderStyle = BorderStyle.FixedSingle;
            unitPriceTextBox.Font = new Font("Segoe UI", 11F);
            unitPriceTextBox.Location = new Point(260, 270);
            unitPriceTextBox.Name = "unitPriceTextBox";
            unitPriceTextBox.Size = new Size(180, 32);
            unitPriceTextBox.TabIndex = 14;
            // 
            // backToDashBtn
            // 
            backToDashBtn.BackColor = Color.FromArgb(52, 73, 94);
            backToDashBtn.BackgroundImageLayout = ImageLayout.Center;
            backToDashBtn.Cursor = Cursors.Hand;
            backToDashBtn.FlatAppearance.BorderSize = 0;
            backToDashBtn.FlatStyle = FlatStyle.Flat;
            backToDashBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            backToDashBtn.ForeColor = Color.White;
            backToDashBtn.Image = Properties.Resources.dashboard_png;
            backToDashBtn.ImageAlign = ContentAlignment.MiddleLeft;
            backToDashBtn.Location = new Point(20, 12);
            backToDashBtn.Margin = new Padding(0, 3, 3, 3);
            backToDashBtn.Name = "backToDashBtn";
            backToDashBtn.Padding = new Padding(10, 0, 0, 0);
            backToDashBtn.RightToLeft = RightToLeft.No;
            backToDashBtn.Size = new Size(140, 45);
            backToDashBtn.TabIndex = 15;
            backToDashBtn.Text = "Dashboard";
            backToDashBtn.TextAlign = ContentAlignment.MiddleRight;
            backToDashBtn.UseVisualStyleBackColor = false;
            backToDashBtn.Click += backToDashBtn_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(44, 62, 80);
            panel1.Controls.Add(backToDashBtn);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1440, 70);
            panel1.TabIndex = 16;
            // 
            // sales_subtotalLabel
            // 
            sales_subtotalLabel.AutoSize = true;
            sales_subtotalLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            sales_subtotalLabel.ForeColor = Color.FromArgb(52, 73, 94);
            sales_subtotalLabel.Location = new Point(758, 542);
            sales_subtotalLabel.Name = "sales_subtotalLabel";
            sales_subtotalLabel.Size = new Size(90, 28);
            sales_subtotalLabel.TabIndex = 17;
            sales_subtotalLabel.Text = "SubTotal";
            // 
            // subTotalLabel
            // 
            subTotalLabel.AutoSize = true;
            subTotalLabel.Font = new Font("Segoe UI", 12F);
            subTotalLabel.ForeColor = Color.FromArgb(52, 73, 94);
            subTotalLabel.Location = new Point(1050, 440);
            subTotalLabel.Name = "subTotalLabel";
            subTotalLabel.Size = new Size(0, 28);
            subTotalLabel.TabIndex = 18;
            // 
            // Sales
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            Controls.Add(subTotalLabel);
            Controls.Add(sales_subtotalLabel);
            Controls.Add(panel1);
            Controls.Add(unitPriceTextBox);
            Controls.Add(priceLabel);
            Controls.Add(vatLabel);
            Controls.Add(totalLabel);
            Controls.Add(sales_vatLabel);
            Controls.Add(sales_totalLabel);
            Controls.Add(printReceiptBtn);
            Controls.Add(completesaleBtn);
            Controls.Add(saleGridView);
            Controls.Add(quantityTextBox);
            Controls.Add(addToCartBtn);
            Controls.Add(qtyLabel);
            Controls.Add(productComboBox);
            Controls.Add(productLabel);
            Controls.Add(salesentryheaderLabel);
            Name = "Sales";
            Size = new Size(1440, 700);
            ((System.ComponentModel.ISupportInitialize)saleGridView).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label salesentryheaderLabel;
        private Label productLabel;
        private ComboBox productComboBox;
        private Label qtyLabel;
        private Button addToCartBtn;
        private TextBox quantityTextBox;
        private DataGridView saleGridView;
        private Button completesaleBtn;
        private Button printReceiptBtn;
        private Label sales_totalLabel;
        private Label sales_vatLabel;
        private Label totalLabel;
        private Label vatLabel;
        private Label priceLabel;
        private TextBox unitPriceTextBox;
        private Button backToDashBtn;
        private Panel panel1;
        private Label sales_subtotalLabel;
        private Label subTotalLabel;
        private DataGridViewTextBoxColumn productName;
        private DataGridViewTextBoxColumn price;
        private DataGridViewTextBoxColumn quantity;
        private DataGridViewTextBoxColumn vatAmount;
        private DataGridViewTextBoxColumn totalPrice;
    }
}
