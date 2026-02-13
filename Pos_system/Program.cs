using Pos_system.Views.Pages;
using Pos_system.Services;

namespace Pos_system
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            while(true)
            {
                using (LoginPage login = new LoginPage())
                {
                    if (login.ShowDialog() != DialogResult.OK)
                    {
                        break;
                    }                    
                }
                SessionManager.Logout();
                Application.Run(new DashboardForm());
            }
           
        }
    }
}