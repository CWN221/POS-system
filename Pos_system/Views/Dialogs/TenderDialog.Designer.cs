using Guna.UI2.WinForms.Suite;

namespace Pos_system.Views.Dialogs
{
    partial class TenderDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges15 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges16 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            headerLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            totalCaption = new Label();
            totalValue = new Label();
            tenderedCaption = new Label();
            tenderedBox = new Guna.UI2.WinForms.Guna2TextBox();
            changeCaption = new Label();
            changeValue = new Label();
            methodCaption = new Label();
            methodCombo = new Guna.UI2.WinForms.Guna2ComboBox();
            phoneCaption = new Label();
            phoneBox = new Guna.UI2.WinForms.Guna2TextBox();
            stkBtn = new Guna.UI2.WinForms.Guna2Button();
            statusLbl = new Label();
            refCaption = new Label();
            refBox = new Guna.UI2.WinForms.Guna2TextBox();
            confirmBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            cancelBtn = new Guna.UI2.WinForms.Guna2Button();
            headerPanel.SuspendLayout();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(headerLbl);
            headerPanel.CustomizableEdges = customizableEdges1;
            headerPanel.Dock = DockStyle.Top;
            headerPanel.FillColor = Color.White;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.ShadowDecoration.CustomizableEdges = customizableEdges2;
            headerPanel.Size = new Size(520, 60);
            headerPanel.TabIndex = 0;
            // 
            // headerLbl
            // 
            headerLbl.BackColor = Color.Transparent;
            headerLbl.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            headerLbl.ForeColor = Color.FromArgb(15, 23, 42);
            headerLbl.Location = new Point(24, 18);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(152, 33);
            headerLbl.TabIndex = 0;
            headerLbl.Text = "Complete Sale";
            // 
            // totalCaption
            // 
            totalCaption.AutoSize = true;
            totalCaption.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            totalCaption.ForeColor = Color.FromArgb(100, 116, 139);
            totalCaption.Location = new Point(24, 84);
            totalCaption.Name = "totalCaption";
            totalCaption.Size = new Size(95, 23);
            totalCaption.TabIndex = 1;
            totalCaption.Text = "TOTAL DUE";
            // 
            // totalValue
            // 
            totalValue.AutoSize = true;
            totalValue.Font = new Font("Segoe UI Semibold", 24F, FontStyle.Bold);
            totalValue.ForeColor = Color.FromArgb(22, 163, 74);
            totalValue.Location = new Point(22, 110);
            totalValue.Name = "totalValue";
            totalValue.Size = new Size(179, 54);
            totalValue.TabIndex = 2;
            totalValue.Text = "KSh 0.00";
            // 
            // tenderedCaption
            // 
            tenderedCaption.AutoSize = true;
            tenderedCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            tenderedCaption.ForeColor = Color.FromArgb(51, 65, 85);
            tenderedCaption.Location = new Point(24, 274);
            tenderedCaption.Name = "tenderedCaption";
            tenderedCaption.Size = new Size(142, 21);
            tenderedCaption.TabIndex = 5;
            tenderedCaption.Text = "Amount Tendered";
            // 
            // tenderedBox
            // 
            tenderedBox.BorderColor = Color.FromArgb(203, 213, 225);
            tenderedBox.BorderRadius = 8;
            tenderedBox.CustomizableEdges = customizableEdges3;
            tenderedBox.DefaultText = "";
            tenderedBox.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            tenderedBox.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            tenderedBox.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            tenderedBox.Location = new Point(24, 300);
            tenderedBox.Margin = new Padding(3, 4, 3, 4);
            tenderedBox.Name = "tenderedBox";
            tenderedBox.PlaceholderText = "0.00";
            tenderedBox.SelectedText = "";
            tenderedBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            tenderedBox.Size = new Size(472, 44);
            tenderedBox.TabIndex = 6;
            tenderedBox.TextChanged += TenderedBox_TextChanged;
            // 
            // changeCaption
            // 
            changeCaption.AutoSize = true;
            changeCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            changeCaption.ForeColor = Color.FromArgb(51, 65, 85);
            changeCaption.Location = new Point(24, 360);
            changeCaption.Name = "changeCaption";
            changeCaption.Size = new Size(65, 21);
            changeCaption.TabIndex = 7;
            changeCaption.Text = "Change";
            // 
            // changeValue
            // 
            changeValue.AutoSize = true;
            changeValue.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            changeValue.ForeColor = Color.FromArgb(37, 99, 235);
            changeValue.Location = new Point(22, 384);
            changeValue.Name = "changeValue";
            changeValue.Size = new Size(135, 41);
            changeValue.TabIndex = 8;
            changeValue.Text = "KSh 0.00";
            // 
            // methodCaption
            // 
            methodCaption.AutoSize = true;
            methodCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            methodCaption.ForeColor = Color.FromArgb(51, 65, 85);
            methodCaption.Location = new Point(24, 190);
            methodCaption.Name = "methodCaption";
            methodCaption.Size = new Size(136, 21);
            methodCaption.TabIndex = 3;
            methodCaption.Text = "Payment Method";
            // 
            // methodCombo
            // 
            methodCombo.BackColor = Color.Transparent;
            methodCombo.BorderColor = Color.FromArgb(203, 213, 225);
            methodCombo.BorderRadius = 8;
            methodCombo.CustomizableEdges = customizableEdges5;
            methodCombo.DrawMode = DrawMode.OwnerDrawFixed;
            methodCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            methodCombo.FocusedColor = Color.FromArgb(37, 99, 235);
            methodCombo.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            methodCombo.Font = new Font("Segoe UI", 10F);
            methodCombo.ForeColor = Color.FromArgb(68, 88, 112);
            methodCombo.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            methodCombo.ItemHeight = 30;
            methodCombo.Location = new Point(24, 216);
            methodCombo.Name = "methodCombo";
            methodCombo.ShadowDecoration.CustomizableEdges = customizableEdges6;
            methodCombo.Size = new Size(472, 36);
            methodCombo.TabIndex = 4;
            // 
            // phoneCaption
            // 
            phoneCaption.AutoSize = true;
            phoneCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            phoneCaption.ForeColor = Color.FromArgb(51, 65, 85);
            phoneCaption.Location = new Point(24, 274);
            phoneCaption.Name = "phoneCaption";
            phoneCaption.Size = new Size(220, 21);
            phoneCaption.TabIndex = 13;
            phoneCaption.Text = "Customer Phone (M-Pesa)";
            phoneCaption.Visible = false;
            // 
            // phoneBox
            // 
            phoneBox.BorderColor = Color.FromArgb(203, 213, 225);
            phoneBox.BorderRadius = 8;
            phoneBox.CustomizableEdges = customizableEdges7;
            phoneBox.DefaultText = "";
            phoneBox.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            phoneBox.Font = new Font("Segoe UI", 10F);
            phoneBox.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            phoneBox.Location = new Point(24, 300);
            phoneBox.Margin = new Padding(3, 4, 3, 4);
            phoneBox.Name = "phoneBox";
            phoneBox.PlaceholderText = "2547XXXXXXXX";
            phoneBox.SelectedText = "";
            phoneBox.ShadowDecoration.CustomizableEdges = customizableEdges8;
            phoneBox.Size = new Size(300, 40);
            phoneBox.TabIndex = 14;
            phoneBox.Visible = false;
            // 
            // stkBtn
            // 
            stkBtn.BorderRadius = 8;
            stkBtn.CustomizableEdges = customizableEdges9;
            stkBtn.FillColor = Color.FromArgb(37, 99, 235);
            stkBtn.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            stkBtn.ForeColor = Color.White;
            stkBtn.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            stkBtn.Location = new Point(336, 300);
            stkBtn.Name = "stkBtn";
            stkBtn.ShadowDecoration.CustomizableEdges = customizableEdges10;
            stkBtn.Size = new Size(160, 40);
            stkBtn.TabIndex = 15;
            stkBtn.Text = "Send STK Push";
            stkBtn.Visible = false;
            stkBtn.Click += StkBtn_Click;
            // 
            // statusLbl
            // 
            statusLbl.AutoSize = true;
            statusLbl.Font = new Font("Segoe UI", 8.5F);
            statusLbl.ForeColor = Color.FromArgb(100, 116, 139);
            statusLbl.Location = new Point(24, 348);
            statusLbl.Name = "statusLbl";
            statusLbl.Size = new Size(200, 20);
            statusLbl.TabIndex = 16;
            statusLbl.Text = "";
            statusLbl.Visible = false;
            // 
            // refCaption
            // 
            refCaption.AutoSize = true;
            refCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            refCaption.ForeColor = Color.FromArgb(51, 65, 85);
            refCaption.Location = new Point(24, 442);
            refCaption.Name = "refCaption";
            refCaption.Size = new Size(201, 21);
            refCaption.TabIndex = 9;
            refCaption.Text = "Reference (M-Pesa / Card)";
            // 
            // refBox
            // 
            refBox.BorderColor = Color.FromArgb(203, 213, 225);
            refBox.BorderRadius = 8;
            refBox.CustomizableEdges = customizableEdges11;
            refBox.DefaultText = "";
            refBox.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            refBox.Font = new Font("Segoe UI", 10F);
            refBox.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            refBox.Location = new Point(24, 468);
            refBox.Margin = new Padding(3, 4, 3, 4);
            refBox.Name = "refBox";
            refBox.PlaceholderText = "Optional";
            refBox.SelectedText = "";
            refBox.ShadowDecoration.CustomizableEdges = customizableEdges12;
            refBox.Size = new Size(472, 40);
            refBox.TabIndex = 10;
            // 
            // confirmBtn
            // 
            confirmBtn.BorderRadius = 10;
            confirmBtn.CustomizableEdges = customizableEdges13;
            confirmBtn.FillColor = Color.Green;
            confirmBtn.FillColor2 = Color.Green;
            confirmBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            confirmBtn.ForeColor = Color.White;
            confirmBtn.HoverState.FillColor = Color.FromArgb(21, 128, 61);
            confirmBtn.HoverState.FillColor2 = Color.FromArgb(22, 163, 74);
            confirmBtn.Location = new Point(268, 536);
            confirmBtn.Name = "confirmBtn";
            confirmBtn.ShadowDecoration.CustomizableEdges = customizableEdges14;
            confirmBtn.Size = new Size(228, 44);
            confirmBtn.TabIndex = 11;
            confirmBtn.Text = "Confirm Payment";
            confirmBtn.Click += ConfirmBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.BorderColor = Color.FromArgb(203, 213, 225);
            cancelBtn.BorderRadius = 10;
            cancelBtn.BorderThickness = 1;
            cancelBtn.CustomizableEdges = customizableEdges15;
            cancelBtn.FillColor = Color.White;
            cancelBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            cancelBtn.ForeColor = Color.FromArgb(71, 85, 105);
            cancelBtn.HoverState.FillColor = Color.FromArgb(248, 250, 252);
            cancelBtn.Location = new Point(24, 536);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.ShadowDecoration.CustomizableEdges = customizableEdges16;
            cancelBtn.Size = new Size(228, 44);
            cancelBtn.TabIndex = 12;
            cancelBtn.Text = "Cancel";
            cancelBtn.Click += CancelBtn_Click;
            // 
            // TenderDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(520, 604);
            Controls.Add(cancelBtn);
            Controls.Add(confirmBtn);
            Controls.Add(refBox);
            Controls.Add(refCaption);
            Controls.Add(statusLbl);
            Controls.Add(stkBtn);
            Controls.Add(phoneBox);
            Controls.Add(phoneCaption);
            Controls.Add(changeValue);
            Controls.Add(changeCaption);
            Controls.Add(tenderedBox);
            Controls.Add(tenderedCaption);
            Controls.Add(methodCombo);
            Controls.Add(methodCaption);
            Controls.Add(totalValue);
            Controls.Add(totalCaption);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "TenderDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Complete Sale";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel headerLbl;
        private Label totalCaption;
        private Label totalValue;
        private Label methodCaption;
        private Guna.UI2.WinForms.Guna2ComboBox methodCombo;
        private Label tenderedCaption;
        private Guna.UI2.WinForms.Guna2TextBox tenderedBox;
        private Label changeCaption;
        private Label changeValue;
        private Label phoneCaption;
        private Guna.UI2.WinForms.Guna2TextBox phoneBox;
        private Guna.UI2.WinForms.Guna2Button stkBtn;
        private Label statusLbl;
        private Label refCaption;
        private Guna.UI2.WinForms.Guna2TextBox refBox;
        private Guna.UI2.WinForms.Guna2GradientButton confirmBtn;
        private Guna.UI2.WinForms.Guna2Button cancelBtn;
    }
}