namespace Pos_system.Views.Pages
{
    partial class Settings
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
            settingsTabControl = new TabControl();
            Users = new TabPage();
            Store = new TabPage();
            Logs = new TabPage();
            usersLabel = new Label();
            dataGridView1 = new DataGridView();
            userDetailsLabel = new Label();
            usernameLabel = new Label();
            roleLabel = new Label();
            passwordLabel = new Label();
            comboBox1 = new ComboBox();
            usernameTextBox = new TextBox();
            passwordTextBox = new TextBox();
            saveBtn = new Button();
            deleteBtn = new Button();
            infoLabel = new Label();
            storenameLabel = new Label();
            addressLabel = new Label();
            contactLabel = new Label();
            storenameLabelOut = new Label();
            addressLabelOut = new Label();
            contactLabelOut = new Label();
            editLabel = new Label();
            editContactLabel = new Label();
            editAddressLabel = new Label();
            editNameLabel = new Label();
            storenameTextBox = new TextBox();
            addressTextBox = new TextBox();
            contactTextBox = new TextBox();
            storeSaveBtn = new Button();
            storeCancelBtn = new Button();
            logsListLabel = new Label();
            backToDashboardBtn = new Button();
            settingsTabControl.SuspendLayout();
            Users.SuspendLayout();
            Store.SuspendLayout();
            Logs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // settingsTabControl
            // 
            settingsTabControl.Controls.Add(Users);
            settingsTabControl.Controls.Add(Store);
            settingsTabControl.Controls.Add(Logs);
            settingsTabControl.Location = new Point(73, 105);
            settingsTabControl.Name = "settingsTabControl";
            settingsTabControl.SelectedIndex = 0;
            settingsTabControl.Size = new Size(1133, 586);
            settingsTabControl.TabIndex = 0;
            // 
            // Users
            // 
            Users.BorderStyle = BorderStyle.Fixed3D;
            Users.Controls.Add(deleteBtn);
            Users.Controls.Add(saveBtn);
            Users.Controls.Add(passwordTextBox);
            Users.Controls.Add(usernameTextBox);
            Users.Controls.Add(comboBox1);
            Users.Controls.Add(passwordLabel);
            Users.Controls.Add(roleLabel);
            Users.Controls.Add(usernameLabel);
            Users.Controls.Add(userDetailsLabel);
            Users.Controls.Add(dataGridView1);
            Users.Controls.Add(usersLabel);
            Users.Font = new Font("Tahoma", 10F);
            Users.Location = new Point(4, 29);
            Users.Name = "Users";
            Users.Padding = new Padding(3);
            Users.Size = new Size(1125, 553);
            Users.TabIndex = 0;
            Users.Text = "Users";
            Users.UseVisualStyleBackColor = true;
            // 
            // Store
            // 
            Store.BorderStyle = BorderStyle.Fixed3D;
            Store.Controls.Add(storeCancelBtn);
            Store.Controls.Add(storeSaveBtn);
            Store.Controls.Add(contactTextBox);
            Store.Controls.Add(addressTextBox);
            Store.Controls.Add(storenameTextBox);
            Store.Controls.Add(editContactLabel);
            Store.Controls.Add(editAddressLabel);
            Store.Controls.Add(editNameLabel);
            Store.Controls.Add(editLabel);
            Store.Controls.Add(contactLabelOut);
            Store.Controls.Add(addressLabelOut);
            Store.Controls.Add(storenameLabelOut);
            Store.Controls.Add(contactLabel);
            Store.Controls.Add(addressLabel);
            Store.Controls.Add(storenameLabel);
            Store.Controls.Add(infoLabel);
            Store.Font = new Font("Tahoma", 10F);
            Store.Location = new Point(4, 29);
            Store.Name = "Store";
            Store.Padding = new Padding(3);
            Store.Size = new Size(1125, 553);
            Store.TabIndex = 1;
            Store.Text = "tabPage2";
            Store.UseVisualStyleBackColor = true;
            // 
            // Logs
            // 
            Logs.BorderStyle = BorderStyle.Fixed3D;
            Logs.Controls.Add(logsListLabel);
            Logs.Font = new Font("Tahoma", 10F);
            Logs.Location = new Point(4, 29);
            Logs.Name = "Logs";
            Logs.Padding = new Padding(3);
            Logs.Size = new Size(1125, 553);
            Logs.TabIndex = 2;
            Logs.Text = "tabPage3";
            Logs.UseVisualStyleBackColor = true;
            // 
            // usersLabel
            // 
            usersLabel.AutoSize = true;
            usersLabel.Font = new Font("Tahoma", 11F);
            usersLabel.Location = new Point(41, 73);
            usersLabel.Name = "usersLabel";
            usersLabel.Size = new Size(55, 23);
            usersLabel.TabIndex = 0;
            usersLabel.Text = "Users";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(41, 119);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(478, 294);
            dataGridView1.TabIndex = 1;
            // 
            // userDetailsLabel
            // 
            userDetailsLabel.AutoSize = true;
            userDetailsLabel.Font = new Font("Tahoma", 11F);
            userDetailsLabel.Location = new Point(632, 73);
            userDetailsLabel.Name = "userDetailsLabel";
            userDetailsLabel.Size = new Size(108, 23);
            userDetailsLabel.TabIndex = 2;
            userDetailsLabel.Text = "User Details";
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(630, 119);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(85, 21);
            usernameLabel.TabIndex = 3;
            usernameLabel.Text = "Username";
            // 
            // roleLabel
            // 
            roleLabel.AutoSize = true;
            roleLabel.Location = new Point(632, 189);
            roleLabel.Name = "roleLabel";
            roleLabel.Size = new Size(43, 21);
            roleLabel.TabIndex = 4;
            roleLabel.Text = "Role";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(630, 270);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(81, 21);
            passwordLabel.TabIndex = 5;
            passwordLabel.Text = "Password";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Admin", "Manager", "Cashier" });
            comboBox1.Location = new Point(782, 186);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(224, 29);
            comboBox1.TabIndex = 6;
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(782, 119);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(224, 28);
            usernameTextBox.TabIndex = 7;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(782, 270);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(224, 28);
            passwordTextBox.TabIndex = 8;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(632, 384);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(94, 29);
            saveBtn.TabIndex = 9;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            // 
            // deleteBtn
            // 
            deleteBtn.Location = new Point(879, 384);
            deleteBtn.Name = "deleteBtn";
            deleteBtn.Size = new Size(127, 29);
            deleteBtn.TabIndex = 10;
            deleteBtn.Text = "Delete User";
            deleteBtn.UseVisualStyleBackColor = true;
            // 
            // infoLabel
            // 
            infoLabel.AutoSize = true;
            infoLabel.Font = new Font("Tahoma", 13F);
            infoLabel.Location = new Point(47, 76);
            infoLabel.Name = "infoLabel";
            infoLabel.Size = new Size(109, 27);
            infoLabel.TabIndex = 0;
            infoLabel.Text = "Store Info";
            // 
            // storenameLabel
            // 
            storenameLabel.AutoSize = true;
            storenameLabel.Location = new Point(47, 133);
            storenameLabel.Name = "storenameLabel";
            storenameLabel.Size = new Size(59, 21);
            storenameLabel.TabIndex = 1;
            storenameLabel.Text = "Name:";
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Location = new Point(50, 202);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new Size(76, 21);
            addressLabel.TabIndex = 2;
            addressLabel.Text = "Address:";
            // 
            // contactLabel
            // 
            contactLabel.AutoSize = true;
            contactLabel.Location = new Point(50, 264);
            contactLabel.Name = "contactLabel";
            contactLabel.Size = new Size(73, 21);
            contactLabel.TabIndex = 3;
            contactLabel.Text = "Contact:";
            // 
            // storenameLabelOut
            // 
            storenameLabelOut.AutoSize = true;
            storenameLabelOut.Location = new Point(175, 133);
            storenameLabelOut.Name = "storenameLabelOut";
            storenameLabelOut.Size = new Size(82, 21);
            storenameLabelOut.TabIndex = 4;
            storenameLabelOut.Text = "Loading...";
            // 
            // addressLabelOut
            // 
            addressLabelOut.AutoSize = true;
            addressLabelOut.Location = new Point(175, 202);
            addressLabelOut.Name = "addressLabelOut";
            addressLabelOut.Size = new Size(82, 21);
            addressLabelOut.TabIndex = 5;
            addressLabelOut.Text = "Loading...";
            // 
            // contactLabelOut
            // 
            contactLabelOut.AutoSize = true;
            contactLabelOut.Location = new Point(175, 264);
            contactLabelOut.Name = "contactLabelOut";
            contactLabelOut.Size = new Size(82, 21);
            contactLabelOut.TabIndex = 6;
            contactLabelOut.Text = "Loading...";
            // 
            // editLabel
            // 
            editLabel.AutoSize = true;
            editLabel.Font = new Font("Tahoma", 13F);
            editLabel.Location = new Point(542, 79);
            editLabel.Name = "editLabel";
            editLabel.Size = new Size(48, 27);
            editLabel.TabIndex = 7;
            editLabel.Text = "Edit";
            // 
            // editContactLabel
            // 
            editContactLabel.AutoSize = true;
            editContactLabel.Location = new Point(545, 264);
            editContactLabel.Name = "editContactLabel";
            editContactLabel.Size = new Size(73, 21);
            editContactLabel.TabIndex = 10;
            editContactLabel.Text = "Contact:";
            // 
            // editAddressLabel
            // 
            editAddressLabel.AutoSize = true;
            editAddressLabel.Location = new Point(542, 202);
            editAddressLabel.Name = "editAddressLabel";
            editAddressLabel.Size = new Size(76, 21);
            editAddressLabel.TabIndex = 9;
            editAddressLabel.Text = "Address:";
            // 
            // editNameLabel
            // 
            editNameLabel.AutoSize = true;
            editNameLabel.Location = new Point(542, 133);
            editNameLabel.Name = "editNameLabel";
            editNameLabel.Size = new Size(59, 21);
            editNameLabel.TabIndex = 8;
            editNameLabel.Text = "Name:";
            // 
            // storenameTextBox
            // 
            storenameTextBox.Location = new Point(684, 133);
            storenameTextBox.Name = "storenameTextBox";
            storenameTextBox.Size = new Size(341, 28);
            storenameTextBox.TabIndex = 11;
            // 
            // addressTextBox
            // 
            addressTextBox.Location = new Point(684, 202);
            addressTextBox.Name = "addressTextBox";
            addressTextBox.Size = new Size(342, 28);
            addressTextBox.TabIndex = 12;
            // 
            // contactTextBox
            // 
            contactTextBox.Location = new Point(684, 264);
            contactTextBox.Name = "contactTextBox";
            contactTextBox.Size = new Size(341, 28);
            contactTextBox.TabIndex = 13;
            // 
            // storeSaveBtn
            // 
            storeSaveBtn.Location = new Point(545, 387);
            storeSaveBtn.Name = "storeSaveBtn";
            storeSaveBtn.Size = new Size(94, 29);
            storeSaveBtn.TabIndex = 14;
            storeSaveBtn.Text = "Save";
            storeSaveBtn.UseVisualStyleBackColor = true;
            // 
            // storeCancelBtn
            // 
            storeCancelBtn.Location = new Point(932, 387);
            storeCancelBtn.Name = "storeCancelBtn";
            storeCancelBtn.Size = new Size(94, 29);
            storeCancelBtn.TabIndex = 15;
            storeCancelBtn.Text = "Cancel";
            storeCancelBtn.UseVisualStyleBackColor = true;
            // 
            // logsListLabel
            // 
            logsListLabel.AutoSize = true;
            logsListLabel.Location = new Point(67, 77);
            logsListLabel.Name = "logsListLabel";
            logsListLabel.Size = new Size(75, 21);
            logsListLabel.TabIndex = 0;
            logsListLabel.Text = "Logs List";
            // 
            // backToDashboardBtn
            // 
            backToDashboardBtn.BackColor = Color.FromArgb(55, 161, 218);
            backToDashboardBtn.Font = new Font("Arial Narrow", 12F, FontStyle.Bold);
            backToDashboardBtn.ForeColor = Color.White;
            backToDashboardBtn.Image = Properties.Resources.dashboard_png;
            backToDashboardBtn.ImageAlign = ContentAlignment.MiddleLeft;
            backToDashboardBtn.Location = new Point(37, 36);
            backToDashboardBtn.Name = "backToDashboardBtn";
            backToDashboardBtn.Size = new Size(127, 45);
            backToDashboardBtn.TabIndex = 1;
            backToDashboardBtn.Text = "Dashboard";
            backToDashboardBtn.TextAlign = ContentAlignment.MiddleRight;
            backToDashboardBtn.UseVisualStyleBackColor = false;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(backToDashboardBtn);
            Controls.Add(settingsTabControl);
            Name = "Settings";
            Size = new Size(1280, 720);
            settingsTabControl.ResumeLayout(false);
            Users.ResumeLayout(false);
            Users.PerformLayout();
            Store.ResumeLayout(false);
            Store.PerformLayout();
            Logs.ResumeLayout(false);
            Logs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TabControl settingsTabControl;
        private TabPage Users;
        private TabPage Store;
        private TabPage Logs;
        private Label usersLabel;
        private Label userDetailsLabel;
        private DataGridView dataGridView1;
        private Label roleLabel;
        private Label usernameLabel;
        private Button saveBtn;
        private TextBox passwordTextBox;
        private TextBox usernameTextBox;
        private ComboBox comboBox1;
        private Label passwordLabel;
        private Button deleteBtn;
        private Label contactLabel;
        private Label addressLabel;
        private Label storenameLabel;
        private Label infoLabel;
        private Label contactLabelOut;
        private Label addressLabelOut;
        private Label storenameLabelOut;
        private Label editLabel;
        private Label editContactLabel;
        private Label editAddressLabel;
        private Label editNameLabel;
        private TextBox contactTextBox;
        private TextBox addressTextBox;
        private TextBox storenameTextBox;
        private Button storeCancelBtn;
        private Button storeSaveBtn;
        private Label logsListLabel;
        private Button backToDashboardBtn;
    }
}
