using Domain.Entities.OwnedEntities;

namespace Domain.Entities
{
    public class AppointmentMessage: EntityBase
    {
        public string? Content { get; set; }
        public DateTime MessagedOn { get; set; }
        public ICollection<Media> Medias { get; set; }

        public int AppointmentId { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public Appointment Appointment { get; set; }
    }
}
