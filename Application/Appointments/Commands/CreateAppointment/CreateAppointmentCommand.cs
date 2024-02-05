namespace Application.Appointments.Commands.CreateAppointment
{
    public class CreateAppointmentCommand: IRequest<int>
    {
        public string Date { get; set; }
        public int DoctorId { get; set; }
        public int TimeSlotId { get; set; }
        public string PaymentIntentId { get; set; }
    }
}
