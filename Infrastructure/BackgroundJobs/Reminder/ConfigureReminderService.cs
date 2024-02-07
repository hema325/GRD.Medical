using Infrastructure.BackgroundJobs.Reminder.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.BackgroundJobs.Reminder
{
    internal static class ConfigureReminderService
    {
        public static IServiceCollection AddReminderService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHostedService<ReminderBackgroundService>();
            services.AddScoped<IReminderService, ReminderService>();

            services.Configure<ReminderSettings>(configuration.GetSection(ReminderSettings.SectionName));

            return services;
        }
    }
}
