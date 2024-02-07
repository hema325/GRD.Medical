using Infrastructure.BackgroundJobs.Reminder.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.BackgroundJobs.Reminder
{
    internal class ReminderBackgroundService : BackgroundService
    {
        private readonly ReminderSettings _settings;
        private readonly IServiceProvider _serviceProvider;

        public ReminderBackgroundService(IOptions<ReminderSettings> settings, IServiceProvider serviceProvider)
        {
            _settings = settings.Value;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scrop = _serviceProvider.CreateScope())
                {
                    var reminder = scrop.ServiceProvider.GetRequiredService<IReminderService>();
                    await reminder.RemindAsync();
                }

                await Task.Delay(_settings.DelayInSeconds * 1000);
            }
        }
    }
}
