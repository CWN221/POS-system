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
            salesentryheaderLabel.Font = new Font("Segoe UI", 16F);
            salesentryheaderLabel.Location = new Point(38, 111);
            salesentryheaderLabel.Margin = new Padding(5, 0, 5, 0);
            salesentryheaderLabel.Name = "salesentryheaderLabel";
            salesentryheaderLabel.Size = new Size(144, 37);
            salesentryheaderLabel.TabIndex = 0;
            salesentryheaderLabel.Text = "Sales Entry";
            // 
            // productLabel
            // 
            productLabel.AutoSize = true;
            productLabel.Font = new Font("Segoe UI", 12F);
            productLabel.Location = new Point(38, 215);
            productLabel.Name = "productLabel";
            productLabel.Size = new Size(81, 28);
            productLabel.TabIndex = 1;
            productLabel.Text = "Product";
            // 
            // productComboBox
            // 
            productComboBox.FormattingEnabled = true;
            productComboBox.Location = new Point(146, 219);
            productComboBox.Name = "productComboBox";
            productComboBox.Size = new Size(363, 28);
            productComboBox.TabIndex = 2;
            productComboBox.SelectedIndexChanged += productComboBox_SelectedIndexChanged;
            // 
            // qtyLabel
            // 
            qtyLabel.AutoSize = true;
            qtyLabel.Font = new Font("Segoe UI", 12F);
            qtyLabel.Location = new Point(38, 316);
            qtyLabel.Name = "qtyLabel";
            qtyLabel.Size = new Size(88, 28);
            qtyLabel.TabIndex = 3;
            qtyLabel.Text = "Quantity";
            // 
            // addToCartBtn
            // 
            addToCartBtn.BackColor = SystemColors.MenuHighlight;
            addToCartBtn.Font = new Font("Segoe UI", 12F);
            addToCartBtn.ForeColor = Color.White;
            addToCartBtn.Location = new Point(39, 544);
            addToCartBtn.Name = "addToCartBtn";
            addToCartBtn.Size = new Size(145, 42);
            addToCartBtn.TabIndex = 4;
            addToCartBtn.Text = "Add To Cart";
            addToCartBtn.UseVisualStyleBackColor = false;
            addToCartBtn.Click += addToCartBtn_Click;
            // 
            // quantityTextBox
            // 
            quantityTextBox.Location = new Point(146, 316);
            quantityTextBox.Name = "quantityTextBox";
            quantityTextBox.Size = new Size(141, 27);
            quantityTextBox.TabIndex = 5;
            // 
            // saleGridView
            // 
            saleGridView.BackgroundColor = SystemColors.Window;
            saleGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            saleGridView.Columns.AddRange(new DataGridViewColumn[] { productName, price, quantity, vatAmount, totalPrice });
            saleGridView.GridColor = SystemColors.GradientActiveCaption;
            saleGridView.Location = new Point(598, 184);
            saleGridView.Name = "saleGridView";
            saleGridView.RowHeadersWidth = 51;
            saleGridView.Size = new Size(778, 371);
            saleGridView.TabIndex = 6;
            // 
            // productName
            // 
            productName.HeaderText = "Product Name";
            productName.MinimumWidth = 6;
            productName.Name = "productName";
            productName.ReadOnly = true;
            productName.Width = 310;
            // 
            // price
            // 
            price.HeaderText = "Price";
            price.MinimumWidth = 6;
            price.Name = "price";
            price.ReadOnly = true;
            price.Width = 110;
            // 
            // quantity
            // 
            quantity.HeaderText = "Quantity";
            quantity.MinimumWidth = 6;
            quantity.Name = "quantity";
            quantity.ReadOnly = true;
            quantity.Width = 75;
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
            totalPrice.Width = 110;
            // 
            // completesaleBtn
            // 
            completesaleBtn.BackColor = SystemColors.MenuHighlight;
            completesaleBtn.Font = new Font("Segoe UI", 10F);
            completesaleBtn.ForeColor = SystemColors.ButtonHighlight;
            completesaleBtn.Location = new Point(598, 761);
            completesaleBtn.Name = "completesaleBtn";
            completesaleBtn.Size = new Size(153, 40);
            completesaleBtn.TabIndex = 7;
            completesaleBtn.Text = "Complete Sale";
            completesaleBtn.UseVisualStyleBackColor = false;
            completesaleBtn.Click += completeSaleBtn;
            // 
            // printReceiptBtn
            // 
            printReceiptBtn.BackColor = SystemColors.MenuHighlight;
            printReceiptBtn.Font = new Font("Segoe UI", 10F);
            printReceiptBtn.ForeColor = SystemColors.ButtonHighlight;
            printReceiptBtn.Location = new Point(1033, 761);
            printReceiptBtn.Name = "printReceiptBtn";
            printReceiptBtn.Size = new Size(130, 40);
            printReceiptBtn.TabIndex = 8;
            printReceiptBtn.Text = "Print Receipt";
            printReceiptBtn.UseVisualStyleBackColor = false;
            printReceiptBtn.Visible = false;
            // 
            // sales_totalLabel
            // 
            sales_totalLabel.AutoSize = true;
            sales_totalLabel.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sales_totalLabel.Location = new Point(598, 703);
            sales_totalLabel.Name = "sales_totalLabel";
            sales_totalLabel.Size = new Size(56, 22);
            sales_totalLabel.TabIndex = 9;
            sales_totalLabel.Text = "Total";
            // 
            // sales_vatLabel
            // 
            sales_vatLabel.AutoSize = true;
            sales_vatLabel.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sales_vatLabel.Location = new Point(598, 638);
            sales_vatLabel.Name = "sales_vatLabel";
            sales_vatLabel.Size = new Size(49, 22);
            sales_vatLabel.TabIndex = 10;
            sales_vatLabel.Text = "VAT";
            // 
            // totalLabel
            // 
            totalLabel.AutoSize = true;
            totalLabel.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            totalLabel.Location = new Point(765, 703);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new Size(0, 20);
            totalLabel.TabIndex = 11;
            // 
            // vatLabel
            // 
            vatLabel.AutoSize = true;
            vatLabel.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            vatLabel.Location = new Point(765, 640);
            vatLabel.Name = "vatLabel";
            vatLabel.Size = new Size(0, 20);
            vatLabel.TabIndex = 12;
            // 
            // priceLabel
            // 
            priceLabel.AutoSize = true;
            priceLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            priceLabel.Location = new Point(39, 406);
            priceLabel.Name = "priceLabel";
            priceLabel.Size = new Size(54, 28);
            priceLabel.TabIndex = 13;
            priceLabel.Text = "Price";
            // 
            // unitPriceTextBox
            // 
            unitPriceTextBox.Location = new Point(146, 410);
            unitPriceTextBox.Name = "unitPriceTextBox";
            unitPriceTextBox.Size = new Size(141, 27);
            unitPriceTextBox.TabIndex = 14;
            // 
            // backToDashBtn
            // 
            backToDashBtn.BackColor = Color.FromArgb(55, 161, 218);
            backToDashBtn.BackgroundImageLayout = ImageLayout.Center;
            backToDashBtn.Cursor = Cursors.Hand;
            backToDashBtn.FlatStyle = FlatStyle.Popup;
            backToDashBtn.Font = new Font("Arial Narrow", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            backToDashBtn.ForeColor = Color.White;
            backToDashBtn.Image = Properties.Resources.dashboard_png;
            backToDashBtn.ImageAlign = ContentAlignment.MiddleLeft;
            backToDashBtn.Location = new Point(14, 15);
            backToDashBtn.Margin = new Padding(0, 3, 3, 3);
            backToDashBtn.Name = "backToDashBtn";
            backToDashBtn.RightToLeft = RightToLeft.No;
            backToDashBtn.Size = new Size(127, 47);
            backToDashBtn.TabIndex = 15;
            backToDashBtn.Text = "Dashboard";
            backToDashBtn.TextAlign = ContentAlignment.MiddleRight;
            backToDashBtn.UseVisualStyleBackColor = false;
            backToDashBtn.Click += backToDashBtn_Click;
            // 
            // panel1
            // 
            panel1.Controls.Add(backToDashBtn);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1379, 65);
            panel1.TabIndex = 16;
            // 
            // sales_subtotalLabel
            // 
            sales_subtotalLabel.AutoSize = true;
            sales_subtotalLabel.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            sales_subtotalLabel.Location = new Point(598, 586);
            sales_subtotalLabel.Name = "sales_subtotalLabel";
            sales_subtotalLabel.Size = new Size(91, 22);
            sales_subtotalLabel.TabIndex = 17;
            sales_subtotalLabel.Text = "SubTotal";
            // 
            // subTotalLabel
            // 
            subTotalLabel.AutoSize = true;
            subTotalLabel.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            subTotalLabel.Location = new Point(765, 586);
            subTotalLabel.Name = "subTotalLabel";
            subTotalLabel.Size = new Size(0, 22);
            subTotalLabel.TabIndex = 18;
            // 
            // Sales
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = Color.White;
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
            Size = new Size(1379, 823);
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
