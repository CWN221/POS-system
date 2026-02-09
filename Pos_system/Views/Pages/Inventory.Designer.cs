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
            backToDashboardBtn = new Button();
            tabPage2 = new TabPage();
            pCategoryComboBox = new ComboBox();
            vatComboBox = new ComboBox();
            priceTextBox = new TextBox();
            pnameTextBox = new TextBox();
            AddBtn = new Button();
            stockCheckBox = new CheckBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            productViewModelBindingSource = new BindingSource(components);
            pictureBox1 = new PictureBox();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource1).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tabControl1.Location = new Point(0, 0);
            tabControl1.Margin = new Padding(3, 6, 3, 6);
            tabControl1.Name = "tabControl1";
            tabControl1.Padding = new Point(16, 10);
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1024, 768);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Controls.Add(backToDashboardBtn);
            tabPage1.Location = new Point(4, 48);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1016, 716);
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
            dataGridView1.Location = new Point(15, 83);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(973, 632);
            dataGridView1.TabIndex = 2;
            // 
            // productViewModelBindingSource1
            // 
            productViewModelBindingSource1.DataSource = typeof(ViewModels.Pages.ProductViewModel);
            // 
            // backToDashboardBtn
            // 
            backToDashboardBtn.BackColor = Color.FromArgb(55, 161, 218);
            backToDashboardBtn.Cursor = Cursors.Hand;
            backToDashboardBtn.Font = new Font("Arial Narrow", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            backToDashboardBtn.ForeColor = Color.White;
            backToDashboardBtn.Image = Properties.Resources.dashboard_png;
            backToDashboardBtn.ImageAlign = ContentAlignment.MiddleLeft;
            backToDashboardBtn.Location = new Point(15, 6);
            backToDashboardBtn.Name = "backToDashboardBtn";
            backToDashboardBtn.Size = new Size(127, 45);
            backToDashboardBtn.TabIndex = 1;
            backToDashboardBtn.Text = "Dashboard";
            backToDashboardBtn.TextAlign = ContentAlignment.MiddleRight;
            backToDashboardBtn.UseVisualStyleBackColor = false;
            backToDashboardBtn.Click += backToDashboardBtn_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(pictureBox1);
            tabPage2.Controls.Add(pCategoryComboBox);
            tabPage2.Controls.Add(vatComboBox);
            tabPage2.Controls.Add(priceTextBox);
            tabPage2.Controls.Add(pnameTextBox);
            tabPage2.Controls.Add(AddBtn);
            tabPage2.Controls.Add(stockCheckBox);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new Point(4, 48);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1016, 716);
            tabPage2.TabIndex = 3;
            tabPage2.Text = "Add Product";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // pCategoryComboBox
            // 
            pCategoryComboBox.FormattingEnabled = true;
            pCategoryComboBox.Location = new Point(225, 273);
            pCategoryComboBox.Name = "pCategoryComboBox";
            pCategoryComboBox.Size = new Size(252, 33);
            pCategoryComboBox.TabIndex = 10;
            // 
            // vatComboBox
            // 
            vatComboBox.FormattingEnabled = true;
            vatComboBox.Location = new Point(225, 425);
            vatComboBox.Name = "vatComboBox";
            vatComboBox.Size = new Size(252, 33);
            vatComboBox.TabIndex = 9;
            // 
            // priceTextBox
            // 
            priceTextBox.Location = new Point(225, 343);
            priceTextBox.Name = "priceTextBox";
            priceTextBox.Size = new Size(252, 30);
            priceTextBox.TabIndex = 8;
            // 
            // pnameTextBox
            // 
            pnameTextBox.Location = new Point(225, 210);
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
            AddBtn.Location = new Point(225, 597);
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
            stockCheckBox.Location = new Point(225, 493);
            stockCheckBox.Name = "stockCheckBox";
            stockCheckBox.Size = new Size(116, 29);
            stockCheckBox.TabIndex = 5;
            stockCheckBox.Text = "In Stock?";
            stockCheckBox.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(59, 497);
            label5.Name = "label5";
            label5.Size = new Size(62, 25);
            label5.TabIndex = 4;
            label5.Text = "Stock";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(59, 425);
            label4.Name = "label4";
            label4.Size = new Size(138, 25);
            label4.TabIndex = 3;
            label4.Text = "VAT Category";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(59, 350);
            label3.Name = "label3";
            label3.Size = new Size(56, 25);
            label3.TabIndex = 2;
            label3.Text = "Price";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 273);
            label2.Name = "label2";
            label2.Size = new Size(92, 25);
            label2.TabIndex = 1;
            label2.Text = "Category";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(59, 210);
            label1.Name = "label1";
            label1.Size = new Size(136, 25);
            label1.TabIndex = 0;
            label1.Text = "Product Name";
            // 
            // productViewModelBindingSource
            // 
            productViewModelBindingSource.DataSource = typeof(ViewModels.Pages.ProductViewModel);
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImageLayout = ImageLayout.None;
            pictureBox1.Image = Properties.Resources.new_product_icon;
            pictureBox1.Location = new Point(59, 35);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(165, 127);
            pictureBox1.TabIndex = 11;
            pictureBox1.TabStop = false;
            // 
            // Inventory
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tabControl1);
            Name = "Inventory";
            Size = new Size(1024, 768);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource1).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)productViewModelBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Label label1;
        private Label label3;
        private Label label2;
        private CheckBox stockCheckBox;
        private Label label5;
        private Label label4;
        private TextBox priceTextBox;
        private TextBox pnameTextBox;
        private Button AddBtn;
        private ComboBox pCategoryComboBox;
        private ComboBox vatComboBox;
        private BindingSource productViewModelBindingSource;
        private Button backToDashboardBtn;
        private BindingSource productViewModelBindingSource1;
        private DataGridView dataGridView1;
        private PictureBox pictureBox1;
    }
}
