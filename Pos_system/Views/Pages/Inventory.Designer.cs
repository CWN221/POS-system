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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            productViewModelBindingSource1 = new BindingSource(components);
            productViewModelBindingSource = new BindingSource(components);
            HeaderPanel = new Guna.UI2.WinForms.Guna2GradientPanel();
            BackToDashboardBtn = new Guna.UI2.WinForms.Guna2Button();
            branchLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            titleLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            TabControl = new Guna.UI2.WinForms.Guna2TabControl();
            viewpTabPage = new TabPage();
            InventoryGridView = new Guna.UI2.WinForms.Guna2DataGridView();
            tabPage2 = new TabPage();
            cancelBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            AddBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            headerinfo2Label = new Label();
            headerinfoLabel = new Label();
            lowstockthresholdTextBox = new TextBox();
            stockqtyTextBox = new TextBox();
            sellingpriceTextBox = new TextBox();
            lowStockLbl = new Label();
            stockQtyLbl = new Label();
            spLBL = new Label();
            skuTextBox = new TextBox();
            skulabel = new Label();
            pCategoryComboBox = new ComboBox();
            vatComboBox = new ComboBox();
            priceTextBox = new TextBox();
            pnameTextBox = new TextBox();
            stockCheckBox = new CheckBox();
            stockchkboxLbl = new Label();
            vatcategoryLabel = new Label();
            priceLbl = new Label();
            categoryLabel = new Label();
            nameLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource).BeginInit();
            HeaderPanel.SuspendLayout();
            TabControl.SuspendLayout();
            viewpTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)InventoryGridView).BeginInit();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // productViewModelBindingSource1
            // 
            productViewModelBindingSource1.DataSource = typeof(ViewModels.Pages.ProductViewModel);
            // 
            // productViewModelBindingSource
            // 
            productViewModelBindingSource.DataSource = typeof(ViewModels.Pages.ProductViewModel);
            // 
            // HeaderPanel
            // 
            HeaderPanel.Controls.Add(BackToDashboardBtn);
            HeaderPanel.Controls.Add(branchLabel);
            HeaderPanel.Controls.Add(titleLabel);
            HeaderPanel.CustomizableEdges = customizableEdges3;
            HeaderPanel.Dock = DockStyle.Top;
            HeaderPanel.FillColor = Color.FromArgb(244, 248, 254);
            HeaderPanel.FillColor2 = Color.FromArgb(224, 228, 234);
            HeaderPanel.Location = new Point(0, 0);
            HeaderPanel.Name = "HeaderPanel";
            HeaderPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            HeaderPanel.Size = new Size(1200, 72);
            HeaderPanel.TabIndex = 1;
            // 
            // BackToDashboardBtn
            // 
            BackToDashboardBtn.Anchor = AnchorStyles.Left;
            BackToDashboardBtn.BorderRadius = 7;
            BackToDashboardBtn.Cursor = Cursors.Hand;
            BackToDashboardBtn.CustomizableEdges = customizableEdges1;
            BackToDashboardBtn.DisabledState.BorderColor = Color.DarkGray;
            BackToDashboardBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            BackToDashboardBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            BackToDashboardBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            BackToDashboardBtn.FillColor = Color.FromArgb(111, 179, 255);
            BackToDashboardBtn.FocusedColor = Color.FromArgb(75, 146, 249);
            BackToDashboardBtn.Font = new Font("Arial", 10F);
            BackToDashboardBtn.ForeColor = Color.White;
            BackToDashboardBtn.HoverState.FillColor = Color.FromArgb(75, 146, 249);
            BackToDashboardBtn.HoverState.Font = new Font("Arial", 10.1F);
            BackToDashboardBtn.Image = Properties.Resources.dashboard_png;
            BackToDashboardBtn.Location = new Point(19, 12);
            BackToDashboardBtn.Name = "BackToDashboardBtn";
            BackToDashboardBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            BackToDashboardBtn.Size = new Size(143, 45);
            BackToDashboardBtn.TabIndex = 8;
            BackToDashboardBtn.Text = "Dashboard";
            BackToDashboardBtn.Click += backToDashboardBtn_Click;
            // 
            // branchLabel
            // 
            branchLabel.BackColor = Color.Transparent;
            branchLabel.Font = new Font("Arial", 10F);
            branchLabel.Location = new Point(961, 28);
            branchLabel.Name = "branchLabel";
            branchLabel.Size = new Size(30, 21);
            branchLabel.TabIndex = 1;
            branchLabel.Text = "N/A";
            // 
            // titleLabel
            // 
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Font = new Font("Arial", 11F, FontStyle.Bold);
            titleLabel.Location = new Point(509, 28);
            titleLabel.Name = "titleLabel";
            titleLabel.Padding = new Padding(3);
            titleLabel.Size = new Size(211, 29);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Inventory Management";
            // 
            // TabControl
            // 
            TabControl.Alignment = TabAlignment.Left;
            TabControl.Controls.Add(viewpTabPage);
            TabControl.Controls.Add(tabPage2);
            TabControl.Dock = DockStyle.Fill;
            TabControl.ItemSize = new Size(180, 40);
            TabControl.Location = new Point(0, 72);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            TabControl.Size = new Size(1200, 728);
            TabControl.TabButtonHoverState.BorderColor = Color.Empty;
            TabControl.TabButtonHoverState.FillColor = Color.FromArgb(40, 52, 70);
            TabControl.TabButtonHoverState.Font = new Font("Segoe UI Semibold", 10F);
            TabControl.TabButtonHoverState.ForeColor = Color.White;
            TabControl.TabButtonHoverState.InnerColor = Color.FromArgb(40, 52, 70);
            TabControl.TabButtonIdleState.BorderColor = Color.Empty;
            TabControl.TabButtonIdleState.FillColor = Color.FromArgb(33, 42, 57);
            TabControl.TabButtonIdleState.Font = new Font("Segoe UI Semibold", 10F);
            TabControl.TabButtonIdleState.ForeColor = Color.FromArgb(156, 160, 167);
            TabControl.TabButtonIdleState.InnerColor = Color.FromArgb(33, 42, 57);
            TabControl.TabButtonImageAlign = HorizontalAlignment.Left;
            TabControl.TabButtonSelectedState.BorderColor = Color.Empty;
            TabControl.TabButtonSelectedState.FillColor = Color.FromArgb(29, 37, 49);
            TabControl.TabButtonSelectedState.Font = new Font("Segoe UI Semibold", 10F);
            TabControl.TabButtonSelectedState.ForeColor = Color.White;
            TabControl.TabButtonSelectedState.InnerColor = Color.FromArgb(76, 132, 255);
            TabControl.TabButtonSize = new Size(180, 40);
            TabControl.TabIndex = 2;
            TabControl.TabMenuBackColor = Color.FromArgb(33, 42, 57);
            TabControl.TabIndexChanged += TabControl_SelectedIndexChanged;
            // 
            // viewpTabPage
            // 
            viewpTabPage.Controls.Add(InventoryGridView);
            viewpTabPage.Location = new Point(184, 4);
            viewpTabPage.Name = "viewpTabPage";
            viewpTabPage.Padding = new Padding(3);
            viewpTabPage.Size = new Size(1012, 720);
            viewpTabPage.TabIndex = 0;
            viewpTabPage.Text = "View Products";
            viewpTabPage.UseVisualStyleBackColor = true;
            // 
            // InventoryGridView
            // 
            InventoryGridView.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = Color.White;
            InventoryGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.MenuHighlight;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            InventoryGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            InventoryGridView.ColumnHeadersHeight = 50;
            InventoryGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(71, 69, 94);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            InventoryGridView.DefaultCellStyle = dataGridViewCellStyle3;
            InventoryGridView.Dock = DockStyle.Fill;
            InventoryGridView.GridColor = Color.FromArgb(231, 229, 255);
            InventoryGridView.Location = new Point(3, 3);
            InventoryGridView.Name = "InventoryGridView";
            InventoryGridView.RowHeadersVisible = false;
            InventoryGridView.RowHeadersWidth = 51;
            InventoryGridView.Size = new Size(1006, 714);
            InventoryGridView.TabIndex = 0;
            InventoryGridView.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            InventoryGridView.ThemeStyle.AlternatingRowsStyle.Font = null;
            InventoryGridView.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            InventoryGridView.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            InventoryGridView.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            InventoryGridView.ThemeStyle.BackColor = Color.White;
            InventoryGridView.ThemeStyle.GridColor = Color.FromArgb(231, 229, 255);
            InventoryGridView.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            InventoryGridView.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            InventoryGridView.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            InventoryGridView.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            InventoryGridView.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            InventoryGridView.ThemeStyle.HeaderStyle.Height = 50;
            InventoryGridView.ThemeStyle.ReadOnly = false;
            InventoryGridView.ThemeStyle.RowsStyle.BackColor = Color.White;
            InventoryGridView.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            InventoryGridView.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            InventoryGridView.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            InventoryGridView.ThemeStyle.RowsStyle.Height = 29;
            InventoryGridView.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            InventoryGridView.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(cancelBtn);
            tabPage2.Controls.Add(AddBtn);
            tabPage2.Controls.Add(headerinfo2Label);
            tabPage2.Controls.Add(headerinfoLabel);
            tabPage2.Controls.Add(lowstockthresholdTextBox);
            tabPage2.Controls.Add(stockqtyTextBox);
            tabPage2.Controls.Add(sellingpriceTextBox);
            tabPage2.Controls.Add(lowStockLbl);
            tabPage2.Controls.Add(stockQtyLbl);
            tabPage2.Controls.Add(spLBL);
            tabPage2.Controls.Add(skuTextBox);
            tabPage2.Controls.Add(skulabel);
            tabPage2.Controls.Add(pCategoryComboBox);
            tabPage2.Controls.Add(vatComboBox);
            tabPage2.Controls.Add(priceTextBox);
            tabPage2.Controls.Add(pnameTextBox);
            tabPage2.Controls.Add(stockCheckBox);
            tabPage2.Controls.Add(stockchkboxLbl);
            tabPage2.Controls.Add(vatcategoryLabel);
            tabPage2.Controls.Add(priceLbl);
            tabPage2.Controls.Add(categoryLabel);
            tabPage2.Controls.Add(nameLabel);
            tabPage2.Location = new Point(184, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1012, 720);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Add Product";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // cancelBtn
            // 
            cancelBtn.CustomizableEdges = customizableEdges5;
            cancelBtn.DisabledState.BorderColor = Color.DarkGray;
            cancelBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            cancelBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            cancelBtn.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            cancelBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            cancelBtn.FillColor = Color.FromArgb(79, 172, 254);
            cancelBtn.FillColor2 = Color.FromArgb(102, 126, 234);
            cancelBtn.Font = new Font("Segoe UI", 9F);
            cancelBtn.ForeColor = Color.White;
            cancelBtn.HoverState.FillColor = Color.FromArgb(102, 126, 234);
            cancelBtn.HoverState.FillColor2 = Color.FromArgb(118, 75, 162);
            cancelBtn.Location = new Point(600, 500);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.ShadowDecoration.CustomizableEdges = customizableEdges6;
            cancelBtn.Size = new Size(207, 48);
            cancelBtn.TabIndex = 68;
            cancelBtn.Text = "Cancel";
            cancelBtn.Click += cancelBtn_Click;
            // 
            // AddBtn
            // 
            AddBtn.CustomizableEdges = customizableEdges7;
            AddBtn.DisabledState.BorderColor = Color.DarkGray;
            AddBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            AddBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            AddBtn.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            AddBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            AddBtn.FillColor = Color.FromArgb(79, 172, 254);
            AddBtn.FillColor2 = Color.FromArgb(102, 126, 234);
            AddBtn.Font = new Font("Segoe UI", 9F);
            AddBtn.ForeColor = Color.White;
            AddBtn.HoverState.FillColor = Color.FromArgb(102, 126, 234);
            AddBtn.HoverState.FillColor2 = Color.FromArgb(118, 75, 162);
            AddBtn.Location = new Point(25, 500);
            AddBtn.Name = "AddBtn";
            AddBtn.ShadowDecoration.CustomizableEdges = customizableEdges8;
            AddBtn.Size = new Size(203, 48);
            AddBtn.TabIndex = 67;
            AddBtn.Text = "Add";
            AddBtn.Click += AddBtn_Click;
            // 
            // headerinfo2Label
            // 
            headerinfo2Label.AutoSize = true;
            headerinfo2Label.Font = new Font("Microsoft Sans Serif", 13F);
            headerinfo2Label.Location = new Point(600, 52);
            headerinfo2Label.Name = "headerinfo2Label";
            headerinfo2Label.Size = new Size(216, 26);
            headerinfo2Label.TabIndex = 66;
            headerinfo2Label.Text = "Pricing and Inventory";
            // 
            // headerinfoLabel
            // 
            headerinfoLabel.AutoSize = true;
            headerinfoLabel.Font = new Font("Microsoft Sans Serif", 13F);
            headerinfoLabel.Location = new Point(25, 52);
            headerinfoLabel.Name = "headerinfoLabel";
            headerinfoLabel.Size = new Size(129, 26);
            headerinfoLabel.TabIndex = 65;
            headerinfoLabel.Text = "Product Info";
            // 
            // lowstockthresholdTextBox
            // 
            lowstockthresholdTextBox.Location = new Point(801, 321);
            lowstockthresholdTextBox.Name = "lowstockthresholdTextBox";
            lowstockthresholdTextBox.Size = new Size(175, 27);
            lowstockthresholdTextBox.TabIndex = 63;
            // 
            // stockqtyTextBox
            // 
            stockqtyTextBox.Location = new Point(798, 258);
            stockqtyTextBox.Name = "stockqtyTextBox";
            stockqtyTextBox.Size = new Size(175, 27);
            stockqtyTextBox.TabIndex = 62;
            // 
            // sellingpriceTextBox
            // 
            sellingpriceTextBox.Location = new Point(798, 192);
            sellingpriceTextBox.Name = "sellingpriceTextBox";
            sellingpriceTextBox.Size = new Size(175, 27);
            sellingpriceTextBox.TabIndex = 61;
            // 
            // lowStockLbl
            // 
            lowStockLbl.AutoSize = true;
            lowStockLbl.Font = new Font("Microsoft Sans Serif", 11F);
            lowStockLbl.Location = new Point(603, 321);
            lowStockLbl.Name = "lowStockLbl";
            lowStockLbl.Size = new Size(159, 24);
            lowStockLbl.TabIndex = 60;
            lowStockLbl.Text = "Stock (Threshold)";
            // 
            // stockQtyLbl
            // 
            stockQtyLbl.AutoSize = true;
            stockQtyLbl.Font = new Font("Microsoft Sans Serif", 11F);
            stockQtyLbl.Location = new Point(600, 258);
            stockQtyLbl.Name = "stockQtyLbl";
            stockQtyLbl.Size = new Size(129, 24);
            stockQtyLbl.TabIndex = 59;
            stockQtyLbl.Text = "Stock Quantity";
            // 
            // spLBL
            // 
            spLBL.AutoSize = true;
            spLBL.Font = new Font("Microsoft Sans Serif", 11F);
            spLBL.Location = new Point(600, 198);
            spLBL.Name = "spLBL";
            spLBL.Size = new Size(115, 24);
            spLBL.TabIndex = 58;
            spLBL.Text = "Selling Price";
            // 
            // skuTextBox
            // 
            skuTextBox.Location = new Point(191, 190);
            skuTextBox.Name = "skuTextBox";
            skuTextBox.Size = new Size(358, 27);
            skuTextBox.TabIndex = 57;
            // 
            // skulabel
            // 
            skulabel.AutoSize = true;
            skulabel.Font = new Font("Microsoft Sans Serif", 11F);
            skulabel.Location = new Point(27, 190);
            skulabel.Name = "skulabel";
            skulabel.Size = new Size(47, 24);
            skulabel.TabIndex = 56;
            skulabel.Text = "SKU";
            // 
            // pCategoryComboBox
            // 
            pCategoryComboBox.FormattingEnabled = true;
            pCategoryComboBox.Location = new Point(191, 254);
            pCategoryComboBox.Name = "pCategoryComboBox";
            pCategoryComboBox.Size = new Size(358, 28);
            pCategoryComboBox.TabIndex = 55;
            // 
            // vatComboBox
            // 
            vatComboBox.FormattingEnabled = true;
            vatComboBox.Location = new Point(191, 357);
            vatComboBox.Name = "vatComboBox";
            vatComboBox.Size = new Size(358, 28);
            vatComboBox.TabIndex = 54;
            // 
            // priceTextBox
            // 
            priceTextBox.Location = new Point(798, 136);
            priceTextBox.Name = "priceTextBox";
            priceTextBox.Size = new Size(175, 27);
            priceTextBox.TabIndex = 53;
            // 
            // pnameTextBox
            // 
            pnameTextBox.Location = new Point(191, 130);
            pnameTextBox.Name = "pnameTextBox";
            pnameTextBox.Size = new Size(358, 27);
            pnameTextBox.TabIndex = 52;
            // 
            // stockCheckBox
            // 
            stockCheckBox.AutoSize = true;
            stockCheckBox.Font = new Font("Microsoft Sans Serif", 11F);
            stockCheckBox.Location = new Point(801, 372);
            stockCheckBox.Name = "stockCheckBox";
            stockCheckBox.Size = new Size(108, 28);
            stockCheckBox.TabIndex = 50;
            stockCheckBox.Text = "In Stock?";
            stockCheckBox.UseVisualStyleBackColor = true;
            // 
            // stockchkboxLbl
            // 
            stockchkboxLbl.AutoSize = true;
            stockchkboxLbl.Font = new Font("Microsoft Sans Serif", 11F);
            stockchkboxLbl.Location = new Point(603, 372);
            stockchkboxLbl.Name = "stockchkboxLbl";
            stockchkboxLbl.Size = new Size(56, 24);
            stockchkboxLbl.TabIndex = 49;
            stockchkboxLbl.Text = "Stock";
            // 
            // vatcategoryLabel
            // 
            vatcategoryLabel.AutoSize = true;
            vatcategoryLabel.Font = new Font("Microsoft Sans Serif", 11F);
            vatcategoryLabel.Location = new Point(25, 357);
            vatcategoryLabel.Name = "vatcategoryLabel";
            vatcategoryLabel.Size = new Size(128, 24);
            vatcategoryLabel.TabIndex = 48;
            vatcategoryLabel.Text = "VAT Category";
            // 
            // priceLbl
            // 
            priceLbl.AutoSize = true;
            priceLbl.Font = new Font("Microsoft Sans Serif", 11F);
            priceLbl.Location = new Point(603, 136);
            priceLbl.Name = "priceLbl";
            priceLbl.Size = new Size(53, 24);
            priceLbl.TabIndex = 47;
            priceLbl.Text = "Price";
            // 
            // categoryLabel
            // 
            categoryLabel.AutoSize = true;
            categoryLabel.Font = new Font("Microsoft Sans Serif", 11F);
            categoryLabel.Location = new Point(27, 254);
            categoryLabel.Name = "categoryLabel";
            categoryLabel.Size = new Size(85, 24);
            categoryLabel.TabIndex = 46;
            categoryLabel.Text = "Category";
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Font = new Font("Microsoft Sans Serif", 11F);
            nameLabel.Location = new Point(25, 130);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new Size(61, 24);
            nameLabel.TabIndex = 45;
            nameLabel.Text = "Name";
            // 
            // Inventory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(TabControl);
            Controls.Add(HeaderPanel);
            Name = "Inventory";
            Size = new Size(1200, 800);
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource1).EndInit();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource).EndInit();
            HeaderPanel.ResumeLayout(false);
            HeaderPanel.PerformLayout();
            TabControl.ResumeLayout(false);
            viewpTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)InventoryGridView).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private Label label3;
        private Label stockstatusLabel;
        private BindingSource productViewModelBindingSource;
        private BindingSource productViewModelBindingSource1;
        private Label lowstocklabel;
        private Label stockqtyLabel;
        private Label sellingPriceLabel;
        private Guna.UI2.WinForms.Guna2GradientPanel HeaderPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel branchLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel titleLabel;
        private Guna.UI2.WinForms.Guna2TabControl TabControl;
        private TabPage tabPage2;
        private Label headerinfo2Label;
        private Label headerinfoLabel;
        private TextBox lowstockthresholdTextBox;
        private TextBox stockqtyTextBox;
        private TextBox sellingpriceTextBox;
        private Label lowStockLbl;
        private Label stockQtyLbl;
        private Label spLBL;
        private TextBox skuTextBox;
        private Label skulabel;
        private ComboBox pCategoryComboBox;
        private ComboBox vatComboBox;
        private TextBox priceTextBox;
        private TextBox pnameTextBox;
        private CheckBox stockCheckBox;
        private Label stockchkboxLbl;
        private Label vatcategoryLabel;
        private Label priceLbl;
        private Label categoryLabel;
        private Label nameLabel;
        private Guna.UI2.WinForms.Guna2GradientButton cancelBtn;
        private Guna.UI2.WinForms.Guna2GradientButton AddBtn;
        private Guna.UI2.WinForms.Guna2Button BackToDashboardBtn;
        private Guna.UI2.WinForms.Guna2DataGridView InventoryGridView;
        private TabPage viewpTabPage;
    }
}
