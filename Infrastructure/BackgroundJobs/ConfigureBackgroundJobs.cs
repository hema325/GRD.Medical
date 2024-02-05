using Infrastructure.BackgroundJobs.DBCleaner;
using Infrastructure.BackgroundJobs.Reminder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.BackgroundJobs
{
    internal static class ConfigureBackgroundJobs
    {
        public static IServiceCollection AddBackgroundJobs(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddReminderService(configuration);
            services.AddDBCleanerService(configuration);

            return services;
        }
    }
}
