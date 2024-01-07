using Domain.Common.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    internal static class MediatorExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IPublisher publisher, DbContext context)
        {
            var entries = context.ChangeTracker.Entries<EntityBase>().Where(e => e.Entity.DomainEvents.Any()).ToList();
            var domainEvents = entries.SelectMany(e => e.Entity.DomainEvents);

            await Task.WhenAll(domainEvents.Where(e => !e.IsFired).Select(e =>
            {
                e.MarkAsFired();
                return publisher.Publish(e);
            }));
            entries.ForEach(e => e.Entity.ClearDomainEvents());
        }
    }
}
