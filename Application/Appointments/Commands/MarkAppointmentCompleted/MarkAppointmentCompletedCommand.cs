namespace Application.Appointments.Commands.MarkAppointmentCompleted
{
    public class MarkAppointmentCompletedCommand: IRequest
    {
        public int Id { get; set; }
    }
}
