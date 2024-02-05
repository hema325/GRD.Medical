using Application.Common.Models;

namespace Application.AppointmentMessages.Commands.Queries.GetAppointmentMessages
{
    public class GetAppointmentMessagesQuery: PaginationBase, IRequest<PaginatedList<AppointmentMessageDto>>
    {
        public int AppointmentId { get; set; }
    }
}
