using Infrastructure.Common;
using Infrastructure.Persistance.Conversions;
using Infrastructure.Persistance.Seeds;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistance
{
    internal class ApplicationDbContext: DbContext, IApplicationDbContext
    {
        private readonly IPublisher _publisher;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            ModelSeeder.Seed(modelBuilder);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<DateTime>()
                .HaveConversion<DateTimeToUtcConverter>();

            base.ConfigureConventions(configurationBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var affectedRows = await base.SaveChangesAsync(cancellationToken);
            await _publisher.DispatchDomainEventsAsync(this);
            return affectedRows;
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<RefreshToken> RefreshTokens { get; private set; }
        public DbSet<Author> Authors { get; private set; }
        public DbSet<Article> Articles { get; private set; }
        public DbSet<Advice> Advices { get; private set; }
        public DbSet<Post> Posts { get; private set; }
        public DbSet<Comment> Comments { get; private set; }
        public DbSet<UserChatBotMessage> UserChatBotMessages { get; private set; }
        public DbSet<Notification> Notifications { get; private set; }
        public DbSet<Speciality> Specialities { get; private set; }
        public DbSet<Language> Languages { get; private set; }
        public DbSet<Doctor> Doctors { get; private set; }
        public DbSet<TimeSlot> TimeSlots { get; private set; }
        public DbSet<Appointment> Appointments { get; private set; }
        public DbSet<BillingInfo> BillingInfos { get; private set; }
        public DbSet<AppointmentMessage> AppointmentMessages { get; private set; }
        public DbSet<Review> Reviews { get; private set; }
    }
}
