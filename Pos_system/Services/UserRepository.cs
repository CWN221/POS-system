using Pos_system.Core;
using Pos_system.Models;
using Pos_system.Database;
using Npgsql;
using Pos_system.logs;

namespace Pos_system.Services
{
    public class UserRepository
    {
        public async Task<string> UpdateUser(UserModel userInfo)
        {
            AuthorizationGuard.RequireManageUsers();

            using var conn = DBConnection.GetConnection();
            const string query = @"UPDATE users SET Username = @Username, Role = @Role, IsActive = @IsActive, BranchId = @BranchId
                                   WHERE id = @Id";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", userInfo.Username);
                cmd.Parameters.AddWithValue("@Role", userInfo.Role.ToString());
                cmd.Parameters.AddWithValue("@IsActive", userInfo.IsActive);
                cmd.Parameters.AddWithValue("@BranchId", userInfo.BranchId);
                cmd.Parameters.AddWithValue("@Id", userInfo.Id);

                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    await AppServices.GetRequiredService<AuditService>()
                        .LogAsync("USER_UPDATED", "user", userInfo.Id.ToString());
                    return "User information updated.";
                }
                return "Error updating user information.";
            }
            catch (PostgresException ex)
            {
                Logger.Log("Error updating user information: " + ex.Message);
                return "Error updating user information";
            }
        }

        public async Task<string> DeleteUser(int userId)
        {
            AuthorizationGuard.RequireManageUsers();

            if (SessionManager.CurrentUser?.Id == userId)
                return "You cannot delete your own account while logged in.";

            using var conn = DBConnection.GetConnection();
            const string query = "DELETE FROM users WHERE Id=@Id";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", userId);
                await cmd.ExecuteNonQueryAsync();

                await AppServices.GetRequiredService<AuditService>()
                    .LogAsync("USER_DELETED", "user", userId.ToString());

                return "User deleted successfully.";
            }
            catch (PostgresException ex)
            {
                Logger.Log("Error deleting user: " + ex.Message);
                return "Error deleting user.";
            }
        }
    }
}
