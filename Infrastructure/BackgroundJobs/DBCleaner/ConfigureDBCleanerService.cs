using Infrastructure.BackgroundJobs.DBCleaner.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.BackgroundJobs.DBCleaner
{
    internal static class ConfigureDBCleanerService
    {
        public static IServiceCollection AddDBCleanerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<DBCleanerBackgroundService>();
            services.AddScoped<IDBCleanerService, DBCleanerService>();

            services.Configure<DBCleanerSettings>(configuration.GetSection(DBCleanerSettings.SectionName));

            return services;
        }
    }
}
