namespace Pos_system.Views.Pages
{
    partial class UsersPage
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            panel1 = new Panel();
            guna2GradientPanel1 = new Guna.UI2.WinForms.Guna2GradientPanel();
            guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            AddUserBtn = new Guna.UI2.WinForms.Guna2Button();
            ManageUsersBtn = new Guna.UI2.WinForms.Guna2Button();
            panel1.SuspendLayout();
            guna2GradientPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.Controls.Add(guna2GradientPanel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1280, 720);
            panel1.TabIndex = 0;
            // 
            // guna2GradientPanel1
            // 
            guna2GradientPanel1.Controls.Add(guna2HtmlLabel1);
            guna2GradientPanel1.Controls.Add(AddUserBtn);
            guna2GradientPanel1.Controls.Add(ManageUsersBtn);
            guna2GradientPanel1.CustomizableEdges = customizableEdges5;
            guna2GradientPanel1.Dock = DockStyle.Fill;
            guna2GradientPanel1.Location = new Point(0, 0);
            guna2GradientPanel1.Name = "guna2GradientPanel1";
            guna2GradientPanel1.Padding = new Padding(0, 20, 0, 0);
            guna2GradientPanel1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2GradientPanel1.Size = new Size(1280, 720);
            guna2GradientPanel1.TabIndex = 0;
            // 
            // guna2HtmlLabel1
            // 
            guna2HtmlLabel1.Anchor = AnchorStyles.None;
            guna2HtmlLabel1.BackColor = Color.Transparent;
            guna2HtmlLabel1.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            guna2HtmlLabel1.Location = new Point(469, 13);
            guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            guna2HtmlLabel1.Size = new Size(289, 47);
            guna2HtmlLabel1.TabIndex = 6;
            guna2HtmlLabel1.Text = "User Management";
            // 
            // AddUserBtn
            // 
            AddUserBtn.Anchor = AnchorStyles.None;
            AddUserBtn.CustomizableEdges = customizableEdges1;
            AddUserBtn.DisabledState.BorderColor = Color.DarkGray;
            AddUserBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            AddUserBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            AddUserBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            AddUserBtn.FillColor = Color.DodgerBlue;
            AddUserBtn.Font = new Font("Arial Narrow", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            AddUserBtn.ForeColor = Color.White;
            AddUserBtn.Location = new Point(269, 259);
            AddUserBtn.Name = "AddUserBtn";
            AddUserBtn.ShadowDecoration.CustomizableEdges = customizableEdges2;
            AddUserBtn.Size = new Size(150, 50);
            AddUserBtn.TabIndex = 5;
            AddUserBtn.Text = "Add New Users";
            AddUserBtn.Click += AddUserBtn_Click;
            // 
            // ManageUsersBtn
            // 
            ManageUsersBtn.Anchor = AnchorStyles.None;
            ManageUsersBtn.CustomizableEdges = customizableEdges3;
            ManageUsersBtn.DisabledState.BorderColor = Color.DarkGray;
            ManageUsersBtn.DisabledState.CustomBorderColor = Color.DarkGray;
            ManageUsersBtn.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            ManageUsersBtn.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            ManageUsersBtn.FillColor = Color.DodgerBlue;
            ManageUsersBtn.Font = new Font("Arial Narrow", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ManageUsersBtn.ForeColor = Color.White;
            ManageUsersBtn.Location = new Point(726, 259);
            ManageUsersBtn.Name = "ManageUsersBtn";
            ManageUsersBtn.ShadowDecoration.CustomizableEdges = customizableEdges4;
            ManageUsersBtn.Size = new Size(150, 50);
            ManageUsersBtn.TabIndex = 4;
            ManageUsersBtn.Text = "Manage Users";
            ManageUsersBtn.Click += ManageUsersBtn_Click;
            // 
            // UsersPage
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Name = "UsersPage";
            Size = new Size(1280, 720);
            panel1.ResumeLayout(false);
            guna2GradientPanel1.ResumeLayout(false);
            guna2GradientPanel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Guna.UI2.WinForms.Guna2GradientPanel guna2GradientPanel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2Button AddUserBtn;
        private Guna.UI2.WinForms.Guna2Button ManageUsersBtn;
    }
}
