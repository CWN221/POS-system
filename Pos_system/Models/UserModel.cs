/*
 * UserModel.cs
 * 
 * Represents a user in the system with properties for authentication and RBAC
 * 
 */

namespace Pos_system.Models
{

    public enum UserRole
    {
        Admin,
        Manager,
        Cashier
    }

    public class UserModel
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public string PasswordHash { get; set; }
        public required UserRole Role { get; set; }
        public bool IsActive { get; set; } // to indicate if the user is active or not
        public int BranchId { get; set; }
        public bool MustChangePassword { get; set; }

        public bool CanMakeSales {
            get
            {
                return Role == UserRole.Admin || Role == UserRole.Manager || Role == UserRole.Cashier;
            }
        }

        public bool CanManageInventory
        {
            get
            {
                return Role == UserRole.Admin || Role == UserRole.Manager;
            }
        }

        public bool CanViewReports {
            get
            {
                return Role == UserRole.Admin || Role == UserRole.Manager;
            }
        }

        public bool CanProcessReturns =>
            Role == UserRole.Admin || Role == UserRole.Manager || Role == UserRole.Cashier;

        public bool CanManageSettings =>
            Role == UserRole.Admin;

        public bool CanManageUsers =>
            Role == UserRole.Admin;

    }
}
