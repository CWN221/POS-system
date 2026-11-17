namespace Pos_system.Views.Dialogs
{
    partial class ManagerAuthDialog
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
            headerPanel = new Guna.UI2.WinForms.Guna2Panel();
            headerLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
            subtitleLbl = new Label();
            usernameCaption = new Label();
            usernameBox = new Guna.UI2.WinForms.Guna2TextBox();
            passwordCaption = new Label();
            passwordBox = new Guna.UI2.WinForms.Guna2TextBox();
            errorLbl = new Label();
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
            headerPanel.Size = new Size(480, 60);
            headerPanel.TabIndex = 0;
            // 
            // headerLbl
            // 
            headerLbl.BackColor = Color.Transparent;
            headerLbl.Font = new Font("Segoe UI Semibold", 14F, FontStyle.Bold);
            headerLbl.ForeColor = Color.FromArgb(15, 23, 42);
            headerLbl.Location = new Point(24, 16);
            headerLbl.Name = "headerLbl";
            headerLbl.Size = new Size(244, 33);
            headerLbl.TabIndex = 0;
            headerLbl.Text = "Manager Authorization";
            // 
            // subtitleLbl
            // 
            subtitleLbl.AutoSize = true;
            subtitleLbl.Font = new Font("Segoe UI", 9.5F);
            subtitleLbl.ForeColor = Color.FromArgb(100, 116, 139);
            subtitleLbl.Location = new Point(24, 78);
            subtitleLbl.Name = "subtitleLbl";
            subtitleLbl.Size = new Size(349, 21);
            subtitleLbl.TabIndex = 1;
            subtitleLbl.Text = "Enter manager credentials to approve this return.";
            // 
            // usernameCaption
            // 
            usernameCaption.AutoSize = true;
            usernameCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            usernameCaption.ForeColor = Color.FromArgb(51, 65, 85);
            usernameCaption.Location = new Point(24, 120);
            usernameCaption.Name = "usernameCaption";
            usernameCaption.Size = new Size(83, 21);
            usernameCaption.TabIndex = 2;
            usernameCaption.Text = "Username";
            // 
            // usernameBox
            // 
            usernameBox.BorderColor = Color.FromArgb(203, 213, 225);
            usernameBox.BorderRadius = 8;
            usernameBox.CustomizableEdges = customizableEdges3;
            usernameBox.DefaultText = "";
            usernameBox.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            usernameBox.Font = new Font("Segoe UI", 10F);
            usernameBox.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            usernameBox.Location = new Point(24, 146);
            usernameBox.Margin = new Padding(3, 4, 3, 4);
            usernameBox.Name = "usernameBox";
            usernameBox.PlaceholderText = "Manager username";
            usernameBox.SelectedText = "";
            usernameBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            usernameBox.Size = new Size(432, 40);
            usernameBox.TabIndex = 3;
            // 
            // passwordCaption
            // 
            passwordCaption.AutoSize = true;
            passwordCaption.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            passwordCaption.ForeColor = Color.FromArgb(51, 65, 85);
            passwordCaption.Location = new Point(24, 202);
            passwordCaption.Name = "passwordCaption";
            passwordCaption.Size = new Size(79, 21);
            passwordCaption.TabIndex = 4;
            passwordCaption.Text = "Password";
            // 
            // passwordBox
            // 
            passwordBox.BorderColor = Color.FromArgb(203, 213, 225);
            passwordBox.BorderRadius = 8;
            passwordBox.CustomizableEdges = customizableEdges5;
            passwordBox.DefaultText = "";
            passwordBox.FocusedState.BorderColor = Color.FromArgb(37, 99, 235);
            passwordBox.Font = new Font("Segoe UI", 10F);
            passwordBox.HoverState.BorderColor = Color.FromArgb(37, 99, 235);
            passwordBox.Location = new Point(24, 228);
            passwordBox.Margin = new Padding(3, 4, 3, 4);
            passwordBox.Name = "passwordBox";
            passwordBox.PasswordChar = '●';
            passwordBox.PlaceholderText = "Manager password";
            passwordBox.SelectedText = "";
            passwordBox.ShadowDecoration.CustomizableEdges = customizableEdges6;
            passwordBox.Size = new Size(432, 40);
            passwordBox.TabIndex = 5;
            // 
            // errorLbl
            // 
            errorLbl.AutoSize = true;
            errorLbl.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            errorLbl.ForeColor = Color.FromArgb(220, 38, 38);
            errorLbl.Location = new Point(24, 280);
            errorLbl.Name = "errorLbl";
            errorLbl.Size = new Size(0, 21);
            errorLbl.TabIndex = 6;
            errorLbl.Visible = false;
            // 
            // confirmBtn
            // 
            confirmBtn.BorderRadius = 10;
            confirmBtn.CustomizableEdges = customizableEdges7;
            confirmBtn.FillColor = Color.FromArgb(37, 99, 235);
            confirmBtn.FillColor2 = Color.FromArgb(59, 130, 246);
            confirmBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            confirmBtn.ForeColor = Color.White;
            confirmBtn.HoverState.FillColor = Color.FromArgb(29, 78, 216);
            confirmBtn.HoverState.FillColor2 = Color.FromArgb(37, 99, 235);
            confirmBtn.Location = new Point(248, 320);
            confirmBtn.Name = "confirmBtn";
            confirmBtn.ShadowDecoration.CustomizableEdges = customizableEdges8;
            confirmBtn.Size = new Size(208, 42);
            confirmBtn.TabIndex = 7;
            confirmBtn.Text = "Approve";
            confirmBtn.Click += ConfirmBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.BorderColor = Color.FromArgb(203, 213, 225);
            cancelBtn.BorderRadius = 10;
            cancelBtn.BorderThickness = 1;
            cancelBtn.CustomizableEdges = customizableEdges9;
            cancelBtn.FillColor = Color.White;
            cancelBtn.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            cancelBtn.ForeColor = Color.FromArgb(71, 85, 105);
            cancelBtn.HoverState.FillColor = Color.FromArgb(248, 250, 252);
            cancelBtn.Location = new Point(24, 320);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.ShadowDecoration.CustomizableEdges = customizableEdges10;
            cancelBtn.Size = new Size(208, 42);
            cancelBtn.TabIndex = 8;
            cancelBtn.Text = "Cancel";
            cancelBtn.Click += CancelBtn_Click;
            // 
            // ManagerAuthDialog
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 250, 252);
            ClientSize = new Size(480, 384);
            Controls.Add(cancelBtn);
            Controls.Add(confirmBtn);
            Controls.Add(errorLbl);
            Controls.Add(passwordBox);
            Controls.Add(passwordCaption);
            Controls.Add(usernameBox);
            Controls.Add(usernameCaption);
            Controls.Add(subtitleLbl);
            Controls.Add(headerPanel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ManagerAuthDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Manager Authorization";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private Guna.UI2.WinForms.Guna2Panel headerPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel headerLbl;
        private Label subtitleLbl;
        private Label usernameCaption;
        private Guna.UI2.WinForms.Guna2TextBox usernameBox;
        private Label passwordCaption;
        private Guna.UI2.WinForms.Guna2TextBox passwordBox;
        private Label errorLbl;
        private Guna.UI2.WinForms.Guna2GradientButton confirmBtn;
        private Guna.UI2.WinForms.Guna2Button cancelBtn;
    }
}