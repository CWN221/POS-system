using Npgsql;
using Pos_system.Database;
using Pos_system.logs;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.Services
{
    public class AuditService
    {
        public async Task LogAsync(string action, string? entityType = null, string? entityId = null, string? details = null)
        {
            try
            {
                using var conn = DBConnection.GetConnection();
                const string query = @"
                    INSERT INTO audit_log (user_id, branch_id, action, entity_type, entity_id, details)
                    VALUES (@userId, @branchId, @action, @entityType, @entityId, @details)";

                using var cmd = new NpgsqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", AppContext.CurrentUserId > 0 ? AppContext.CurrentUserId : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@branchId", AppContext.CurrentBranchId > 0 ? AppContext.CurrentBranchId : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@action", action);
                cmd.Parameters.AddWithValue("@entityType", entityType ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@entityId", entityId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@details", details ?? (object)DBNull.Value);

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Logger.LogWarning($"Audit log failed: {ex.Message}");
            }
        }
    }
}
