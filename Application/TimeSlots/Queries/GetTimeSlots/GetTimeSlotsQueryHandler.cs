using Application.Common.Extensions;
using Application.Common.Models;

namespace Application.TimeSlots.Queries.GetTimeSlots
{
    internal class GetTimeSlotsQueryHandler : IRequestHandler<GetTimeSlotsQuery, PaginatedList<TimeSlotDto>>
    {
        private IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private ICurrentUser _currentUser;

        public GetTimeSlotsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<PaginatedList<TimeSlotDto>> Handle(GetTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var timeSlots = await _context.TimeSlots.Where(ts => ts.DoctorId == _currentUser.DoctorId)
                .PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<TimeSlotDto>>(timeSlots);
        }
    }
}
