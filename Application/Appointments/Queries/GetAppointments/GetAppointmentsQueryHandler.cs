using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Appointments.Queries.GetAppointments
{
    internal class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, PaginatedList<AppointmentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public GetAppointmentsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<PaginatedList<AppointmentDto>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Appointments
                .Include(a=>a.BillingInfo)
                .OrderByDescending(a => a.StartDateTime)
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

            var appointment = await query.PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<AppointmentDto>>(appointment);
        }
    }
}
