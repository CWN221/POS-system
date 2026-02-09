using Pos_system.Services;
using System;
using System.Net;
using Authorization = Pos_system.Services.Authorization;

namespace Pos_system.ViewModels
{
    public class LoginViewModel
    {
        Authorization authorization_service = new();

        // Getter & Setter Methods
        public required string Username { get; set; }
        public required string Password { get; set; }

        public void UserLogin()
        {
            var userLogin = authorization_service.login(Username, Password);
        }

        public void UserSignup()
        {
            var userLogin = authorization_service.signup(Username, Password);
        }

    }
}
