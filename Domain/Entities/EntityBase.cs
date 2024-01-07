using System.ComponentModel.DataAnnotations.Schema;
using Domain.Events;

namespace Domain.Common.Abstractions
{
    public abstract class EntityBase
    {
        public int Id { get; set; }

        #region domainEvents
        private List<EventBase> _domainEvents = new();

        [NotMapped]
        public IReadOnlyCollection<EventBase> DomainEvents => _domainEvents;

        public void AddDomainEvent(EventBase domainEvent)
            => _domainEvents.Add(domainEvent);

        public void ClearDomainEvents()
            => _domainEvents.Clear();

        public void RemoveDomainEvent(EventBase domainEvent)
            => _domainEvents.Remove(domainEvent);
        #endregion
    }
}
