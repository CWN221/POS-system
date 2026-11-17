using Npgsql;

namespace Pos_system.Database
{
    public static class CloudConnectionFactory
    {
        public static NpgsqlConnection? GetConnection()
        {
            if (!Services.CloudConfig.IsEnabled)
                return null;

            var connString = Services.CloudConfig.GetCloudConnectionString();
            if (string.IsNullOrWhiteSpace(connString))
                return null;

            var conn = new NpgsqlConnection(connString);
            conn.Open();
            return conn;
        }

        public static bool TestConnection()
        {
            try
            {
                using var conn = GetConnection();
                return conn != null;
            }
            catch
            {
                return false;
            }
        }
    }
}