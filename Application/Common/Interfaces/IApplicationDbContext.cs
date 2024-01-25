using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        DbSet<Author> Authors { get; }
        DbSet<Article> Articles { get; }
        DbSet<Advice> Advices { get; }
        DbSet<Post> Posts { get; }
        DbSet<Comment> Comments { get; }
        DbSet<UserChatBotMessage> UserChatBotMessages { get; }
        DbSet<Notification> Notifications { get; }
        DbSet<Speciality> Specialities { get; }
        DbSet<Language> Languages { get; }
        DbSet<Doctor> Doctors { get; }
        DbSet<TimeSlot> TimeSlots { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
