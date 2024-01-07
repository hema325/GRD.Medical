namespace Domain.Events
{
    public abstract class EventBase : INotification
    {
        public bool IsFired { get; private set; }

        public void MarkAsFired()
            => IsFired = true;
    }
}
