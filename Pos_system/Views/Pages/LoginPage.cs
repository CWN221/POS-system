using Microsoft.VisualBasic.ApplicationServices;
using Pos_system.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pos_system.Views.Pages
{
    public partial class LoginPage : Form
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            Authorization auth = new Authorization();
            var success = auth.Login(usernameTextBox.Text, passwordTextBox.Text);

            if (success != null) {

                SessionManager.login(success);

                this.DialogResult = DialogResult.OK;
                MessageBox.Show("Login successful!");                

                // Hide login form
                this.Close();

            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

    }
}
