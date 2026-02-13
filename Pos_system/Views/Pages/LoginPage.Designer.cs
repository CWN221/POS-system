namespace Pos_system.Views.Pages
{
    partial class LoginPage
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
            usernameLabel = new MaterialSkin.Controls.MaterialLabel();
            passwordLabel = new MaterialSkin.Controls.MaterialLabel();
            usernameTextBox = new MaterialSkin.Controls.MaterialTextBox2();
            passwordTextBox = new MaterialSkin.Controls.MaterialMaskedTextBox();
            loginBtn = new MaterialSkin.Controls.MaterialButton();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // usernameLabel
            // 
            usernameLabel.AutoSize = true;
            usernameLabel.Depth = 0;
            usernameLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            usernameLabel.Location = new Point(869, 216);
            usernameLabel.MouseState = MaterialSkin.MouseState.HOVER;
            usernameLabel.Name = "usernameLabel";
            usernameLabel.Size = new Size(72, 19);
            usernameLabel.TabIndex = 0;
            usernameLabel.Text = "Username";
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Depth = 0;
            passwordLabel.Font = new Font("Roboto", 14F, FontStyle.Regular, GraphicsUnit.Pixel);
            passwordLabel.Location = new Point(869, 430);
            passwordLabel.MouseState = MaterialSkin.MouseState.HOVER;
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(71, 19);
            passwordLabel.TabIndex = 1;
            passwordLabel.Text = "Password";
            // 
            // usernameTextBox
            // 
            usernameTextBox.AnimateReadOnly = false;
            usernameTextBox.BackgroundImageLayout = ImageLayout.None;
            usernameTextBox.CharacterCasing = CharacterCasing.Normal;
            usernameTextBox.Depth = 0;
            usernameTextBox.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            usernameTextBox.HideSelection = true;
            usernameTextBox.LeadingIcon = null;
            usernameTextBox.Location = new Point(866, 276);
            usernameTextBox.MaxLength = 32767;
            usernameTextBox.MouseState = MaterialSkin.MouseState.OUT;
            usernameTextBox.Name = "usernameTextBox";
            usernameTextBox.PasswordChar = '\0';
            usernameTextBox.PrefixSuffixText = null;
            usernameTextBox.ReadOnly = false;
            usernameTextBox.RightToLeft = RightToLeft.No;
            usernameTextBox.SelectedText = "";
            usernameTextBox.SelectionLength = 0;
            usernameTextBox.SelectionStart = 0;
            usernameTextBox.ShortcutsEnabled = true;
            usernameTextBox.Size = new Size(312, 48);
            usernameTextBox.TabIndex = 4;
            usernameTextBox.TabStop = false;
            usernameTextBox.TextAlign = HorizontalAlignment.Left;
            usernameTextBox.TrailingIcon = null;
            usernameTextBox.UseSystemPasswordChar = false;
            // 
            // passwordTextBox
            // 
            passwordTextBox.AllowPromptAsInput = true;
            passwordTextBox.AnimateReadOnly = false;
            passwordTextBox.AsciiOnly = false;
            passwordTextBox.BackgroundImageLayout = ImageLayout.None;
            passwordTextBox.BeepOnError = false;
            passwordTextBox.CutCopyMaskFormat = MaskFormat.IncludeLiterals;
            passwordTextBox.Depth = 0;
            passwordTextBox.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            passwordTextBox.HidePromptOnLeave = false;
            passwordTextBox.HideSelection = true;
            passwordTextBox.InsertKeyMode = InsertKeyMode.Default;
            passwordTextBox.LeadingIcon = null;
            passwordTextBox.Location = new Point(869, 491);
            passwordTextBox.Mask = "";
            passwordTextBox.MaxLength = 32767;
            passwordTextBox.MouseState = MaterialSkin.MouseState.OUT;
            passwordTextBox.Name = "passwordTextBox";
            passwordTextBox.PasswordChar = '\0';
            passwordTextBox.PrefixSuffixText = null;
            passwordTextBox.PromptChar = '_';
            passwordTextBox.ReadOnly = false;
            passwordTextBox.RejectInputOnFirstFailure = false;
            passwordTextBox.ResetOnPrompt = true;
            passwordTextBox.ResetOnSpace = true;
            passwordTextBox.RightToLeft = RightToLeft.No;
            passwordTextBox.SelectedText = "";
            passwordTextBox.SelectionLength = 0;
            passwordTextBox.SelectionStart = 0;
            passwordTextBox.ShortcutsEnabled = true;
            passwordTextBox.Size = new Size(312, 48);
            passwordTextBox.SkipLiterals = true;
            passwordTextBox.TabIndex = 5;
            passwordTextBox.TabStop = false;
            passwordTextBox.TextAlign = HorizontalAlignment.Left;
            passwordTextBox.TextMaskFormat = MaskFormat.IncludeLiterals;
            passwordTextBox.TrailingIcon = null;
            passwordTextBox.UseSystemPasswordChar = false;
            passwordTextBox.ValidatingType = null;
            // 
            // loginBtn
            // 
            loginBtn.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            loginBtn.Cursor = Cursors.Hand;
            loginBtn.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            loginBtn.Depth = 0;
            loginBtn.HighEmphasis = true;
            loginBtn.Icon = null;
            loginBtn.Location = new Point(869, 589);
            loginBtn.Margin = new Padding(3, 5, 3, 5);
            loginBtn.MouseState = MaterialSkin.MouseState.HOVER;
            loginBtn.Name = "loginBtn";
            loginBtn.NoAccentTextColor = Color.Empty;
            loginBtn.Size = new Size(64, 36);
            loginBtn.TabIndex = 6;
            loginBtn.Text = "Login";
            loginBtn.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            loginBtn.UseAccentColor = false;
            loginBtn.UseVisualStyleBackColor = true;
            loginBtn.Click += loginBtn_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.POS_logo;
            pictureBox1.Location = new Point(114, 153);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(450, 405);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13F);
            label1.Location = new Point(869, 122);
            label1.Name = "label1";
            label1.Size = new Size(66, 30);
            label1.TabIndex = 8;
            label1.Text = "Login";
            // 
            // LoginPage
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1294, 666);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(loginBtn);
            Controls.Add(passwordTextBox);
            Controls.Add(usernameTextBox);
            Controls.Add(passwordLabel);
            Controls.Add(usernameLabel);
            Font = new Font("Tahoma", 9F);
            Name = "LoginPage";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel usernameLabel;
        private MaterialSkin.Controls.MaterialLabel passwordLabel;
        private MaterialSkin.Controls.MaterialTextBox2 usernameTextBox;
        private MaterialSkin.Controls.MaterialMaskedTextBox passwordTextBox;
        private MaterialSkin.Controls.MaterialButton loginBtn;
        private PictureBox pictureBox1;
        private Label label1;
    }
}
