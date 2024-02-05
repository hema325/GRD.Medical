using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.BackgroundJobs.DBCleaner
{
    internal static class ConfigureDBCleanerService
    {
        public static IServiceCollection AddDBCleanerService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<DBCleanerService>();

            services.Configure<DBCleanerSettings>(configuration.GetSection(DBCleanerSettings.SectionName));

            return services;
        }
    }
}
