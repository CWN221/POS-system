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

            try
            {
                // Check if this user has any history
                const string checkQuery = @"
                        SELECT
                            (SELECT COUNT(*) FROM sales WHERE cashier_id = @Id) +
                            (SELECT COUNT(*) FROM returns WHERE requested_by = @Id OR approved_by = @Id) +
                            (SELECT COUNT(*) FROM stock_movements WHERE user_id = @Id) +
                            (SELECT COUNT(*) FROM audit_log WHERE user_id = @Id) +
                            (SELECT COUNT(*) FROM store_credit_ledger WHERE user_id = @Id) AS total_refs";

                int references;
                using (var checkCmd = new NpgsqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Id", userId);
                    references = Convert.ToInt32(await checkCmd.ExecuteScalarAsync());
                }

                if (references > 0)
                {
                    const string softQuery = "UPDATE users SET IsActive = FALSE WHERE id = @Id";

                    using var softCmd = new NpgsqlCommand(softQuery, conn);
                    softCmd.Parameters.AddWithValue("@Id", userId);

                    if (await softCmd.ExecuteNonQueryAsync() > 0)
                    {
                        await AppServices.GetRequiredService<AuditService>().LogAsync("USER_DEACTIVATED", "user", userId.ToString());
                        return $"This user has {references} historical record(s). They have been deactivated instead of deleted so that sales history stays intact.";
                    }
                    return "User not found.";
                }

                // If no history is found, safe to hard-delete
                const string deleteQuery = "DELETE FROM users WHERE id = @Id";

                using var deleteCmd = new NpgsqlCommand(deleteQuery, conn);
                deleteCmd.Parameters.AddWithValue("@Id", userId);

                if (await deleteCmd.ExecuteNonQueryAsync() > 0)
                {
                    await AppServices.GetRequiredService<AuditService>().LogAsync("USER_DELETED", "user", userId.ToString());
                    return "User deleted permanently.";
                }
                return "User not found.";
            }
            catch (PostgresException ex)
            {
                Logger.Log($"DeleteUser failed for ID {userId}: {ex.Message}");

                if (ex.SqlState == "23503")
                    return "This user cannot be deleted because they are referenced by existing records. Try deactivating them instead.";
                else
                    return $"Database error while deleting user: {ex.Message}";
            }
        }
    }
}
