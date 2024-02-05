using Application.Common.Models;

namespace Application.Appointments.Queries.GetAppointments
{
    public class GetAppointmentsQuery: PaginationBase, IRequest<PaginatedList<AppointmentDto>>
    {
    }
}
