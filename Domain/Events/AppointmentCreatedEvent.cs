using Domain.Entities;

namespace Domain.Events
{
    public class AppointmentCreatedEvent: EventBase
    {
        public Appointment Appointment { get; set; }

        public AppointmentCreatedEvent(Appointment appointment)
        {
            Appointment = appointment;
        }
    }
}
