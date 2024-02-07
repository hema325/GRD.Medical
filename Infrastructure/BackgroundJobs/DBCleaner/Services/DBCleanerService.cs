using Microsoft.EntityFrameworkCore;

namespace Infrastructure.BackgroundJobs.DBCleaner.Services
{
    internal class DBCleanerService: IDBCleanerService
    {
        private readonly IApplicationDbContext _context;

        public DBCleanerService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CleanAsync()
        {
            await RemoveExpiredRefreshTokens();
            await RemoveOldUnConfirmedUsers();
            await _context.SaveChangesAsync();
        }

        private async Task RemoveOldUnConfirmedUsers()
        {
            var users = await _context.Users
                .Where(u => !u.IsEmailConfirmed && DateTime.UtcNow.AddDays(-30) > u.JoinedOn)
                .ToListAsync();
            _context.Users.RemoveRange(users);
        }

        private async Task RemoveExpiredRefreshTokens()
        {
            var refreshTokens = await _context.RefreshTokens
                .Where(rt => DateTime.UtcNow >= rt.ExpiresOn)
                .ToListAsync();

            _context.RefreshTokens.RemoveRange(refreshTokens);
        }
    }
}
