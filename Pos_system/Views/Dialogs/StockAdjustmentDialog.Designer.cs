namespace Pos_system.Views.Dialogs
{
    partial class StockAdjustmentDialog
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
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            headerLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            productCaption = new Label();
            productValue = new Label();
            currentCaption = new Label();
            currentValue = new Label();
            deltaCaption = new Label();
            deltaBox = new Guna.UI2.WinForms.Guna2TextBox();
            newQtyCaption = new Label();
            newQtyValue = new Label();
            reasonCaption = new Label();
            reasonBox = new Guna.UI2.WinForms.Guna2TextBox();
            presetFlow = new FlowLayoutPanel();
            adjustSaveBtn = new Guna.UI2.WinForms.Guna2GradientButton();
            adjustCancelBtn = new Guna.UI2.WinForms.Guna2Button();
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
            headerPanel.Size = new Size(698, 60);
            headerPanel.TabIndex = 0;
            // 
            // headerLbl
            // 
            headerLbl.BackColor = Color.Transparent;
            headerLbl.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            headerLbl.ForeColor = Color.FromArgb(15, 23, 42);
            headerLbl.Location = new Point(24, 16);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(186, 33);
            headerLbl.TabIndex = 0;
            headerLbl.Text = "Stock Adjustment";
            // 
            // productCaption
            // 
            productCaption.AutoSize = true;
            productCaption.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            productCaption.ForeColor = Color.FromArgb(100, 116, 139);
            productCaption.Location = new Point(24, 82);
            productCaption.Name = "productCaption";
            productCaption.Size = new Size(77, 20);
            productCaption.TabIndex = 1;
            productCaption.Text = "PRODUCT";
            // 
            // productValue
            // 
            productValue.AutoSize = true;
            productValue.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            productValue.ForeColor = Color.FromArgb(15, 23, 42);
            productValue.Location = new Point(24, 102);
            productValue.Name = "productValue";
            productValue.Size = new Size(32, 28);
            productValue.TabIndex = 2;
            productValue.Text = "—";
            // 
            // currentCaption
            // 
            currentCaption.AutoSize = true;
            currentCaption.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            currentCaption.ForeColor = Color.FromArgb(100, 116, 139);
            currentCaption.Location = new Point(24, 148);
            currentCaption.Name = "currentCaption";
            currentCaption.Size = new Size(123, 20);
            currentCaption.TabIndex = 3;
            currentCaption.Text = "CURRENT STOCK";
            // 
            // currentValue
            // 
            currentValue.AutoSize = true;
            currentValue.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            currentValue.ForeColor = Color.FromArgb(37, 99, 235);
            currentValue.Location = new Point(24, 168);
            currentValue.Name = "currentValue";
            currentValue.Size = new Size(27, 32);
            currentValue.TabIndex = 4;
            currentValue.Text = "0";
            // 
            // deltaCaption
            // 
            deltaCaption.AutoSize = true;
            deltaCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            deltaCaption.ForeColor = Color.FromArgb(51, 65, 85);
            deltaCaption.Location = new Point(24, 226);
            deltaCaption.Name = "deltaCaption";
            deltaCaption.Size = new Size(240, 21);
            deltaCaption.TabIndex = 5;
            deltaCaption.Text = "Adjustment (use - for decrease)";
            // 
            // deltaBox
            // 
            deltaBox.BorderColor = Color.FromArgb(203, 213, 225);
            deltaBox.BorderRadius = 8;
            deltaBox.CustomizableEdges = customizableEdges3;
            deltaBox.DefaultText = "";
            deltaBox.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            deltaBox.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            deltaBox.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            deltaBox.Location = new Point(24, 252);
            deltaBox.Margin = new Padding(3, 4, 3, 4);
            deltaBox.Name = "deltaBox";
            deltaBox.PlaceholderText = "e.g. -3";
            deltaBox.SelectedText = "";
            deltaBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            deltaBox.Size = new Size(617, 44);
            deltaBox.TabIndex = 6;
            deltaBox.TextChanged += DeltaBox_TextChanged;
            // 
            // newQtyCaption
            // 
            newQtyCaption.AutoSize = true;
            newQtyCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            newQtyCaption.ForeColor = Color.FromArgb(51, 65, 85);
            newQtyCaption.Location = new Point(24, 312);
            newQtyCaption.Name = "newQtyCaption";
            newQtyCaption.Size = new Size(88, 21);
            newQtyCaption.TabIndex = 7;
            newQtyCaption.Text = "New Stock";
            // 
            // newQtyValue
            // 
            newQtyValue.AutoSize = true;
            newQtyValue.Font = new Font("Segoe UI Semibold", 16F, FontStyle.Bold);
            newQtyValue.ForeColor = Color.FromArgb(22, 163, 74);
            newQtyValue.Location = new Point(22, 334);
            newQtyValue.Name = "newQtyValue";
            newQtyValue.Size = new Size(32, 37);
            newQtyValue.TabIndex = 8;
            newQtyValue.Text = "0";
            // 
            // reasonCaption
            // 
            reasonCaption.AutoSize = true;
            reasonCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            reasonCaption.ForeColor = Color.FromArgb(51, 65, 85);
            reasonCaption.Location = new Point(24, 394);
            reasonCaption.Name = "reasonCaption";
            reasonCaption.Size = new Size(140, 21);
            reasonCaption.TabIndex = 9;
            reasonCaption.Text = "Reason (required)";
            // 
            // reasonBox
            // 
            reasonBox.BorderColor = Color.FromArgb(203, 213, 225);
            reasonBox.BorderRadius = 8;
            reasonBox.CustomizableEdges = customizableEdges5;
            reasonBox.DefaultText = "";
            reasonBox.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            reasonBox.Font = new Font("Segoe UI", 10F);
            reasonBox.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            reasonBox.Location = new Point(24, 420);
            reasonBox.Margin = new Padding(3, 4, 3, 4);
            reasonBox.Name = "reasonBox";
            reasonBox.PlaceholderText = "";
            reasonBox.SelectedText = "";
            reasonBox.ShadowDecoration.CustomizableEdges = customizableEdges5;
            reasonBox.Size = new Size(617, 44);
            reasonBox.TabIndex = 10;
            // 
            // presetFlow
            // 
            presetFlow.Location = new Point(24, 478);
            presetFlow.Name = "presetFlow";
            presetFlow.Size = new Size(617, 83);
            presetFlow.TabIndex = 11;
            // 
            // adjustSaveBtn
            // 
            adjustSaveBtn.BorderRadius = 10;
            adjustSaveBtn.CustomizableEdges = customizableEdges6;
            adjustSaveBtn.FillColor = Color.FromArgb(37, 99, 235);
            adjustSaveBtn.FillColor2 = Color.FromArgb(59, 130, 246);
            adjustSaveBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            adjustSaveBtn.ForeColor = Color.White;
            adjustSaveBtn.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            adjustSaveBtn.HoverState.FillColor2 = Color.FromArgb(37, 99, 235);
            adjustSaveBtn.Location = new Point(368, 578);
            adjustSaveBtn.Name = "adjustSaveBtn";
            adjustSaveBtn.ShadowDecoration.CustomizableEdges = customizableEdges7;
            adjustSaveBtn.Size = new Size(273, 44);
            adjustSaveBtn.TabIndex = 12;
            adjustSaveBtn.Text = "Apply Adjustment";
            adjustSaveBtn.Click += SaveBtn_Click;
            // 
            // adjustCancelBtn
            // 
            adjustCancelBtn.BorderColor = Color.FromArgb(203, 213, 225);
            adjustCancelBtn.BorderRadius = 10;
            adjustCancelBtn.BorderThickness = 1;
            adjustCancelBtn.CustomizableEdges = customizableEdges8;
            adjustCancelBtn.FillColor = Color.White;
            adjustCancelBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            adjustCancelBtn.ForeColor = Color.FromArgb(71, 85, 105);
            adjustCancelBtn.HoverState.FillColor = Color.FromArgb(248, 250, 252);
            adjustCancelBtn.Location = new Point(24, 578);
            adjustCancelBtn.Name = "adjustCancelBtn";
            adjustCancelBtn.ShadowDecoration.CustomizableEdges = customizableEdges9;
            adjustCancelBtn.Size = new Size(283, 44);
            adjustCancelBtn.TabIndex = 13;
            adjustCancelBtn.Text = "Cancel";
            adjustCancelBtn.Click += CancelBtn_Click;
            // 
            // StockAdjustmentDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(698, 650);
            Controls.Add(adjustCancelBtn);
            Controls.Add(adjustSaveBtn);
            Controls.Add(presetFlow);
            Controls.Add(reasonBox);
            Controls.Add(reasonCaption);
            Controls.Add(newQtyValue);
            Controls.Add(newQtyCaption);
            Controls.Add(deltaBox);
            Controls.Add(deltaCaption);
            Controls.Add(currentValue);
            Controls.Add(currentCaption);
            Controls.Add(productValue);
            Controls.Add(productCaption);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "StockAdjustmentDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Stock Adjustment";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel headerLbl;
        private Label productCaption;
        private Label productValue;
        private Label currentCaption;
        private Label currentValue;
        private Label deltaCaption;
        private Guna.UI2.WinForms.Guna2TextBox deltaBox;
        private Label newQtyCaption;
        private Label newQtyValue;
        private Label reasonCaption;
        private Guna.UI2.WinForms.Guna2TextBox reasonBox;
        private FlowLayoutPanel presetFlow;
        private Guna.UI2.WinForms.Guna2GradientButton adjustSaveBtn;
        private Guna.UI2.WinForms.Guna2Button adjustCancelBtn;
    }
}