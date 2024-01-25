using Application.TimeSlots.Queries;

namespace Application.TimeSlots.Commands.CreateTimeSlot
{
    public class CreateTimeSlotCommand: IRequest<TimeSlotDto>
    {
        public string Start { get; set; }
        public string End { get; set; }
        public string Day { get; set; }
    }
}
