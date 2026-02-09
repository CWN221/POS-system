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
            label1 = new Label();
            label2 = new Label();
            productComboBox = new ComboBox();
            label3 = new Label();
            addToCartBtn = new Button();
            quantityTextBox = new TextBox();
            saleGridView = new DataGridView();
            productName = new DataGridViewTextBoxColumn();
            price = new DataGridViewTextBoxColumn();
            quantity = new DataGridViewTextBoxColumn();
            vatAmount = new DataGridViewTextBoxColumn();
            totalPrice = new DataGridViewTextBoxColumn();
            button2 = new Button();
            button3 = new Button();
            label4 = new Label();
            label5 = new Label();
            totalLabel = new Label();
            vatLabel = new Label();
            label6 = new Label();
            unitPriceTextBox = new TextBox();
            backToDashBtn = new Button();
            panel1 = new Panel();
            label8 = new Label();
            subTotalLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)saleGridView).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F);
            label1.Location = new Point(86, 104);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(129, 32);
            label1.TabIndex = 0;
            label1.Text = "Sales Entry";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(86, 196);
            label2.Name = "label2";
            label2.Size = new Size(81, 28);
            label2.TabIndex = 1;
            label2.Text = "Product";
            // 
            // productComboBox
            // 
            productComboBox.FormattingEnabled = true;
            productComboBox.Location = new Point(235, 200);
            productComboBox.Name = "productComboBox";
            productComboBox.Size = new Size(363, 28);
            productComboBox.TabIndex = 2;
            productComboBox.SelectedIndexChanged += productComboBox_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(86, 307);
            label3.Name = "label3";
            label3.Size = new Size(88, 28);
            label3.TabIndex = 3;
            label3.Text = "Quantity";
            // 
            // addToCartBtn
            // 
            addToCartBtn.BackColor = SystemColors.MenuHighlight;
            addToCartBtn.Font = new Font("Segoe UI", 12F);
            addToCartBtn.ForeColor = Color.White;
            addToCartBtn.Location = new Point(86, 492);
            addToCartBtn.Name = "addToCartBtn";
            addToCartBtn.Size = new Size(145, 42);
            addToCartBtn.TabIndex = 4;
            addToCartBtn.Text = "Add To Cart";
            addToCartBtn.UseVisualStyleBackColor = false;
            addToCartBtn.Click += addToCartBtn_Click;
            // 
            // quantityTextBox
            // 
            quantityTextBox.Location = new Point(235, 307);
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
            saleGridView.Location = new Point(711, 196);
            saleGridView.Name = "saleGridView";
            saleGridView.RowHeadersWidth = 51;
            saleGridView.Size = new Size(719, 371);
            saleGridView.TabIndex = 6;
            // 
            // productName
            // 
            productName.HeaderText = "Product Name";
            productName.MinimumWidth = 6;
            productName.Name = "productName";
            productName.ReadOnly = true;
            productName.Width = 200;
            // 
            // price
            // 
            price.HeaderText = "Price";
            price.MinimumWidth = 6;
            price.Name = "price";
            price.ReadOnly = true;
            price.Width = 125;
            // 
            // quantity
            // 
            quantity.HeaderText = "Quantity";
            quantity.MinimumWidth = 6;
            quantity.Name = "quantity";
            quantity.ReadOnly = true;
            quantity.Width = 90;
            // 
            // vatAmount
            // 
            vatAmount.HeaderText = "VAT Amount";
            vatAmount.MinimumWidth = 6;
            vatAmount.Name = "vatAmount";
            vatAmount.ReadOnly = true;
            vatAmount.Width = 125;
            // 
            // totalPrice
            // 
            totalPrice.HeaderText = "Total Price";
            totalPrice.MinimumWidth = 6;
            totalPrice.Name = "totalPrice";
            totalPrice.ReadOnly = true;
            totalPrice.Width = 125;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.MenuHighlight;
            button2.Font = new Font("Segoe UI", 10F);
            button2.ForeColor = SystemColors.ButtonHighlight;
            button2.Location = new Point(737, 780);
            button2.Name = "button2";
            button2.Size = new Size(153, 40);
            button2.TabIndex = 7;
            button2.Text = "Complete Sale";
            button2.UseVisualStyleBackColor = false;
            button2.Click += completeSaleBtn;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.MenuHighlight;
            button3.Font = new Font("Segoe UI", 10F);
            button3.ForeColor = SystemColors.ButtonHighlight;
            button3.Location = new Point(1172, 780);
            button3.Name = "button3";
            button3.Size = new Size(130, 40);
            button3.TabIndex = 8;
            button3.Text = "Print Receipt";
            button3.UseVisualStyleBackColor = false;
            button3.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(737, 722);
            label4.Name = "label4";
            label4.Size = new Size(56, 22);
            label4.TabIndex = 9;
            label4.Text = "Total";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(737, 657);
            label5.Name = "label5";
            label5.Size = new Size(49, 22);
            label5.TabIndex = 10;
            label5.Text = "VAT";
            // 
            // totalLabel
            // 
            totalLabel.AutoSize = true;
            totalLabel.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            totalLabel.Location = new Point(904, 722);
            totalLabel.Name = "totalLabel";
            totalLabel.Size = new Size(0, 20);
            totalLabel.TabIndex = 11;
            // 
            // vatLabel
            // 
            vatLabel.AutoSize = true;
            vatLabel.Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            vatLabel.Location = new Point(904, 659);
            vatLabel.Name = "vatLabel";
            vatLabel.Size = new Size(0, 20);
            vatLabel.TabIndex = 12;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(87, 383);
            label6.Name = "label6";
            label6.Size = new Size(54, 28);
            label6.TabIndex = 13;
            label6.Text = "Price";
            // 
            // unitPriceTextBox
            // 
            unitPriceTextBox.Location = new Point(235, 387);
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
            panel1.Size = new Size(1800, 65);
            panel1.TabIndex = 16;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(737, 605);
            label8.Name = "label8";
            label8.Size = new Size(91, 22);
            label8.TabIndex = 17;
            label8.Text = "SubTotal";
            // 
            // subTotalLabel
            // 
            subTotalLabel.AutoSize = true;
            subTotalLabel.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            subTotalLabel.Location = new Point(904, 605);
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
            Controls.Add(label8);
            Controls.Add(panel1);
            Controls.Add(unitPriceTextBox);
            Controls.Add(label6);
            Controls.Add(vatLabel);
            Controls.Add(totalLabel);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(saleGridView);
            Controls.Add(quantityTextBox);
            Controls.Add(addToCartBtn);
            Controls.Add(label3);
            Controls.Add(productComboBox);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Sales";
            Size = new Size(1800, 1000);
            ((System.ComponentModel.ISupportInitialize)saleGridView).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private ComboBox productComboBox;
        private Label label3;
        private Button addToCartBtn;
        private TextBox quantityTextBox;
        private DataGridView saleGridView;
        private Button button2;
        private Button button3;
        private Label label4;
        private Label label5;
        private Label totalLabel;
        private Label vatLabel;
        private Label label6;
        private TextBox unitPriceTextBox;
        private Button backToDashBtn;
        private DataGridViewTextBoxColumn productName;
        private DataGridViewTextBoxColumn price;
        private DataGridViewTextBoxColumn quantity;
        private DataGridViewTextBoxColumn vatAmount;
        private DataGridViewTextBoxColumn totalPrice;
        private Panel panel1;
        private Label label8;
        private Label subTotalLabel;
    }
}
