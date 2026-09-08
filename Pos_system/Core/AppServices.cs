using Microsoft.Extensions.DependencyInjection;
using Pos_system.Services;

namespace Pos_system.Core
{
    public static class AppServices
    {
        private static ServiceProvider? _provider;

        public static void Configure()
        {
            var services = new ServiceCollection();

            services.AddSingleton<AuditService>();
            services.AddSingleton<ProductDAO>();
            services.AddSingleton<CreateSaleDAO>();
            services.AddSingleton<TransactionDAO>();
            services.AddSingleton<ReportsService>();
            services.AddSingleton<ReturnsService>();
            services.AddSingleton<ZReportService>();
            services.AddSingleton<ZReportPdfService>();
            services.AddSingleton<StoreSettingsService>();
            services.AddSingleton<Authorization>();
            services.AddSingleton<UserRepository>();
            services.AddSingleton<GetUsers>();
            services.AddSingleton<VatDAO>();

            _provider = services.BuildServiceProvider();
        }

        public static T GetRequiredService<T>() where T : notnull
        {
            if (_provider == null)
                Configure();

            return _provider!.GetRequiredService<T>();
        }
    }
}
