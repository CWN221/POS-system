namespace Pos_system.Views.Pages
{
    partial class Inventory
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
            components = new System.ComponentModel.Container();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            dataGridView1 = new DataGridView();
            productViewModelBindingSource1 = new BindingSource(components);
            tabPage2 = new TabPage();
            headerinfo2Label = new Label();
            headerinfoLabel = new Label();
            cancelBtn = new Button();
            lowstockthresholdTextBox = new TextBox();
            stockqtyTextBox = new TextBox();
            sellingpriceTextBox = new TextBox();
            lowstocklabel = new Label();
            stockqtyLabel = new Label();
            sellingPriceLabel = new Label();
            skuTextBox = new TextBox();
            skulabel = new Label();
            pCategoryComboBox = new ComboBox();
            vatComboBox = new ComboBox();
            priceTextBox = new TextBox();
            pnameTextBox = new TextBox();
            AddBtn = new Button();
            stockCheckBox = new CheckBox();
            stockstatusLabel = new Label();
            vatcategoryLabel = new Label();
            label3 = new Label();
            categoryLabel = new Label();
            nameLabel = new Label();
            backToDashboardBtn = new Button();
            productViewModelBindingSource = new BindingSource(components);
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource1).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.None;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(16, 67);
            tabControl1.Margin = new Padding(3, 6, 3, 6);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(22, 7);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1171, 727);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 42);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1163, 681);
            tabPage1.TabIndex = 2;
            tabPage1.Text = "View Products";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.DataSource = productViewModelBindingSource1;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(1157, 675);
            dataGridView1.TabIndex = 2;
            // 
            // productViewModelBindingSource1
            // 
            productViewModelBindingSource1.DataSource = typeof(ViewModels.Pages.ProductViewModel);
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(headerinfo2Label);
            tabPage2.Controls.Add(headerinfoLabel);
            tabPage2.Controls.Add(cancelBtn);
            tabPage2.Controls.Add(lowstockthresholdTextBox);
            tabPage2.Controls.Add(stockqtyTextBox);
            tabPage2.Controls.Add(sellingpriceTextBox);
            tabPage2.Controls.Add(lowstocklabel);
            tabPage2.Controls.Add(stockqtyLabel);
            tabPage2.Controls.Add(sellingPriceLabel);
            tabPage2.Controls.Add(skuTextBox);
            tabPage2.Controls.Add(skulabel);
            tabPage2.Controls.Add(pCategoryComboBox);
            tabPage2.Controls.Add(vatComboBox);
            tabPage2.Controls.Add(priceTextBox);
            tabPage2.Controls.Add(pnameTextBox);
            tabPage2.Controls.Add(AddBtn);
            tabPage2.Controls.Add(stockCheckBox);
            tabPage2.Controls.Add(stockstatusLabel);
            tabPage2.Controls.Add(vatcategoryLabel);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(categoryLabel);
            tabPage2.Controls.Add(nameLabel);
            tabPage2.Location = new Point(4, 42);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1163, 681);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "Add Product";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // headerinfo2Label
            // 
            headerinfo2Label.AutoSize = true;
            headerinfo2Label.Font = new Font("Microsoft Sans Serif", 13F);
            headerinfo2Label.Location = new Point(702, 93);
            headerinfo2Label.Name = "headerinfo2Label";
            headerinfo2Label.Size = new Size(216, 26);
            headerinfo2Label.TabIndex = 22;
            headerinfo2Label.Text = "Pricing and Inventory";
            // 
            // headerinfoLabel
            // 
            headerinfoLabel.AutoSize = true;
            headerinfoLabel.Font = new Font("Microsoft Sans Serif", 13F);
            headerinfoLabel.Location = new Point(59, 93);
            headerinfoLabel.Name = "headerinfoLabel";
            headerinfoLabel.Size = new Size(129, 26);
            headerinfoLabel.TabIndex = 21;
            headerinfoLabel.Text = "Product Info";
            // 
            // cancelBtn
            // 
            cancelBtn.BackColor = Color.FromArgb(55, 161, 218);
            cancelBtn.Cursor = Cursors.Hand;
            cancelBtn.FlatStyle = FlatStyle.Popup;
            cancelBtn.Font = new Font("Arial Narrow", 12F, FontStyle.Bold);
            cancelBtn.ForeColor = Color.White;
            cancelBtn.Location = new Point(702, 597);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(174, 36);
            cancelBtn.TabIndex = 20;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = false;
            cancelBtn.Click += cancelBtn_Click;
            // 
            // lowstockthresholdTextBox
            // 
            lowstockthresholdTextBox.Location = new Point(900, 392);
            lowstockthresholdTextBox.Name = "lowstockthresholdTextBox";
            lowstockthresholdTextBox.Size = new Size(175, 30);
            lowstockthresholdTextBox.TabIndex = 19;
            // 
            // stockqtyTextBox
            // 
            stockqtyTextBox.Location = new Point(900, 346);
            stockqtyTextBox.Name = "stockqtyTextBox";
            stockqtyTextBox.Size = new Size(175, 30);
            stockqtyTextBox.TabIndex = 18;
            // 
            // sellingpriceTextBox
            // 
            sellingpriceTextBox.Location = new Point(900, 233);
            sellingpriceTextBox.Name = "sellingpriceTextBox";
            sellingpriceTextBox.Size = new Size(175, 30);
            sellingpriceTextBox.TabIndex = 17;
            // 
            // lowstocklabel
            // 
            lowstocklabel.AutoSize = true;
            lowstocklabel.Font = new Font("Microsoft Sans Serif", 11F);
            lowstocklabel.Location = new Point(702, 392);
            lowstocklabel.Name = "lowstocklabel";
            lowstocklabel.Size = new Size(199, 24);
            lowstocklabel.TabIndex = 16;
            lowstocklabel.Text = "Low Stock (Threshold)";
            // 
            // stockqtyLabel
            // 
            stockqtyLabel.AutoSize = true;
            stockqtyLabel.Font = new Font("Microsoft Sans Serif", 11F);
            stockqtyLabel.Location = new Point(702, 346);
            stockqtyLabel.Name = "stockqtyLabel";
            stockqtyLabel.Size = new Size(129, 24);
            stockqtyLabel.TabIndex = 15;
            stockqtyLabel.Text = "Stock Quantity";
            // 
            // sellingPriceLabel
            // 
            sellingPriceLabel.AutoSize = true;
            sellingPriceLabel.Font = new Font("Microsoft Sans Serif", 11F);
            sellingPriceLabel.Location = new Point(702, 239);
            sellingPriceLabel.Name = "sellingPriceLabel";
            sellingPriceLabel.Size = new Size(115, 24);
            sellingPriceLabel.TabIndex = 14;
            sellingPriceLabel.Text = "Selling Price";
            // 
            // skuTextBox
            // 
            skuTextBox.Location = new Point(225, 231);
            skuTextBox.Name = "skuTextBox";
            skuTextBox.Size = new Size(358, 30);
            skuTextBox.TabIndex = 13;
            // 
            // skulabel
            // 
            skulabel.AutoSize = true;
            skulabel.Font = new Font("Microsoft Sans Serif", 11F);
            skulabel.Location = new Point(61, 231);
            skulabel.Name = "skulabel";
            skulabel.Size = new Size(47, 24);
            skulabel.TabIndex = 12;
            skulabel.Text = "SKU";
            // 
            // pCategoryComboBox
            // 
            pCategoryComboBox.FormattingEnabled = true;
            pCategoryComboBox.Location = new Point(225, 295);
            pCategoryComboBox.Name = "pCategoryComboBox";
            pCategoryComboBox.Size = new Size(358, 33);
            pCategoryComboBox.TabIndex = 10;
            // 
            // vatComboBox
            // 
            vatComboBox.FormattingEnabled = true;
            vatComboBox.Location = new Point(225, 398);
            vatComboBox.Name = "vatComboBox";
            vatComboBox.Size = new Size(358, 33);
            vatComboBox.TabIndex = 9;
            // 
            // priceTextBox
            // 
            priceTextBox.Location = new Point(900, 177);
            priceTextBox.Name = "priceTextBox";
            priceTextBox.Size = new Size(175, 30);
            priceTextBox.TabIndex = 8;
            // 
            // pnameTextBox
            // 
            pnameTextBox.Location = new Point(225, 171);
            pnameTextBox.Name = "pnameTextBox";
            pnameTextBox.Size = new Size(358, 30);
            pnameTextBox.TabIndex = 7;
            // 
            // AddBtn
            // 
            AddBtn.BackColor = Color.FromArgb(55, 161, 218);
            AddBtn.Cursor = Cursors.Hand;
            AddBtn.FlatStyle = FlatStyle.Popup;
            AddBtn.Font = new Font("Arial Narrow", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddBtn.ForeColor = Color.White;
            AddBtn.Location = new Point(61, 597);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(174, 36);
            AddBtn.TabIndex = 6;
            AddBtn.Text = "Add";
            AddBtn.UseVisualStyleBackColor = false;
            AddBtn.Click += AddBtn_Click;
            // 
            // stockCheckBox
            // 
            stockCheckBox.AutoSize = true;
            stockCheckBox.Font = new Font("Microsoft Sans Serif", 11F);
            stockCheckBox.Location = new Point(900, 443);
            stockCheckBox.Name = "stockCheckBox";
            stockCheckBox.Size = new Size(108, 28);
            stockCheckBox.TabIndex = 5;
            stockCheckBox.Text = "In Stock?";
            stockCheckBox.UseVisualStyleBackColor = true;
            // 
            // stockstatusLabel
            // 
            stockstatusLabel.AutoSize = true;
            stockstatusLabel.Font = new Font("Microsoft Sans Serif", 11F);
            stockstatusLabel.Location = new Point(702, 443);
            stockstatusLabel.Name = "stockstatusLabel";
            stockstatusLabel.Size = new Size(56, 24);
            stockstatusLabel.TabIndex = 4;
            stockstatusLabel.Text = "Stock";
            // 
            // vatcategoryLabel
            // 
            vatcategoryLabel.AutoSize = true;
            vatcategoryLabel.Font = new Font("Microsoft Sans Serif", 11F);
            vatcategoryLabel.Location = new Point(59, 398);
            vatcategoryLabel.Name = "vatcategoryLabel";
            vatcategoryLabel.Size = new Size(128, 24);
            vatcategoryLabel.TabIndex = 3;
            vatcategoryLabel.Text = "VAT Category";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 11F);
            label3.Location = new Point(705, 177);
            label3.Name = "label3";
            label3.Size = new Size(53, 24);
            label3.TabIndex = 2;
            label3.Text = "Price";
            // 
            // categoryLabel
            // 
            categoryLabel.AutoSize = true;
            categoryLabel.Font = new Font("Microsoft Sans Serif", 11F);
            categoryLabel.Location = new Point(61, 295);
            categoryLabel.Name = "categoryLabel";
            categoryLabel.Size = new Size(85, 24);
            categoryLabel.TabIndex = 1;
            categoryLabel.Text = "Category";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("Microsoft Sans Serif", 11F);
            nameLabel.Location = new Point(59, 171);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(61, 24);
            nameLabel.TabIndex = 0;
            nameLabel.Text = "Name";
            // 
            // backToDashboardBtn
            // 
            backToDashboardBtn.BackColor = Color.FromArgb(55, 161, 218);
            backToDashboardBtn.Cursor = Cursors.Hand;
            backToDashboardBtn.Font = new Font("Arial Narrow", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            backToDashboardBtn.ForeColor = Color.White;
            backToDashboardBtn.Image = Properties.Resources.dashboard_png;
            backToDashboardBtn.ImageAlign = ContentAlignment.MiddleLeft;
            backToDashboardBtn.Location = new Point(14, 13);
            backToDashboardBtn.Name = "backToDashboardBtn";
            backToDashboardBtn.Size = new Size(127, 45);
            backToDashboardBtn.TabIndex = 1;
            backToDashboardBtn.Text = "Dashboard";
            backToDashboardBtn.TextAlign = ContentAlignment.MiddleRight;
            backToDashboardBtn.UseVisualStyleBackColor = false;
            backToDashboardBtn.Click += backToDashboardBtn_Click;
            // 
            // productViewModelBindingSource
            // 
            productViewModelBindingSource.DataSource = typeof(ViewModels.Pages.ProductViewModel);
            // 
            // Inventory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Controls.Add(backToDashboardBtn);
            Name = "Inventory";
            Size = new Size(1200, 800);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label nameLabel;
        private Label label3;
        private Label categoryLabel;
        private CheckBox stockCheckBox;
        private Label stockstatusLabel;
        private Label vatcategoryLabel;
        private TextBox priceTextBox;
        private TextBox pnameTextBox;
        private Button AddBtn;
        private ComboBox pCategoryComboBox;
        private ComboBox vatComboBox;
        private BindingSource productViewModelBindingSource;
        private Button backToDashboardBtn;
        private BindingSource productViewModelBindingSource1;
        private DataGridView dataGridView1;
        private TextBox skuTextBox;
        private Label skulabel;
        private TextBox stockqtyTextBox;
        private TextBox sellingpriceTextBox;
        private Label lowstocklabel;
        private Label stockqtyLabel;
        private Label sellingPriceLabel;
        private TextBox lowstockthresholdTextBox;
        private Button cancelBtn;
        private Label headerinfo2Label;
        private Label headerinfoLabel;
    }
}
