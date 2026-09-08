using Pos_system.Models;
using Pos_system.Core;

namespace Pos_system.Services
{
    public class SessionManager
    {
        public static UserModel? _currentUser;
        public static UserModel? CurrentUser => _currentUser;

        public static event Action<UserModel?>? OnSessionChanged;

        public static void Login(UserModel user)
        {
            _currentUser = user;
            Core.AppContext.CurrentUserId = user.Id;
            Core.AppContext.CurrentUserRole = user.Role;
            OnSessionChanged?.Invoke(_currentUser);
        }

        public static void Logout()
        {
            _currentUser = null;
            Core.AppContext.Reset();
            OnSessionChanged?.Invoke(null);
        }

        public static bool IsLoggedIn => _currentUser != null;

        public static bool IsAdmin => _currentUser?.Role == UserRole.Admin;
        public static bool IsCashier => _currentUser?.Role == UserRole.Cashier;
        public static bool IsManager => _currentUser?.Role == UserRole.Manager;
    }
}
