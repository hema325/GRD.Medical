using Infrastructure.BackgroundJobs.DBCleaner.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.BackgroundJobs.DBCleaner
{
    internal class DBCleanerBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DBCleanerSettings _settings;

        public DBCleanerBackgroundService(IServiceProvider serviceProvider, IOptions<DBCleanerSettings> settings)
        {
            _serviceProvider = serviceProvider;
            _settings = settings.Value;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                using(var scope = _serviceProvider.CreateScope())
                {
                    var cleaner = scope.ServiceProvider.GetRequiredService<IDBCleanerService>();
                    await cleaner.CleanAsync();
                }

                await Task.Delay(_settings.DelayInDays * 86400000);
            }
        }

    }
}
