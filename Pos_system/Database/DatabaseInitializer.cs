using Npgsql;
using System.IO;
using Pos_system.logs;

namespace Pos_system.Database
{
    public static class DatabaseInitializer
    {
        public static void InitializeDatabase(string connectionString)
        {
            CreateDatabaseIfNeeded(connectionString);
            RunSqlFile(connectionString, "schema.sql");
            RunSqlFile(connectionString, "migrations.sql");
            Logger.Log("Database schema initialized successfully.");
        }

        private static void CreateDatabaseIfNeeded(string connectionString)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            var databaseName = builder.Database;

            builder.Database = "postgres";

            using var conn = new NpgsqlConnection(builder.ConnectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand("SELECT 1 FROM pg_database WHERE datname = @dbname", conn);
            cmd.Parameters.AddWithValue("@dbname", databaseName);

            if (cmd.ExecuteScalar() == null)
            {
                using var createCmd = new NpgsqlCommand($"CREATE DATABASE \"{databaseName}\"", conn);
                createCmd.ExecuteNonQuery();
                Logger.Log($"Database '{databaseName}' created.");
            }
        }

        private static void RunSqlFile(string connectionString, string fileName)
        {
            using var conn = new NpgsqlConnection(connectionString);
            conn.Open();

            var schemaPath = ResolveSqlPath(fileName);
            var schemaSql = File.ReadAllText(schemaPath);
            var statements = schemaSql.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (var statement in statements)
            {
                var trimmed = statement.Trim();
                if (string.IsNullOrWhiteSpace(trimmed))
                    continue;

                try
                {
                    using var cmd = new NpgsqlCommand(trimmed, conn);
                    cmd.ExecuteNonQuery();
                }
                catch (PostgresException ex) when (
                    ex.SqlState is "42P07" or "42P16" or "23505" or "42701" or "42710")
                {
                    // Object/column/index already exists — safe to skip.
                }
                catch (PostgresException ex)
                {
                    Logger.LogWarning($"SQL warning in {fileName}: {ex.Message}");
                }
            }
        }

        private static string ResolveSqlPath(string fileName)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(baseDir, "Database", fileName);

            if (!File.Exists(path))
                path = Path.Combine(Directory.GetCurrentDirectory(), "Database", fileName);

            if (!File.Exists(path))
                throw new FileNotFoundException($"{fileName} not found in the Database folder.");

            return path;
        }
    }
}
