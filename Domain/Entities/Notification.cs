namespace Domain.Entities
{
    public class Notification: EntityBase
    {
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime NotifiedOn { get; set; }
        public int ReferenceId { get; set; }
        public ReferenceTypes ReferenceType { get; set; }
        public int? InitiatorId { get; set; }
        public int OwnerId { get; set; }
        public User? Initiator { get; set; }
        public User Owner { get; set; }

    }
}
