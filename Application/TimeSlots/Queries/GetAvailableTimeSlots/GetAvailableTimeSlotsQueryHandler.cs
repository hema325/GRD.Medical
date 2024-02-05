using Microsoft.EntityFrameworkCore;

namespace Application.TimeSlots.Queries.GetAvailableTimeSlots
{
    internal class GetAvailableTimeSlotsQueryHandler : IRequestHandler<GetAvailableTimeSlotsQuery, IEnumerable<TimeSlotDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAvailableTimeSlotsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TimeSlotDto>> Handle(GetAvailableTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var targetDate = DateTime.Parse(request.Date);

            var query =  _context.TimeSlots
                 .Where(ts => ts.Doctor.UserId == request.DoctorId 
                 && !_context.Appointments.Any(
                     a=> a.StartDateTime.Date == targetDate.Date 
                     && a.StartDateTime.TimeOfDay == ts.Start
                     && a.EndDateTime.TimeOfDay == ts.End
                     && a.Status != AppointmentStatuses.Completed)
                 && ts.Day == Enum.Parse<Days>(targetDate.DayOfWeek.ToString()))
                 .AsQueryable();

            if (targetDate == DateTime.UtcNow.Date)
                query = query.Where(ts => DateTime.UtcNow.TimeOfDay < ts.Start);

            var timeSlots = await query.ToListAsync();

            return _mapper.Map<IEnumerable<TimeSlotDto>>(timeSlots);
        }
    }
}
