using Application.Common.Models;

namespace Application.TimeSlots.Queries.GetTimeSlots
{
    public class GetTimeSlotsQuery: PaginationBase, IRequest<PaginatedList<TimeSlotDto>>
    {
    }
}
