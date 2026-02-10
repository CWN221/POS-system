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
            deleteBtn = new Button();
            saveBtn = new Button();
            passwordTextBox = new TextBox();
            usernameTextBox = new TextBox();
            comboBox1 = new ComboBox();
            passwordLabel = new Label();
            roleLabel = new Label();
            usernameLabel = new Label();
            userDetailsLabel = new Label();
            dataGridView1 = new DataGridView();
            usersLabel = new Label();
            Store = new TabPage();
            storeCancelBtn = new Button();
            storeSaveBtn = new Button();
            contactTextBox = new TextBox();
            addressTextBox = new TextBox();
            storenameTextBox = new TextBox();
            editContactLabel = new Label();
            editAddressLabel = new Label();
            editNameLabel = new Label();
            editLabel = new Label();
            contactLabelOut = new Label();
            addressLabelOut = new Label();
            storenameLabelOut = new Label();
            contactLabel = new Label();
            addressLabel = new Label();
            storenameLabel = new Label();
            infoLabel = new Label();
            Logs = new TabPage();
            logsListLabel = new Label();
            backToDashboardBtn = new Button();
            settingsTabControl.SuspendLayout();
            Users.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            Store.SuspendLayout();
            Logs.SuspendLayout();
            SuspendLayout();
            // 
            // settingsTabControl
            // 
            settingsTabControl.Controls.Add(Users);
            settingsTabControl.Controls.Add(Store);
            settingsTabControl.Controls.Add(Logs);
            settingsTabControl.Font = new Font("Tahoma", 12F);
            settingsTabControl.Location = new Point(49, 105);
            settingsTabControl.Multiline = true;
            settingsTabControl.Name = "settingsTabControl";
            settingsTabControl.Padding = new Point(22, 7);
            settingsTabControl.SelectedIndex = 0;
            settingsTabControl.Size = new Size(1228, 612);
            settingsTabControl.TabIndex = 0;
            // 
            // Users
            // 
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
            Users.Font = new Font("Tahoma", 11F);
            Users.Location = new Point(4, 41);
            Users.Name = "Users";
            Users.Padding = new Padding(3);
            Users.Size = new Size(1220, 567);
            Users.TabIndex = 0;
            Users.Text = "Users";
            Users.UseVisualStyleBackColor = true;
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
            // saveBtn
            // 
            saveBtn.Location = new Point(632, 384);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(94, 29);
            saveBtn.TabIndex = 9;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            // 
            // passwordTextBox
            // 
            passwordTextBox.Location = new Point(782, 270);
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.Size = new Size(224, 30);
            passwordTextBox.TabIndex = 8;
            // 
            // usernameTextBox
            // 
            usernameTextBox.Location = new Point(782, 119);
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.Size = new Size(224, 30);
            usernameTextBox.TabIndex = 7;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "Admin", "Manager", "Cashier" });
            comboBox1.Location = new Point(782, 186);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(224, 30);
            comboBox1.TabIndex = 6;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point(630, 270);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(88, 23);
            passwordLabel.TabIndex = 5;
            passwordLabel.Text = "Password";
            // 
            // roleLabel
            // 
            roleLabel.AutoSize = true;
            roleLabel.Location = new Point(632, 189);
            roleLabel.Name = "roleLabel";
            roleLabel.Size = new Size(46, 23);
            roleLabel.TabIndex = 4;
            roleLabel.Text = "Role";
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Location = new Point(630, 119);
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(94, 23);
            usernameLabel.TabIndex = 3;
            usernameLabel.Text = "Username";
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
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(41, 119);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(478, 294);
            dataGridView1.TabIndex = 1;
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
            // Store
            // 
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
            Store.Font = new Font("Tahoma", 11F);
            Store.Location = new Point(4, 41);
            Store.Name = "Store";
            Store.Padding = new Padding(3);
            Store.Size = new Size(1220, 567);
            Store.TabIndex = 1;
            Store.Text = "Store";
            Store.UseVisualStyleBackColor = true;
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
            // storeSaveBtn
            // 
            storeSaveBtn.Location = new Point(545, 387);
            storeSaveBtn.Name = "storeSaveBtn";
            storeSaveBtn.Size = new Size(94, 29);
            storeSaveBtn.TabIndex = 14;
            storeSaveBtn.Text = "Save";
            storeSaveBtn.UseVisualStyleBackColor = true;
            // 
            // contactTextBox
            // 
            contactTextBox.Location = new Point(684, 264);
            contactTextBox.Name = "contactTextBox";
            contactTextBox.Size = new Size(341, 30);
            contactTextBox.TabIndex = 13;
            // 
            // addressTextBox
            // 
            addressTextBox.Location = new Point(684, 202);
            addressTextBox.Name = "addressTextBox";
            addressTextBox.Size = new Size(342, 30);
            addressTextBox.TabIndex = 12;
            // 
            // storenameTextBox
            // 
            storenameTextBox.Location = new Point(684, 133);
            storenameTextBox.Name = "storenameTextBox";
            storenameTextBox.Size = new Size(341, 30);
            storenameTextBox.TabIndex = 11;
            // 
            // editContactLabel
            // 
            editContactLabel.AutoSize = true;
            editContactLabel.Location = new Point(545, 264);
            editContactLabel.Name = "editContactLabel";
            editContactLabel.Size = new Size(80, 23);
            editContactLabel.TabIndex = 10;
            editContactLabel.Text = "Contact:";
            // 
            // editAddressLabel
            // 
            editAddressLabel.AutoSize = true;
            editAddressLabel.Location = new Point(542, 202);
            editAddressLabel.Name = "editAddressLabel";
            editAddressLabel.Size = new Size(83, 23);
            editAddressLabel.TabIndex = 9;
            editAddressLabel.Text = "Address:";
            // 
            // editNameLabel
            // 
            editNameLabel.AutoSize = true;
            editNameLabel.Location = new Point(542, 133);
            editNameLabel.Name = "editNameLabel";
            editNameLabel.Size = new Size(66, 23);
            editNameLabel.TabIndex = 8;
            editNameLabel.Text = "Name:";
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
            // contactLabelOut
            // 
            contactLabelOut.AutoSize = true;
            contactLabelOut.Location = new Point(175, 264);
            contactLabelOut.Name = "contactLabelOut";
            contactLabelOut.Size = new Size(94, 23);
            contactLabelOut.TabIndex = 6;
            contactLabelOut.Text = "Loading...";
            // 
            // addressLabelOut
            // 
            addressLabelOut.AutoSize = true;
            addressLabelOut.Location = new Point(175, 202);
            addressLabelOut.Name = "addressLabelOut";
            addressLabelOut.Size = new Size(94, 23);
            addressLabelOut.TabIndex = 5;
            addressLabelOut.Text = "Loading...";
            // 
            // storenameLabelOut
            // 
            storenameLabelOut.AutoSize = true;
            storenameLabelOut.Location = new Point(175, 133);
            storenameLabelOut.Name = "storenameLabelOut";
            storenameLabelOut.Size = new Size(94, 23);
            storenameLabelOut.TabIndex = 4;
            storenameLabelOut.Text = "Loading...";
            // 
            // contactLabel
            // 
            contactLabel.AutoSize = true;
            contactLabel.Location = new Point(50, 264);
            contactLabel.Name = "contactLabel";
            contactLabel.Size = new Size(80, 23);
            contactLabel.TabIndex = 3;
            contactLabel.Text = "Contact:";
            // 
            // addressLabel
            // 
            addressLabel.AutoSize = true;
            addressLabel.Location = new Point(50, 202);
            addressLabel.Name = "addressLabel";
            addressLabel.Size = new Size(83, 23);
            addressLabel.TabIndex = 2;
            addressLabel.Text = "Address:";
            // 
            // storenameLabel
            // 
            storenameLabel.AutoSize = true;
            storenameLabel.Location = new Point(47, 133);
            storenameLabel.Name = "storenameLabel";
            storenameLabel.Size = new Size(66, 23);
            storenameLabel.TabIndex = 1;
            storenameLabel.Text = "Name:";
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
            // Logs
            // 
            Logs.BackColor = Color.Transparent;
            Logs.Controls.Add(logsListLabel);
            Logs.Font = new Font("Tahoma", 11F);
            Logs.Location = new Point(4, 41);
            Logs.Name = "Logs";
            Logs.Padding = new Padding(3);
            Logs.Size = new Size(1220, 567);
            Logs.TabIndex = 2;
            Logs.Text = "Logs";
            // 
            // logsListLabel
            // 
            logsListLabel.AutoSize = true;
            logsListLabel.Location = new Point(67, 77);
            logsListLabel.Name = "logsListLabel";
            logsListLabel.Size = new Size(81, 23);
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
            backToDashboardBtn.Click += backToDashboardBtn_Click;
            // 
            // Settings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(backToDashboardBtn);
            Controls.Add(settingsTabControl);
            Name = "Settings";
            Size = new Size(1280, 720);
            settingsTabControl.ResumeLayout(false);
            Users.ResumeLayout(false);
            Users.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            Store.ResumeLayout(false);
            Store.PerformLayout();
            Logs.ResumeLayout(false);
            Logs.PerformLayout();
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
