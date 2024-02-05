using Microsoft.EntityFrameworkCore;

namespace Application.Appointments.Queries.GetAppointment
{
    internal class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, AppointmentDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public GetAppointmentQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<AppointmentDto> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Appointments
                .Include(a => a.BillingInfo)
                .AsQueryable();

            if (_currentUser.Role == Roles.Patient)
            {
                query = query.Include(a => a.Doctor)
                    .Where(a => a.PatientId == _currentUser.Id);
            }
            else if (_currentUser.Role == Roles.Doctor)
            {
                query = query.Include(a => a.Patient)
                    .Where(a => a.DoctorId == _currentUser.Id);
            }

            var appointment = await query.FirstOrDefaultAsync(a=>a.Id == request.Id);

            if (appointment == null)
                throw new NotFoundException(nameof(Appointment));

            return _mapper.Map<AppointmentDto>(appointment);
        }
    }
}
