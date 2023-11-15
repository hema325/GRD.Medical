namespace Domain.Events
{
    public class EntityCreatedEvent : EventBase
    {
        public EntityBase Entity { get; }

        public EntityCreatedEvent(EntityBase entity)
        {
            Entity = entity;
        }
    }
}
