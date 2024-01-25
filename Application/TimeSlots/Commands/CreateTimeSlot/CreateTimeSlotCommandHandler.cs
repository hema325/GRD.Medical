using Application.TimeSlots.Queries;

namespace Application.TimeSlots.Commands.CreateTimeSlot
{
    internal class CreateTimeSlotCommandHandler : IRequestHandler<CreateTimeSlotCommand, TimeSlotDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public CreateTimeSlotCommandHandler(IApplicationDbContext context, ICurrentUser currentUser, IMapper mapper)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<TimeSlotDto> Handle(CreateTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var timeSlot = new TimeSlot
            {
                Start = TimeSpan.Parse(request.Start),
                End = TimeSpan.Parse(request.End),
                Day = Enum.Parse<Days>(request.Day),
                DoctorId = _currentUser.DoctorId.Value
            };


            timeSlot.AddDomainEvent(new EntityCreatedEvent(timeSlot));
            _context.TimeSlots.Add(timeSlot);
            await _context.SaveChangesAsync();

            return _mapper.Map<TimeSlotDto>(timeSlot);
        }
    }
}
