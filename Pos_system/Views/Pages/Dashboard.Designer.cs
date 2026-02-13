namespace Pos_system.Views.Pages
{
    partial class Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Dashboard));
            panelSidebar = new Panel();
            logoutLabel = new Label();
            logoutBtn = new Button();
            settingsLabel = new Label();
            reportsLabel = new Label();
            saleslabelPanel = new Label();
            inventoryLabel = new Label();
            settingsNavBtn = new Button();
            reportsNavBtn = new Button();
            salesNavBtn = new Button();
            inventoryNavBtn = new Button();
            pictureBox1 = new PictureBox();
            panelTopbar = new Panel();
            label4 = new Label();
            label1 = new Label();
            latestTransactions = new Panel();
            label2 = new Label();
            dataGridView1 = new DataGridView();
            transactionDAOBindingSource = new BindingSource(components);
            panel4 = new Panel();
            pictureBox3 = new PictureBox();
            totalStockLabel = new Label();
            stockLabel = new Label();
            panel3 = new Panel();
            pictureBox2 = new PictureBox();
            totalSalesLabel = new Label();
            salesLabel = new Label();
            panelSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelTopbar.SuspendLayout();
            latestTransactions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)transactionDAOBindingSource).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.White;
            panelSidebar.Controls.Add(logoutLabel);
            panelSidebar.Controls.Add(logoutBtn);
            panelSidebar.Controls.Add(settingsLabel);
            panelSidebar.Controls.Add(reportsLabel);
            panelSidebar.Controls.Add(saleslabelPanel);
            panelSidebar.Controls.Add(inventoryLabel);
            panelSidebar.Controls.Add(settingsNavBtn);
            panelSidebar.Controls.Add(reportsNavBtn);
            panelSidebar.Controls.Add(salesNavBtn);
            panelSidebar.Controls.Add(inventoryNavBtn);
            panelSidebar.Controls.Add(pictureBox1);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 0);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(235, 800);
            panelSidebar.TabIndex = 0;
            // 
            // logoutLabel
            // 
            logoutLabel.AutoSize = true;
            logoutLabel.Font = new Font("Tahoma", 11F);
            logoutLabel.Location = new Point(135, 746);
            logoutLabel.Name = "logoutLabel";
            logoutLabel.Size = new Size(67, 23);
            logoutLabel.TabIndex = 11;
            logoutLabel.Text = "Logout";
            // 
            // logoutBtn
            // 
            logoutBtn.BackgroundImage = (Image)resources.GetObject("logoutBtn.BackgroundImage");
            logoutBtn.BackgroundImageLayout = ImageLayout.Zoom;
            logoutBtn.Location = new Point(37, 733);
            logoutBtn.Name = "logoutBtn";
            logoutBtn.Size = new Size(50, 50);
            logoutBtn.TabIndex = 10;
            logoutBtn.UseVisualStyleBackColor = true;
            logoutBtn.Click += logoutNavBtn_Click;
            // 
            // settingsLabel
            // 
            settingsLabel.AutoSize = true;
            settingsLabel.Font = new Font("Tahoma", 11F);
            settingsLabel.Location = new Point(135, 614);
            settingsLabel.Name = "settingsLabel";
            settingsLabel.Size = new Size(77, 23);
            settingsLabel.TabIndex = 9;
            settingsLabel.Text = "Settings";
            // 
            // reportsLabel
            // 
            reportsLabel.AutoSize = true;
            reportsLabel.Font = new Font("Tahoma", 11F);
            reportsLabel.Location = new Point(135, 479);
            reportsLabel.Name = "reportsLabel";
            reportsLabel.Size = new Size(74, 23);
            reportsLabel.TabIndex = 8;
            reportsLabel.Text = "Reports";
            // 
            // saleslabelPanel
            // 
            saleslabelPanel.AutoSize = true;
            saleslabelPanel.Font = new Font("Tahoma", 11F);
            saleslabelPanel.Location = new Point(135, 360);
            saleslabelPanel.Name = "saleslabelPanel";
            saleslabelPanel.Size = new Size(53, 23);
            saleslabelPanel.TabIndex = 7;
            saleslabelPanel.Text = "Sales";
            // 
            // inventoryLabel
            // 
            inventoryLabel.AutoSize = true;
            inventoryLabel.Font = new Font("Tahoma", 11F);
            inventoryLabel.Location = new Point(135, 236);
            inventoryLabel.Name = "inventoryLabel";
            inventoryLabel.Size = new Size(90, 23);
            inventoryLabel.TabIndex = 6;
            inventoryLabel.Text = "Inventory";
            // 
            // settingsNavBtn
            // 
            settingsNavBtn.BackgroundImage = (Image)resources.GetObject("settingsNavBtn.BackgroundImage");
            settingsNavBtn.BackgroundImageLayout = ImageLayout.Zoom;
            settingsNavBtn.Cursor = Cursors.Hand;
            settingsNavBtn.Location = new Point(20, 578);
            settingsNavBtn.Name = "settingsNavBtn";
            settingsNavBtn.Size = new Size(80, 80);
            settingsNavBtn.TabIndex = 5;
            settingsNavBtn.UseVisualStyleBackColor = true;
            settingsNavBtn.Click += settingsNavBtn_Click;
            // 
            // reportsNavBtn
            // 
            reportsNavBtn.BackgroundImage = (Image)resources.GetObject("reportsNavBtn.BackgroundImage");
            reportsNavBtn.BackgroundImageLayout = ImageLayout.Zoom;
            reportsNavBtn.Cursor = Cursors.Hand;
            reportsNavBtn.ImageAlign = ContentAlignment.TopCenter;
            reportsNavBtn.Location = new Point(20, 449);
            reportsNavBtn.Name = "reportsNavBtn";
            reportsNavBtn.Size = new Size(80, 80);
            reportsNavBtn.TabIndex = 4;
            reportsNavBtn.TextAlign = ContentAlignment.BottomCenter;
            reportsNavBtn.UseVisualStyleBackColor = true;
            reportsNavBtn.Click += reportsNavBtn_Click;
            // 
            // salesNavBtn
            // 
            salesNavBtn.BackgroundImage = (Image)resources.GetObject("salesNavBtn.BackgroundImage");
            salesNavBtn.BackgroundImageLayout = ImageLayout.Zoom;
            salesNavBtn.Cursor = Cursors.Hand;
            salesNavBtn.FlatAppearance.BorderSize = 0;
            salesNavBtn.FlatStyle = FlatStyle.Flat;
            salesNavBtn.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            salesNavBtn.ImageAlign = ContentAlignment.TopCenter;
            salesNavBtn.Location = new Point(20, 317);
            salesNavBtn.Name = "salesNavBtn";
            salesNavBtn.Size = new Size(80, 80);
            salesNavBtn.TabIndex = 3;
            salesNavBtn.TextAlign = ContentAlignment.BottomCenter;
            salesNavBtn.UseVisualStyleBackColor = true;
            salesNavBtn.Click += salesNavBtn_Click;
            // 
            // inventoryNavBtn
            // 
            inventoryNavBtn.BackgroundImage = (Image)resources.GetObject("inventoryNavBtn.BackgroundImage");
            inventoryNavBtn.BackgroundImageLayout = ImageLayout.Zoom;
            inventoryNavBtn.Cursor = Cursors.Hand;
            inventoryNavBtn.FlatAppearance.BorderSize = 0;
            inventoryNavBtn.FlatStyle = FlatStyle.Flat;
            inventoryNavBtn.Font = new Font("Microsoft Sans Serif", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            inventoryNavBtn.ImageAlign = ContentAlignment.TopCenter;
            inventoryNavBtn.Location = new Point(20, 205);
            inventoryNavBtn.Name = "inventoryNavBtn";
            inventoryNavBtn.Size = new Size(80, 80);
            inventoryNavBtn.TabIndex = 1;
            inventoryNavBtn.TextAlign = ContentAlignment.BottomCenter;
            inventoryNavBtn.UseVisualStyleBackColor = true;
            inventoryNavBtn.Click += InventoryNavBtn_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(20, 20);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(168, 148);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // panelTopbar
            // 
            panelTopbar.BackColor = SystemColors.ControlLightLight;
            panelTopbar.Controls.Add(label4);
            panelTopbar.Controls.Add(label1);
            panelTopbar.Dock = DockStyle.Top;
            panelTopbar.Location = new Point(235, 0);
            panelTopbar.Name = "panelTopbar";
            panelTopbar.Size = new Size(1137, 80);
            panelTopbar.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(379, 35);
            label4.Name = "label4";
            label4.Size = new Size(185, 32);
            label4.TabIndex = 1;
            label4.Text = "POS System";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(25, 35);
            label1.Name = "label1";
            label1.Size = new Size(230, 36);
            label1.TabIndex = 0;
            label1.Text = "Welcome, User";
            // 
            // latestTransactions
            // 
            latestTransactions.BackColor = Color.FromArgb(248, 249, 250);
            latestTransactions.Controls.Add(label2);
            latestTransactions.Controls.Add(dataGridView1);
            latestTransactions.Controls.Add(panel4);
            latestTransactions.Controls.Add(panel3);
            latestTransactions.Dock = DockStyle.Fill;
            latestTransactions.Location = new Point(235, 80);
            latestTransactions.Name = "latestTransactions";
            latestTransactions.Size = new Size(1137, 720);
            latestTransactions.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(593, 101);
            label2.Name = "label2";
            label2.Size = new Size(262, 32);
            label2.TabIndex = 4;
            label2.Text = "Latest Transactions";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.DataSource = transactionDAOBindingSource;
            dataGridView1.Location = new Point(593, 136);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(543, 431);
            dataGridView1.TabIndex = 3;
            // 
            // transactionDAOBindingSource
            // 
            transactionDAOBindingSource.DataSource = typeof(Services.TransactionDAO);
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(197, 226, 241);
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(pictureBox3);
            panel4.Controls.Add(totalStockLabel);
            panel4.Controls.Add(stockLabel);
            panel4.Location = new Point(48, 385);
            panel4.Name = "panel4";
            panel4.Size = new Size(349, 182);
            panel4.TabIndex = 2;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = (Image)resources.GetObject("pictureBox3.Image");
            pictureBox3.Location = new Point(0, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(125, 110);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.TabIndex = 2;
            pictureBox3.TabStop = false;
            // 
            // totalStockLabel
            // 
            totalStockLabel.AutoSize = true;
            totalStockLabel.Font = new Font("Agency FB", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            totalStockLabel.Location = new Point(164, 143);
            totalStockLabel.Name = "totalStockLabel";
            totalStockLabel.Size = new Size(73, 28);
            totalStockLabel.TabIndex = 1;
            totalStockLabel.Text = "Loading...";
            // 
            // stockLabel
            // 
            stockLabel.AutoSize = true;
            stockLabel.Font = new Font("Agency FB", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            stockLabel.Location = new Point(28, 143);
            stockLabel.Name = "stockLabel";
            stockLabel.Size = new Size(110, 28);
            stockLabel.TabIndex = 0;
            stockLabel.Text = "Stock Quantity";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(197, 226, 241);
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(pictureBox2);
            panel3.Controls.Add(totalSalesLabel);
            panel3.Controls.Add(salesLabel);
            panel3.Location = new Point(48, 125);
            panel3.Name = "panel3";
            panel3.Size = new Size(349, 188);
            panel3.TabIndex = 1;
            // 
            // pictureBox2
            // 
            pictureBox2.Anchor = AnchorStyles.None;
            pictureBox2.BackgroundImageLayout = ImageLayout.Center;
            pictureBox2.Image = (Image)resources.GetObject("pictureBox2.Image");
            pictureBox2.Location = new Point(3, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(122, 96);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // totalSalesLabel
            // 
            totalSalesLabel.AutoSize = true;
            totalSalesLabel.Font = new Font("Agency FB", 14F);
            totalSalesLabel.Location = new Point(164, 137);
            totalSalesLabel.Name = "totalSalesLabel";
            totalSalesLabel.Size = new Size(80, 28);
            totalSalesLabel.TabIndex = 1;
            totalSalesLabel.Text = "Loading...";
            // 
            // salesLabel
            // 
            salesLabel.AutoSize = true;
            salesLabel.Font = new Font("Agency FB", 14F);
            salesLabel.ForeColor = Color.Black;
            salesLabel.Location = new Point(28, 137);
            salesLabel.Name = "salesLabel";
            salesLabel.Size = new Size(93, 28);
            salesLabel.TabIndex = 0;
            salesLabel.Text = "Total Sales";
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(latestTransactions);
            Controls.Add(panelTopbar);
            Controls.Add(panelSidebar);
            Name = "Dashboard";
            Size = new Size(1372, 800);
            panelSidebar.ResumeLayout(false);
            panelSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelTopbar.ResumeLayout(false);
            panelTopbar.PerformLayout();
            latestTransactions.ResumeLayout(false);
            latestTransactions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)transactionDAOBindingSource).EndInit();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSidebar;
        private Button salesNavBtn;
        private Button inventoryNavBtn;
        private PictureBox pictureBox1;
        private Panel panelTopbar;
        private Label label1;
        private Panel latestTransactions;
        private Label totalSalesLabel;
        private Label salesLabel;
        private Label label4;
        private Panel panel3;
        private Panel panel4;
        private Label totalStockLabel;
        private Label stockLabel;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private DataGridView dataGridView1;
        private Label label2;
        private BindingSource transactionDAOBindingSource;
        private Button reportsNavBtn;
        private Button settingsNavBtn;
        private Label inventoryLabel;
        private Label saleslabelPanel;
        private Label settingsLabel;
        private Label reportsLabel;
        private Button logoutBtn;
        private Label logoutLabel;
    }
}
