using Pos_system.logs;
using Authorization = Pos_system.Services.Authorization;

namespace Pos_system.ViewModels
{
    public class LoginViewModel
    {
        Authorization authorization_service = new();

        // Getter & Setter Methods
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }

        private readonly string[] ValidRoles = {"Admin", "Manager", "Cashier"};

        // Method to handle user login
        public void UserLogin()
        {
            var userLogin = authorization_service.Login(Username, Password);

            try
            {
                if (userLogin != null)
                {
                    MessageBox.Show($"Welcome, {userLogin.Username}");
                }
                else
                {
                    MessageBox.Show("Login failed. Check username/password or account may be inactive.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log("Error during login: " + ex.Message);
            }
        }


        // Method to handle user signup
        public void UserSignup()
        {
            try
            {
                var userLogin = authorization_service.Signup(Username, Password, Role);

                if (!ValidRoles.Contains(Role))
                {
                    MessageBox.Show("Invalid role selected.");
                    return;
                }

                if (userLogin == "User Registration Successful")
                {
                    MessageBox.Show(userLogin);
                }
                else
                {
                    MessageBox.Show(userLogin);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error during signup" + ex.Message);
            }
        }

    }
}
