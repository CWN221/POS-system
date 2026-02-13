using Pos_system.Models;

namespace Pos_system.Services
{
    public class SessionManager
    {
        public static UserModel? CurrentUser { get; private set; }

        public static void login(UserModel user)
        {
            CurrentUser = user;
        }
        public static void Logout()
        {
            CurrentUser = null;
        }

        public static bool IsLoggedIn()
        {
            return CurrentUser != null;
        }

    }
}
