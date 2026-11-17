namespace Pos_system.Views.Pages
{
    partial class BranchSwitcherPage
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            headerPanel = new Guna.UI2.WinForms.Guna2GradientPanel();
            currentBranchHeaderLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            titleLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            backToDashboardBtn = new Guna.UI2.WinForms.Guna2Button();
            bodyLayout = new TableLayoutPanel();
            switchCard = new Guna.UI2.WinForms.Guna2Panel();
            switchRoot = new TableLayoutPanel();
            switchHeaderPanel = new Panel();
            switchHeaderLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            currentBranchCaption = new Label();
            lblCurrentBranch = new Label();
            selectBranchCaption = new Label();
            BranchesComboBox = new Guna.UI2.WinForms.Guna2ComboBox();
            switchButtonsPanel = new Panel();
            CancelBtn = new Guna.UI2.WinForms.Guna2Button();
            SwitchBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            infoCard = new Guna.UI2.WinForms.Guna2Panel();
            infoRoot = new TableLayoutPanel();
            infoHeaderPanel = new Panel();
            infoHeaderLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            userCaption = new Label();
            userValue = new Label();
            roleCaption = new Label();
            RoleDisplayLbl = new Label();
            accessCaption = new Label();
            AllowedBranchesLbl = new Label();
            headerPanel.SuspendLayout();
            bodyLayout.SuspendLayout();
            switchCard.SuspendLayout();
            switchRoot.SuspendLayout();
            switchHeaderPanel.SuspendLayout();
            switchButtonsPanel.SuspendLayout();
            infoCard.SuspendLayout();
            infoRoot.SuspendLayout();
            infoHeaderPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(currentBranchHeaderLbl);
            headerPanel.Controls.Add(titleLbl);
            headerPanel.Controls.Add(backToDashboardBtn);
            headerPanel.CustomizableEdges = customizableEdges3;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.FillColor = Color.White;
            headerPanel.FillColor2 = Color.White;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            headerPanel.Size = new Size(1024, 68);
            headerPanel.TabIndex = 0;
            // 
            // currentBranchHeaderLbl
            // 
            currentBranchHeaderLbl.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            currentBranchHeaderLbl.BackColor = Color.Transparent;
            currentBranchHeaderLbl.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            currentBranchHeaderLbl.ForeColor = Color.FromArgb(37, 99, 235);
            currentBranchHeaderLbl.Location = new Point(764, 24);
            currentBranchHeaderLbl.Name = "currentBranchHeaderLbl";
            currentBranchHeaderLbl.Size = new Size(87, 25);
            currentBranchHeaderLbl.TabIndex = 1;
            currentBranchHeaderLbl.Text = "Current: —";
            currentBranchHeaderLbl.TextAlignment = ContentAlignment.TopRight;
            // 
            // titleLbl
            // 
            titleLbl.BackColor = Color.Transparent;
            titleLbl.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            titleLbl.ForeColor = Color.FromArgb(15, 23, 42);
            titleLbl.Location = new Point(184, 17);
            titleLbl.Name = "titleLbl";
            titleLbl.Size = new Size(194, 43);
            titleLbl.TabIndex = 0;
            titleLbl.Text = "Switch Branch";
            // 
            // backToDashboardBtn
            // 
            backToDashboardBtn.Anchor = AnchorStyles.Left;
            backToDashboardBtn.BorderRadius = 10;
            backToDashboardBtn.Cursor = Cursors.Hand;
            backToDashboardBtn.CustomizableEdges = customizableEdges1;
            backToDashboardBtn.DisabledState.BorderColor = Color.DarkGray;
            backToDashboardBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            backToDashboardBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            backToDashboardBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            backToDashboardBtn.FillColor = Color.FromArgb(239, 246, 255);
            backToDashboardBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            backToDashboardBtn.ForeColor = Color.FromArgb(37, 99, 235);
            backToDashboardBtn.HoverState.FillColor = Color.FromArgb(219, 234, 254);
            backToDashboardBtn.Location = new Point(20, 14);
            backToDashboardBtn.Name = "backToDashboardBtn";
            backToDashboardBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            backToDashboardBtn.Size = new Size(140, 40);
            backToDashboardBtn.TabIndex = 2;
            backToDashboardBtn.Text = "← Dashboard";
            backToDashboardBtn.Click += BackToDashboardBtn_Click;
            // 
            // bodyLayout
            // 
            bodyLayout.ColumnCount = 2;
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            bodyLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            bodyLayout.Controls.Add(switchCard, 0, 0);
            bodyLayout.Controls.Add(infoCard, 1, 0);
            bodyLayout.Dock = DockStyle.Fill;
            bodyLayout.Location = new Point(0, 68);
            bodyLayout.Name = "bodyLayout";
            bodyLayout.Padding = new Padding(20, 16, 20, 20);
            bodyLayout.RowCount = 1;
            bodyLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            bodyLayout.Size = new Size(1024, 700);
            bodyLayout.TabIndex = 1;
            // 
            // switchCard
            // 
            switchCard.BorderColor = Color.FromArgb(226, 232, 240);
            switchCard.BorderRadius = 14;
            switchCard.BorderThickness = 1;
            switchCard.Controls.Add(switchRoot);
            switchCard.CustomizableEdges = customizableEdges11;
            switchCard.Dock = DockStyle.Fill;
            switchCard.FillColor = Color.White;
            switchCard.Location = new Point(20, 16);
            switchCard.Margin = new Padding(0, 0, 12, 0);
            switchCard.Name = "switchCard";
            switchCard.ShadowDecoration.CustomizableEdges = customizableEdges12;
            switchCard.Size = new Size(529, 664);
            switchCard.TabIndex = 0;
            // 
            // switchRoot
            // 
            switchRoot.ColumnCount = 1;
            switchRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            switchRoot.Controls.Add(switchHeaderPanel, 0, 0);
            switchRoot.Controls.Add(currentBranchCaption, 0, 1);
            switchRoot.Controls.Add(lblCurrentBranch, 0, 2);
            switchRoot.Controls.Add(selectBranchCaption, 0, 3);
            switchRoot.Controls.Add(BranchesComboBox, 0, 4);
            switchRoot.Controls.Add(switchButtonsPanel, 0, 5);
            switchRoot.Dock = DockStyle.Fill;
            switchRoot.Location = new Point(0, 0);
            switchRoot.Name = "switchRoot";
            switchRoot.Padding = new Padding(28);
            switchRoot.RowCount = 6;
            switchRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            switchRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            switchRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 44F));
            switchRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            switchRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 54F));
            switchRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            switchRoot.Size = new Size(529, 664);
            switchRoot.TabIndex = 0;
            // 
            // switchHeaderPanel
            // 
            switchHeaderPanel.BackColor = Color.Transparent;
            switchHeaderPanel.Controls.Add(switchHeaderLbl);
            switchHeaderPanel.Dock = DockStyle.Fill;
            switchHeaderPanel.Location = new Point(31, 31);
            switchHeaderPanel.Name = "switchHeaderPanel";
            switchHeaderPanel.Size = new Size(467, 40);
            switchHeaderPanel.TabIndex = 0;
            // 
            // switchHeaderLbl
            // 
            switchHeaderLbl.BackColor = Color.Transparent;
            switchHeaderLbl.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            switchHeaderLbl.ForeColor = Color.FromArgb(15, 23, 42);
            switchHeaderLbl.Location = new Point(0, 4);
            switchHeaderLbl.Name = "switchHeaderLbl";
            switchHeaderLbl.Size = new Size(213, 33);
            switchHeaderLbl.TabIndex = 0;
            switchHeaderLbl.Text = "Select Active Branch";
            // 
            // currentBranchCaption
            // 
            currentBranchCaption.AutoSize = true;
            currentBranchCaption.Dock = DockStyle.Fill;
            currentBranchCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            currentBranchCaption.ForeColor = Color.FromArgb(100, 116, 139);
            currentBranchCaption.Location = new Point(31, 74);
            currentBranchCaption.Name = "currentBranchCaption";
            currentBranchCaption.Size = new Size(467, 24);
            currentBranchCaption.TabIndex = 1;
            currentBranchCaption.Text = "CURRENT BRANCH";
            // 
            // lblCurrentBranch
            // 
            lblCurrentBranch.AutoSize = true;
            lblCurrentBranch.Dock = DockStyle.Fill;
            lblCurrentBranch.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            lblCurrentBranch.ForeColor = Color.FromArgb(37, 99, 235);
            lblCurrentBranch.Location = new Point(31, 98);
            lblCurrentBranch.Name = "lblCurrentBranch";
            lblCurrentBranch.Size = new Size(467, 44);
            lblCurrentBranch.TabIndex = 2;
            lblCurrentBranch.Text = "—";
            // 
            // selectBranchCaption
            // 
            selectBranchCaption.AutoSize = true;
            selectBranchCaption.Dock = DockStyle.Fill;
            selectBranchCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            selectBranchCaption.ForeColor = Color.FromArgb(51, 65, 85);
            selectBranchCaption.Location = new Point(31, 142);
            selectBranchCaption.Name = "selectBranchCaption";
            selectBranchCaption.Size = new Size(467, 24);
            selectBranchCaption.TabIndex = 3;
            selectBranchCaption.Text = "Target Branch";
            // 
            // BranchesComboBox
            // 
            BranchesComboBox.BackColor = Color.Transparent;
            BranchesComboBox.BorderColor = Color.FromArgb(203, 213, 225);
            BranchesComboBox.BorderRadius = 8;
            BranchesComboBox.CustomizableEdges = customizableEdges5;
            BranchesComboBox.Dock = DockStyle.Fill;
            BranchesComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            BranchesComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            BranchesComboBox.FocusedColor = Color.FromArgb(37, 99, 235);
            BranchesComboBox.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            BranchesComboBox.Font = new Font("Segoe UI", 10F);
            BranchesComboBox.ForeColor = Color.FromArgb(68, 88, 112);
            BranchesComboBox.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            BranchesComboBox.ItemHeight = 30;
            BranchesComboBox.Location = new Point(31, 171);
            BranchesComboBox.Margin = new Padding(3, 5, 3, 5);
            BranchesComboBox.Name = "BranchesComboBox";
            BranchesComboBox.ShadowDecoration.CustomizableEdges = customizableEdges6;
            BranchesComboBox.Size = new Size(467, 36);
            BranchesComboBox.TabIndex = 4;
            // 
            // switchButtonsPanel
            // 
            switchButtonsPanel.BackColor = Color.Transparent;
            switchButtonsPanel.Controls.Add(CancelBtn);
            switchButtonsPanel.Controls.Add(SwitchBtn);
            switchButtonsPanel.Dock = DockStyle.Fill;
            switchButtonsPanel.Location = new Point(31, 223);
            switchButtonsPanel.Name = "switchButtonsPanel";
            switchButtonsPanel.Size = new Size(467, 410);
            switchButtonsPanel.TabIndex = 5;
            // 
            // CancelBtn
            // 
            CancelBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            CancelBtn.BorderColor = Color.FromArgb(203, 213, 225);
            CancelBtn.BorderRadius = 10;
            CancelBtn.BorderThickness = 1;
            CancelBtn.Cursor = Cursors.Hand;
            CancelBtn.CustomizableEdges = customizableEdges7;
            CancelBtn.DisabledState.BorderColor = Color.DarkGray;
            CancelBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            CancelBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            CancelBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            CancelBtn.FillColor = Color.White;
            CancelBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            CancelBtn.ForeColor = Color.FromArgb(71, 85, 105);
            CancelBtn.HoverState.FillColor = Color.FromArgb(248, 250, 252);
            CancelBtn.Location = new Point(108, 0);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.ShadowDecoration.CustomizableEdges = customizableEdges8;
            CancelBtn.Size = new Size(170, 44);
            CancelBtn.TabIndex = 1;
            CancelBtn.Text = "Cancel";
            CancelBtn.Click += CancelBtn_Click;
            // 
            // SwitchBtn
            // 
            SwitchBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SwitchBtn.BorderRadius = 10;
            SwitchBtn.Cursor = Cursors.Hand;
            SwitchBtn.CustomizableEdges = customizableEdges9;
            SwitchBtn.DisabledState.BorderColor = Color.DarkGray;
            SwitchBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            SwitchBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            SwitchBtn.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            SwitchBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            SwitchBtn.FillColor = Color.FromArgb(37, 99, 235);
            SwitchBtn.FillColor2 = Color.FromArgb(59, 130, 246);
            SwitchBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            SwitchBtn.ForeColor = Color.White;
            SwitchBtn.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            SwitchBtn.HoverState.FillColor2 = Color.FromArgb(37, 99, 235);
            SwitchBtn.Location = new Point(288, 0);
            SwitchBtn.Name = "SwitchBtn";
            SwitchBtn.ShadowDecoration.CustomizableEdges = customizableEdges10;
            SwitchBtn.Size = new Size(179, 44);
            SwitchBtn.TabIndex = 0;
            SwitchBtn.Text = "Switch Branch";
            SwitchBtn.Click += SwitchBtn_Click;
            // 
            // infoCard
            // 
            infoCard.BorderColor = Color.FromArgb(226, 232, 240);
            infoCard.BorderRadius = 14;
            infoCard.BorderThickness = 1;
            infoCard.Controls.Add(infoRoot);
            infoCard.CustomizableEdges = customizableEdges11;
            infoCard.Dock = DockStyle.Fill;
            infoCard.FillColor = Color.White;
            infoCard.Location = new Point(573, 16);
            infoCard.Margin = new Padding(12, 0, 0, 0);
            infoCard.Name = "infoCard";
            infoCard.ShadowDecoration.CustomizableEdges = customizableEdges12;
            infoCard.Size = new Size(431, 664);
            infoCard.TabIndex = 1;
            // 
            // infoRoot
            // 
            infoRoot.ColumnCount = 1;
            infoRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            infoRoot.Controls.Add(infoHeaderPanel, 0, 0);
            infoRoot.Controls.Add(userCaption, 0, 1);
            infoRoot.Controls.Add(userValue, 0, 2);
            infoRoot.Controls.Add(roleCaption, 0, 3);
            infoRoot.Controls.Add(RoleDisplayLbl, 0, 4);
            infoRoot.Controls.Add(accessCaption, 0, 5);
            infoRoot.Controls.Add(AllowedBranchesLbl, 0, 6);
            infoRoot.Dock = DockStyle.Fill;
            infoRoot.Location = new Point(0, 0);
            infoRoot.Name = "infoRoot";
            infoRoot.Padding = new Padding(28);
            infoRoot.RowCount = 7;
            infoRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 46F));
            infoRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            infoRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            infoRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            infoRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            infoRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 24F));
            infoRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            infoRoot.Size = new Size(431, 664);
            infoRoot.TabIndex = 0;
            // 
            // infoHeaderPanel
            // 
            infoHeaderPanel.BackColor = Color.Transparent;
            infoHeaderPanel.Controls.Add(infoHeaderLbl);
            infoHeaderPanel.Dock = DockStyle.Fill;
            infoHeaderPanel.Location = new Point(31, 31);
            infoHeaderPanel.Name = "infoHeaderPanel";
            infoHeaderPanel.Size = new Size(369, 40);
            infoHeaderPanel.TabIndex = 0;
            // 
            // infoHeaderLbl
            // 
            infoHeaderLbl.BackColor = Color.Transparent;
            infoHeaderLbl.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            infoHeaderLbl.ForeColor = Color.FromArgb(15, 23, 42);
            infoHeaderLbl.Location = new Point(0, 4);
            infoHeaderLbl.Name = "infoHeaderLbl";
            infoHeaderLbl.Size = new Size(202, 33);
            infoHeaderLbl.TabIndex = 0;
            infoHeaderLbl.Text = "Access Information";
            // 
            // userCaption
            // 
            userCaption.AutoSize = true;
            userCaption.Dock = DockStyle.Fill;
            userCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            userCaption.ForeColor = Color.FromArgb(100, 116, 139);
            userCaption.Location = new Point(31, 74);
            userCaption.Name = "userCaption";
            userCaption.Size = new Size(369, 24);
            userCaption.TabIndex = 1;
            userCaption.Text = "SIGNED IN AS";
            // 
            // userValue
            // 
            userValue.AutoSize = true;
            userValue.Dock = DockStyle.Fill;
            userValue.Font = new Font("Segoe UI", 10.5F);
            userValue.ForeColor = Color.FromArgb(15, 23, 42);
            userValue.Location = new Point(31, 98);
            userValue.Name = "userValue";
            userValue.Size = new Size(369, 40);
            userValue.TabIndex = 2;
            userValue.Text = "—";
            // 
            // roleCaption
            // 
            roleCaption.AutoSize = true;
            roleCaption.Dock = DockStyle.Fill;
            roleCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            roleCaption.ForeColor = Color.FromArgb(100, 116, 139);
            roleCaption.Location = new Point(31, 138);
            roleCaption.Name = "roleCaption";
            roleCaption.Size = new Size(369, 24);
            roleCaption.TabIndex = 3;
            roleCaption.Text = "ROLE";
            // 
            // RoleDisplayLbl
            // 
            RoleDisplayLbl.AutoSize = true;
            RoleDisplayLbl.Dock = DockStyle.Fill;
            RoleDisplayLbl.Font = new Font("Segoe UI", 10.5F);
            RoleDisplayLbl.ForeColor = Color.FromArgb(15, 23, 42);
            RoleDisplayLbl.Location = new Point(31, 162);
            RoleDisplayLbl.Name = "RoleDisplayLbl";
            RoleDisplayLbl.Size = new Size(369, 40);
            RoleDisplayLbl.TabIndex = 4;
            RoleDisplayLbl.Text = "—";
            // 
            // accessCaption
            // 
            accessCaption.AutoSize = true;
            accessCaption.Dock = DockStyle.Fill;
            accessCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            accessCaption.ForeColor = Color.FromArgb(100, 116, 139);
            accessCaption.Location = new Point(31, 202);
            accessCaption.Name = "accessCaption";
            accessCaption.Size = new Size(369, 24);
            accessCaption.TabIndex = 5;
            accessCaption.Text = "BRANCHES YOU CAN ACCESS";
            // 
            // AllowedBranchesLbl
            // 
            AllowedBranchesLbl.Dock = DockStyle.Fill;
            AllowedBranchesLbl.Font = new Font("Segoe UI", 10F);
            AllowedBranchesLbl.ForeColor = Color.FromArgb(30, 41, 59);
            AllowedBranchesLbl.Location = new Point(31, 226);
            AllowedBranchesLbl.Name = "AllowedBranchesLbl";
            AllowedBranchesLbl.Size = new Size(369, 410);
            AllowedBranchesLbl.TabIndex = 6;
            AllowedBranchesLbl.Text = "—";
            // 
            // BranchSwitcherPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            Controls.Add(bodyLayout);
            Controls.Add(headerPanel);
            Name = "BranchSwitcherPage";
            Size = new Size(1024, 768);
            Load += BranchSwitcherPage_Load;
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            bodyLayout.ResumeLayout(false);
            switchCard.ResumeLayout(false);
            switchRoot.ResumeLayout(false);
            switchRoot.PerformLayout();
            switchHeaderPanel.ResumeLayout(false);
            switchHeaderPanel.PerformLayout();
            switchButtonsPanel.ResumeLayout(false);
            infoCard.ResumeLayout(false);
            infoRoot.ResumeLayout(false);
            infoRoot.PerformLayout();
            infoHeaderPanel.ResumeLayout(false);
            infoHeaderPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2GradientPanel headerPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel titleLbl;
        private Guna.UI2.WinForms.Guna2HtmlLabel currentBranchHeaderLbl;
        private Guna.UI2.WinForms.Guna2Button backToDashboardBtn;

        private TableLayoutPanel bodyLayout;

        private Guna.UI2.WinForms.Guna2Panel switchCard;
        private TableLayoutPanel switchRoot;
        private Panel switchHeaderPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel switchHeaderLbl;
        private Label currentBranchCaption;
        private Label lblCurrentBranch;
        private Label selectBranchCaption;
        private Guna.UI2.WinForms.Guna2ComboBox BranchesComboBox;
        private Panel switchButtonsPanel;
        private Guna.UI2.WinForms.Guna2GradientButton SwitchBtn;
        private Guna.UI2.WinForms.Guna2Button CancelBtn;

        private Guna.UI2.WinForms.Guna2Panel infoCard;
        private TableLayoutPanel infoRoot;
        private Panel infoHeaderPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel infoHeaderLbl;
        private Label userCaption;
        private Label userValue;
        private Label roleCaption;
        private Label RoleDisplayLbl;
        private Label accessCaption;
        private Label AllowedBranchesLbl;
    }
}