using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.logs;
using Pos_system.Models;

namespace Pos_system.Services
{
    public class Authorization
    {
        public async Task<string> Signup(string username, string password, UserRole role, int branchId)
        {
            AuthorizationGuard.RequireManageUsers();

            var passwordCheck = PasswordValidator.Validate(password);
            if (!passwordCheck.Success)
                return passwordCheck.Message;

            if (CheckUsername(username))
                return "Username already exists. Please choose a different username.";

            using var conn = DBConnection.GetConnection();
            const string query = @"INSERT INTO users(Username, PasswordHash, Role, IsActive, MustChangePassword, BranchId)
                                   VALUES(@Username, @Passwordhash, @Role, @IsActive, FALSE, @BranchId)";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Passwordhash", BCrypt.Net.BCrypt.HashPassword(password));
                cmd.Parameters.AddWithValue("@Role", role.ToString());
                cmd.Parameters.AddWithValue("@IsActive", true);
                cmd.Parameters.AddWithValue("@BranchId", branchId);

                await cmd.ExecuteNonQueryAsync();
                await AppServices.GetRequiredService<AuditService>().LogAsync("USER_CREATED", "user", username, $"Role: {role}, Branch: {branchId}");
                return "User Registration Successful";
            }
            catch (PostgresException e)
            {
                return "Registration failed: " + e.Message;
            }
        }

        public async Task<UserModel?> Login(string username, string password)
        {
            using var conn = DBConnection.GetConnection();
            const string query = @"SELECT Id, Username, PasswordHash, Role, IsActive, MustChangePassword, BranchId, FailedLoginAttempts, LockoutUntil
                                   FROM users WHERE Username = @Username";

            try
            {
                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", username);

                using var reader = await cmd.ExecuteReaderAsync();
                if (!reader.Read())
                    return null;

                if (!reader.GetBoolean(reader.GetOrdinal("IsActive")))
                    return null;

                var hashedPassword = reader.GetString(reader.GetOrdinal("PasswordHash"));
                if (!BCrypt.Net.BCrypt.Verify(password, hashedPassword))
                    return null;

                return new UserModel
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Username = reader.GetString(reader.GetOrdinal("Username")),
                    PasswordHash = hashedPassword,
                    Role = Enum.Parse<UserRole>(reader.GetString(reader.GetOrdinal("Role"))),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                    MustChangePassword = reader.GetBoolean(reader.GetOrdinal("MustChangePassword")),
                    FailedLoginAttempts = reader["FailedLoginAttempts"] != DBNull.Value ? Convert.ToInt32(reader["FailedLoginAttempts"]) : 0,
                    LockoutUntil = reader["LockoutUntil"] != DBNull.Value ? Convert.ToDateTime(reader["LockoutUntil"]) : null,
                    BranchId = reader["BranchId"] != DBNull.Value ? Convert.ToInt32(reader["BranchId"]) : 0
                };
            }
            catch (PostgresException e)
            {
                Logger.Log("Login failed: " + e.Message);
                return null;
            }
        }

        public async Task<string> ChangePassword(int userId, string currentPassword, string newPassword)
        {
            var passwordCheck = PasswordValidator.Validate(newPassword);
            if (!passwordCheck.Success)
                return passwordCheck.Message;

            using var conn = DBConnection.GetConnection();
            const string query = "SELECT PasswordHash FROM users WHERE Id = @Id";

            using var selectCmd = new NpgsqlCommand(query, conn);
            selectCmd.Parameters.AddWithValue("@Id", userId);
            var hash = await selectCmd.ExecuteScalarAsync() as string;

            if (hash == null || !BCrypt.Net.BCrypt.Verify(currentPassword, hash))
                return "Current password is incorrect.";

            const string updateQuery = @"UPDATE users SET PasswordHash = @Hash, MustChangePassword = FALSE WHERE Id = @Id";
            using var updateCmd = new NpgsqlCommand(updateQuery, conn);
            updateCmd.Parameters.AddWithValue("@Hash", BCrypt.Net.BCrypt.HashPassword(newPassword));
            updateCmd.Parameters.AddWithValue("@Id", userId);
            await updateCmd.ExecuteNonQueryAsync();

            await AppServices.GetRequiredService<AuditService>()
                .LogAsync("PASSWORD_CHANGED", "user", userId.ToString());

            return "Password changed successfully.";
        }

        private bool CheckUsername(string username)
        {
            using var conn = DBConnection.GetConnection();
            using var cmd = new NpgsqlCommand("SELECT COUNT(*) FROM users WHERE Username = @Username", conn);
            cmd.Parameters.AddWithValue("@Username", username);
            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }


        public const int MaxFailedAttempts = 5;
        public static readonly TimeSpan LockoutDuration = TimeSpan.FromMinutes(15); // Lock out user for 15 minutes if max attempt is reached

        public async Task<(bool isLocked, DateTime? lockedUntil)> IsLockedOutAsync(string username)
        {
            try
            {
                using var conn = DBConnection.GetConnection();
                const string query = @"SELECT LockoutUntil FROM users WHERE Username = @username";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);

                var result = await cmd.ExecuteScalarAsync();
                if (result == null || result == DBNull.Value)
                    return (false, null);

                var lockoutUntil = Convert.ToDateTime(result);
                if (lockoutUntil <= DateTime.Now)
                {
                    await ClearFailedLoginsAsync(username);
                    return (false, null);
                }

                return (true, lockoutUntil);
            }
            catch (Exception ex)
            {
                Logger.Log("IsLockedOutAsync failed: " + ex.Message);
                return (false, null);
            }
        }

        public async Task<(int attempts, bool nowLocked, DateTime? lockedUntil)> RecordFailedLoginAsync(string username)
        {
            try
            {
                using var conn = DBConnection.GetConnection();
                await using var transaction = await conn.BeginTransactionAsync();

                int attempts;

                const string getQuery = @"SELECT FailedLoginAttempts FROM users
                                          WHERE Username = @username FOR UPDATE";
                using (var cmd = new NpgsqlCommand(getQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    var result = await cmd.ExecuteScalarAsync();
                    attempts = result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
                }

                attempts++;

                bool nowLocked = attempts >= MaxFailedAttempts;
                DateTime? lockedUntil = nowLocked ? DateTime.Now.Add(LockoutDuration) : null;

                const string updateQuery = @"
                    UPDATE users
                    SET FailedLoginAttempts = @attempts,
                        LockoutUntil = @lockoutUntil
                    WHERE Username = @username";

                using (var cmd = new NpgsqlCommand(updateQuery, conn, transaction))
                {
                    cmd.Parameters.AddWithValue("@attempts", attempts);
                    cmd.Parameters.AddWithValue("@lockoutUntil", (object?)lockedUntil ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@username", username);
                    await cmd.ExecuteNonQueryAsync();
                }

                await transaction.CommitAsync();
                return (attempts, nowLocked, lockedUntil);
            }
            catch (Exception ex)
            {
                Logger.Log("RecordFailedLoginAsync failed: " + ex.Message);
                return (0, false, null);
            }
        }

        public async Task ClearFailedLoginsAsync(string username)
        {
            try
            {
                using var conn = DBConnection.GetConnection();
                const string query = @"
                    UPDATE users
                    SET FailedLoginAttempts = 0, LockoutUntil = NULL
                    WHERE Username = @username";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Logger.Log("ClearFailedLoginsAsync failed: " + ex.Message);
            }
        }
    }
}
