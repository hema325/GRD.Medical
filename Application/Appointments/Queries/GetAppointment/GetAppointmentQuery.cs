namespace Application.Appointments.Queries.GetAppointment
{
    public class GetAppointmentQuery: IRequest<AppointmentDto>
    {
        public int Id { get; set; }
    }
}
