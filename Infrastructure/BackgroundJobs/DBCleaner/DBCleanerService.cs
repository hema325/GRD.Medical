using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Infrastructure.BackgroundJobs.DBCleaner
{
    internal class DBCleanerService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DBCleanerSettings _settings;

        public DBCleanerService(IServiceProvider serviceProvider, IOptions<DBCleanerSettings> settings)
        {
            _serviceProvider = serviceProvider;
            _settings = settings.Value;

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                var context = GetApplicationContext();
                await RemoveExpiredRefreshTokens(context);
                await RemoveOldUnConfirmedUsers(context);
                await context.SaveChangesAsync();

                await Task.Delay(_settings.DelayInDays * 86400000);
            }
        }

        private static async Task RemoveOldUnConfirmedUsers(IApplicationDbContext context)
        {
            var users = await context.Users
                .Where(u => !u.IsEmailConfirmed && DateTime.UtcNow.AddDays(-30) > u.JoinedOn)
                .ToListAsync();
            context.Users.RemoveRange(users);
        }

        private static async Task RemoveExpiredRefreshTokens(IApplicationDbContext context)
        {
            var refreshTokens = await context.RefreshTokens
                .Where(rt => DateTime.UtcNow >= rt.ExpiresOn)
                .ToListAsync();

            context.RefreshTokens.RemoveRange(refreshTokens);
        }

        private IApplicationDbContext GetApplicationContext()
        {
            var scope = _serviceProvider.CreateScope();
            return scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
        }

    }
}
