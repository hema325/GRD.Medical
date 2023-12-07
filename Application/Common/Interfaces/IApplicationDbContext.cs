using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<Author> Authors { get; }
        
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
