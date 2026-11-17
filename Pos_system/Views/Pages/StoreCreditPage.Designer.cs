namespace Pos_system.Views.Pages
{
    partial class StoreCreditPage
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            headerPanel = new Guna.UI2.WinForms.Guna2GradientPanel();
            titleLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            branchLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            backBtn = new Guna.UI2.WinForms.Guna2Button();
            lookupCard = new Guna.UI2.WinForms.Guna2Panel();
            lookupRoot = new TableLayoutPanel();
            phoneCaption = new Label();
            phoneBox = new Guna.UI2.WinForms.Guna2TextBox();
            lookupBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            balanceHost = new Panel();
            balanceCaption = new Label();
            balanceValue = new Label();
            historyCard = new Guna.UI2.WinForms.Guna2Panel();
            historyRoot = new TableLayoutPanel();
            historyHeaderPanel = new Panel();
            entryCountLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            historyHeaderLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            historyGrid = new Guna.UI2.WinForms.Guna2DataGridView();
            headerPanel.SuspendLayout();
            lookupCard.SuspendLayout();
            lookupRoot.SuspendLayout();
            balanceHost.SuspendLayout();
            historyCard.SuspendLayout();
            historyRoot.SuspendLayout();
            historyHeaderPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)historyGrid).BeginInit();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(titleLbl);
            headerPanel.Controls.Add(branchLbl);
            headerPanel.Controls.Add(backBtn);
            headerPanel.CustomizableEdges = customizableEdges3;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.FillColor = Color.White;
            headerPanel.FillColor2 = Color.White;
            headerPanel.Location = new Point(20, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            headerPanel.Size = new Size(1326, 68);
            headerPanel.TabIndex = 0;
            // 
            // titleLbl
            // 
            titleLbl.BackColor = Color.Transparent;
            titleLbl.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            titleLbl.ForeColor = Color.FromArgb(15, 23, 42);
            titleLbl.Location = new Point(184, 17);
            titleLbl.Name = "titleLbl";
            titleLbl.Size = new Size(166, 43);
            titleLbl.TabIndex = 0;
            titleLbl.Text = "Store Credit";
            // 
            // branchLbl
            // 
            branchLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            branchLbl.BackColor = Color.Transparent;
            branchLbl.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            branchLbl.ForeColor = Color.FromArgb(37, 99, 235);
            branchLbl.Location = new Point(1126, 24);
            branchLbl.Name = "branchLbl";
            branchLbl.Size = new Size(82, 25);
            branchLbl.TabIndex = 1;
            branchLbl.Text = "Branch: —";
            branchLbl.TextAlignment = ContentAlignment.TopRight;
            // 
            // backBtn
            // 
            backBtn.Anchor = AnchorStyles.Left;
            backBtn.BorderRadius = 10;
            backBtn.Cursor = Cursors.Hand;
            backBtn.CustomizableEdges = customizableEdges1;
            backBtn.FillColor = Color.FromArgb(239, 246, 255);
            backBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            backBtn.ForeColor = Color.FromArgb(37, 99, 235);
            backBtn.HoverState.FillColor = Color.FromArgb(219, 234, 254);
            backBtn.Location = new Point(20, 14);
            backBtn.Name = "backBtn";
            backBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            backBtn.Size = new Size(140, 40);
            backBtn.TabIndex = 2;
            backBtn.Text = "← Dashboard";
            backBtn.Click += BackBtn_Click;
            // 
            // lookupCard
            // 
            lookupCard.BorderColor = Color.FromArgb(226, 232, 240);
            lookupCard.BorderRadius = 14;
            lookupCard.BorderThickness = 1;
            lookupCard.Controls.Add(lookupRoot);
            lookupCard.CustomizableEdges = customizableEdges9;
            lookupCard.Dock = DockStyle.Top;
            lookupCard.FillColor = Color.White;
            lookupCard.Location = new Point(20, 68);
            lookupCard.Name = "lookupCard";
            lookupCard.ShadowDecoration.CustomizableEdges = customizableEdges10;
            lookupCard.Size = new Size(1326, 180);
            lookupCard.TabIndex = 1;
            // 
            // lookupRoot
            // 
            lookupRoot.ColumnCount = 3;
            lookupRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            lookupRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            lookupRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            lookupRoot.Controls.Add(phoneCaption, 0, 0);
            lookupRoot.Controls.Add(phoneBox, 0, 1);
            lookupRoot.Controls.Add(lookupBtn, 1, 1);
            lookupRoot.Controls.Add(balanceHost, 2, 0);
            lookupRoot.Dock = DockStyle.Fill;
            lookupRoot.Location = new Point(0, 0);
            lookupRoot.Name = "lookupRoot";
            lookupRoot.Padding = new Padding(24);
            lookupRoot.RowCount = 2;
            lookupRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            lookupRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            lookupRoot.Size = new Size(1326, 180);
            lookupRoot.TabIndex = 0;
            // 
            // phoneCaption
            // 
            phoneCaption.AutoSize = true;
            phoneCaption.Dock = DockStyle.Fill;
            phoneCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            phoneCaption.ForeColor = Color.FromArgb(51, 65, 85);
            phoneCaption.Location = new Point(27, 24);
            phoneCaption.Name = "phoneCaption";
            phoneCaption.Size = new Size(505, 24);
            phoneCaption.TabIndex = 0;
            phoneCaption.Text = "Customer Phone Number";
            // 
            // phoneBox
            // 
            phoneBox.BorderColor = Color.FromArgb(203, 213, 225);
            phoneBox.BorderRadius = 8;
            phoneBox.CustomizableEdges = customizableEdges5;
            phoneBox.DefaultText = "";
            phoneBox.Dock = DockStyle.Fill;
            phoneBox.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            phoneBox.Font = new Font("Segoe UI", 10F);
            phoneBox.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            phoneBox.Location = new Point(27, 53);
            phoneBox.Margin = new Padding(3, 5, 3, 5);
            phoneBox.Name = "phoneBox";
            phoneBox.PlaceholderForeColor = Color.FromArgb(148, 163, 184);
            phoneBox.PlaceholderText = "";
            phoneBox.SelectedText = "";
            phoneBox.ShadowDecoration.CustomizableEdges = customizableEdges6;
            phoneBox.Size = new Size(505, 98);
            phoneBox.TabIndex = 1;
            // 
            // lookupBtn
            // 
            lookupBtn.BorderRadius = 10;
            lookupBtn.CustomizableEdges = customizableEdges7;
            lookupBtn.Dock = DockStyle.Fill;
            lookupBtn.FillColor = Color.FromArgb(37, 99, 235);
            lookupBtn.FillColor2 = Color.FromArgb(37, 99, 235);
            lookupBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            lookupBtn.ForeColor = Color.White;
            lookupBtn.Location = new Point(538, 53);
            lookupBtn.Margin = new Padding(3, 5, 3, 5);
            lookupBtn.Name = "lookupBtn";
            lookupBtn.ShadowDecoration.CustomizableEdges = customizableEdges8;
            lookupBtn.Size = new Size(377, 98);
            lookupBtn.TabIndex = 2;
            lookupBtn.Text = "Look Up Balance";
            lookupBtn.Click += LookupBtn_Click;
            // 
            // balanceHost
            // 
            balanceHost.BackColor = Color.FromArgb(248, 250, 252);
            balanceHost.BorderStyle = BorderStyle.FixedSingle;
            balanceHost.Controls.Add(balanceCaption);
            balanceHost.Controls.Add(balanceValue);
            balanceHost.Dock = DockStyle.Fill;
            balanceHost.Location = new Point(934, 24);
            balanceHost.Margin = new Padding(16, 0, 0, 0);
            balanceHost.Name = "balanceHost";
            lookupRoot.SetRowSpan(balanceHost, 2);
            balanceHost.Size = new Size(368, 132);
            balanceHost.TabIndex = 3;
            // 
            // balanceCaption
            // 
            balanceCaption.AutoSize = true;
            balanceCaption.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            balanceCaption.ForeColor = Color.FromArgb(100, 116, 139);
            balanceCaption.Location = new Point(20, 20);
            balanceCaption.Name = "balanceCaption";
            balanceCaption.Size = new Size(164, 23);
            balanceCaption.TabIndex = 0;
            balanceCaption.Text = "CURRENT BALANCE";
            // 
            // balanceValue
            // 
            balanceValue.AutoSize = true;
            balanceValue.Font = new Font("Segoe UI Semibold", 22F, FontStyle.Bold);
            balanceValue.ForeColor = Color.Black;
            balanceValue.Location = new Point(20, 55);
            balanceValue.Name = "balanceValue";
            balanceValue.Size = new Size(59, 50);
            balanceValue.TabIndex = 1;
            balanceValue.Text = "—";
            // 
            // historyCard
            // 
            historyCard.BorderColor = Color.FromArgb(226, 232, 240);
            historyCard.BorderRadius = 14;
            historyCard.BorderThickness = 1;
            historyCard.Controls.Add(historyRoot);
            historyCard.CustomizableEdges = customizableEdges11;
            historyCard.Dock = DockStyle.Fill;
            historyCard.FillColor = Color.White;
            historyCard.Location = new Point(20, 248);
            historyCard.Name = "historyCard";
            historyCard.ShadowDecoration.CustomizableEdges = customizableEdges12;
            historyCard.Size = new Size(1326, 500);
            historyCard.TabIndex = 2;
            // 
            // historyRoot
            // 
            historyRoot.ColumnCount = 1;
            historyRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            historyRoot.Controls.Add(historyHeaderPanel, 0, 0);
            historyRoot.Controls.Add(historyGrid, 0, 1);
            historyRoot.Dock = DockStyle.Fill;
            historyRoot.Location = new Point(0, 0);
            historyRoot.Name = "historyRoot";
            historyRoot.Padding = new Padding(24);
            historyRoot.RowCount = 2;
            historyRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            historyRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            historyRoot.Size = new Size(1326, 500);
            historyRoot.TabIndex = 0;
            // 
            // historyHeaderPanel
            // 
            historyHeaderPanel.Controls.Add(entryCountLbl);
            historyHeaderPanel.Controls.Add(historyHeaderLbl);
            historyHeaderPanel.Dock = DockStyle.Fill;
            historyHeaderPanel.Location = new Point(27, 27);
            historyHeaderPanel.Name = "historyHeaderPanel";
            historyHeaderPanel.Size = new Size(1272, 34);
            historyHeaderPanel.TabIndex = 0;
            // 
            // entryCountLbl
            // 
            entryCountLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            entryCountLbl.BackColor = Color.Transparent;
            entryCountLbl.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            entryCountLbl.ForeColor = Color.FromArgb(100, 116, 139);
            entryCountLbl.Location = new Point(1100, 8);
            entryCountLbl.Name = "entryCountLbl";
            entryCountLbl.Size = new Size(68, 25);
            entryCountLbl.TabIndex = 1;
            entryCountLbl.Text = "0 entries";
            entryCountLbl.TextAlignment = ContentAlignment.TopRight;
            // 
            // historyHeaderLbl
            // 
            historyHeaderLbl.BackColor = Color.Transparent;
            historyHeaderLbl.Font = new Font("Segoe UI Semibold", 13F, FontStyle.Bold);
            historyHeaderLbl.ForeColor = Color.FromArgb(15, 23, 42);
            historyHeaderLbl.Location = new Point(0, 4);
            historyHeaderLbl.Name = "historyHeaderLbl";
            historyHeaderLbl.Size = new Size(194, 32);
            historyHeaderLbl.TabIndex = 0;
            historyHeaderLbl.Text = "Transaction History";
            // 
            // historyGrid
            // 
            historyGrid.AllowUserToAddRows = false;
            historyGrid.AllowUserToDeleteRows = false;
            historyGrid.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(250, 251, 253);
            historyGrid.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle2.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.FromArgb(71, 85, 105);
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(248, 250, 252);
            dataGridViewCellStyle2.SelectionForeColor = Color.FromArgb(71, 85, 105);
            historyGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            historyGrid.ColumnHeadersHeight = 42;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.White;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 10F);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(30, 41, 59);
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(239, 246, 255);
            dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(37, 99, 235);
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            historyGrid.DefaultCellStyle = dataGridViewCellStyle3;
            historyGrid.Dock = DockStyle.Fill;
            historyGrid.GridColor = Color.FromArgb(241, 245, 249);
            historyGrid.Location = new Point(27, 67);
            historyGrid.Name = "historyGrid";
            historyGrid.ReadOnly = true;
            historyGrid.RowHeadersVisible = false;
            historyGrid.RowHeadersWidth = 51;
            historyGrid.RowTemplate.Height = 38;
            historyGrid.Size = new Size(1272, 406);
            historyGrid.TabIndex = 1;
            historyGrid.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            historyGrid.ThemeStyle.AlternatingRowsStyle.Font = null;
            historyGrid.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            historyGrid.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            historyGrid.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            historyGrid.ThemeStyle.BackColor = Color.White;
            historyGrid.ThemeStyle.GridColor = Color.FromArgb(241, 245, 249);
            historyGrid.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            historyGrid.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            historyGrid.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            historyGrid.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            historyGrid.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            historyGrid.ThemeStyle.HeaderStyle.Height = 42;
            historyGrid.ThemeStyle.ReadOnly = true;
            historyGrid.ThemeStyle.RowsStyle.BackColor = Color.White;
            historyGrid.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            historyGrid.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            historyGrid.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            historyGrid.ThemeStyle.RowsStyle.Height = 38;
            historyGrid.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            historyGrid.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // StoreCreditPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(historyCard);
            Controls.Add(lookupCard);
            Controls.Add(headerPanel);
            Name = "StoreCreditPage";
            Padding = new Padding(20, 0, 20, 20);
            Size = new Size(1366, 768);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            lookupCard.ResumeLayout(false);
            lookupRoot.ResumeLayout(false);
            lookupRoot.PerformLayout();
            balanceHost.ResumeLayout(false);
            balanceHost.PerformLayout();
            historyCard.ResumeLayout(false);
            historyRoot.ResumeLayout(false);
            historyHeaderPanel.ResumeLayout(false);
            historyHeaderPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)historyGrid).EndInit();
            ResumeLayout(false);
        }

        private Guna.UI2.WinForms.Guna2GradientPanel headerPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel titleLbl;
        private Guna.UI2.WinForms.Guna2HtmlLabel branchLbl;
        private Guna.UI2.WinForms.Guna2Button backBtn;
        private Guna.UI2.WinForms.Guna2Panel lookupCard;
        private TableLayoutPanel lookupRoot;
        private Label phoneCaption;
        private Guna.UI2.WinForms.Guna2TextBox phoneBox;
        private Guna.UI2.WinForms.Guna2GradientButton lookupBtn;
        private Panel balanceHost;
        private Label balanceCaption;
        private Label balanceValue;
        private Guna.UI2.WinForms.Guna2Panel historyCard;
        private TableLayoutPanel historyRoot;
        private Panel historyHeaderPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel historyHeaderLbl;
        private Guna.UI2.WinForms.Guna2HtmlLabel entryCountLbl;
        private Guna.UI2.WinForms.Guna2DataGridView historyGrid;
    }
}