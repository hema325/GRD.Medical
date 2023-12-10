using Infrastructure.Common;
using Infrastructure.Persistance.Seeds;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistance
{
    internal class ApplicationDbContext: DbContext, IApplicationDbContext
    {
        private readonly IPublisher _publisher;
        private readonly ModelSeeder _seeder;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IPublisher publisher,
        ModelSeeder seeder) : base(options)
        {
            _publisher = publisher;
            _seeder = seeder;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            _seeder.Seed(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _publisher.DispatchDomainEventsAsync(this);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<RefreshToken> RefreshTokens { get; private set; }
        public DbSet<Author> Authors { get; private set; }
        public DbSet<Article> Articles { get; private set; }
        public DbSet<Advice> Advices { get; private set; }
    }
}
