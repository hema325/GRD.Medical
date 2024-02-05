
namespace Application.TimeSlots.Queries.GetAvailableTimeSlots
{
    public class GetAvailableTimeSlotsQuery: IRequest<IEnumerable<TimeSlotDto>>
    {
        public int DoctorId { get; set; }
        public string Date { get; set; }
    }
}
