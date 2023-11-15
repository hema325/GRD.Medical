using Application.Common.Interfaces;
using Infrastructure.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var affectedRows =  await base.SaveChangesAsync(cancellationToken);
            if(ChangeTracker.HasChanges()) await _publisher.DispatchDomainEventsAsync(this); 
            return affectedRows;
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<RefreshToken> RefreshTokens { get; private set; }
    }
}
