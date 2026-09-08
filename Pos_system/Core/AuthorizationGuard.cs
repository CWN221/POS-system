using Pos_system.Models;
using Pos_system.Services;

namespace Pos_system.Core
{
    public static class AuthorizationGuard
    {
        public static void RequireLoggedIn()
        {
            if (!SessionManager.IsLoggedIn)
                throw new UnauthorizedAccessException("You must be logged in.");
        }

        public static void RequireAdmin()
        {
            RequireLoggedIn();
            if (!SessionManager.IsAdmin)
                throw new UnauthorizedAccessException("Admin access required.");
        }

        public static void RequireCapability(Func<UserModel, bool> capability, string action)
        {
            RequireLoggedIn();
            var user = SessionManager.CurrentUser!;
            if (!capability(user))
                throw new UnauthorizedAccessException($"You do not have permission to {action}.");
        }

        public static void RequireManageInventory() =>
            RequireCapability(u => u.CanManageInventory, "manage inventory");

        public static void RequireViewReports() =>
            RequireCapability(u => u.CanViewReports, "view reports");

        public static void RequireProcessReturns() =>
            RequireCapability(u => u.CanProcessReturns, "process returns");

        public static void RequireManageSettings() =>
            RequireCapability(u => u.CanManageSettings, "manage settings");

        public static void RequireManageUsers() =>
            RequireCapability(u => u.CanManageUsers, "manage users");
    }
}
