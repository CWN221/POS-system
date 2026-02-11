
namespace Pos_system.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public string PasswordHash { get; set; }
        public required string Role { get; set; } // Available roles; "Admin", "Manager", "Cashier"

        public bool CanMakeSales {
            get
            {
                return Role == "Admin" || Role == "Manager" || Role == "Cashier";
            }
        }

        public bool CanManageInventory
        {
            get
            {
                return Role == "Admin" || Role == "Manager";
            }
        }

        public bool CanViewReports {
            get
            {
                return Role == "Admin" || Role == "Manager";
            }
        }

        public bool IsActive { get; set; } // to indicate if the user is active or not

    }
}
