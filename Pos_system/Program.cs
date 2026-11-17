using Pos_system.Core;
using Pos_system.Services;
using Pos_system.Views.Pages;
using Pos_system.logs;
using Pos_system.Database;
using LiveChartsCore;

namespace Pos_system
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            LiveCharts.Configure(c => c
                .AddLiveChartsAppSettings()
                .AddLiveChartsRenderSettings());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ApplicationConfiguration.Initialize();

            DotNetEnv.Env.Load();
            AppServices.Configure();

            try
            {
                string? connectionString = Environment.GetEnvironmentVariable("POS_DB_CONNECTION");
                if (string.IsNullOrWhiteSpace(connectionString))
                {
                    MessageBox.Show(
                        "Database connection string not found. Set POS_DB_CONNECTION in your .env file.",
                        "Configuration Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                DatabaseInitializer.InitializeDatabase(connectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Database error: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var culture = new System.Globalization.CultureInfo("en-KE");
            System.Globalization.CultureInfo.CurrentCulture = culture;
            System.Globalization.CultureInfo.CurrentUICulture = culture;
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;

            while (true)
            {
                using (var login = new LoginPage())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                        break;
                }

#if !DEBUG
                var user = SessionManager.CurrentUser;
                if (user?.MustChangePassword == true)
                {
                    using var changePassword = new ChangePasswordForm(user.Id, forceChange: true);
                    if (changePassword.ShowDialog() != DialogResult.OK)
                    {
                        SessionManager.Logout();
                        continue;
                    }
                    user.MustChangePassword = false;
                }
#endif

                Application.Run(new DashboardForm());
                SessionManager.Logout();
            }
        }
    }
}