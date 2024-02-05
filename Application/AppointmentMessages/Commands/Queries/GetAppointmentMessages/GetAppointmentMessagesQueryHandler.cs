using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.AppointmentMessages.Commands.Queries.GetAppointmentMessages
{
    internal class GetAppointmentMessagesQueryHandler : IRequestHandler<GetAppointmentMessagesQuery, PaginatedList<AppointmentMessageDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public GetAppointmentMessagesQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<PaginatedList<AppointmentMessageDto>> Handle(GetAppointmentMessagesQuery request, CancellationToken cancellationToken)
        {
            if (!await _context.Appointments.AnyAsync(a => a.Id == request.AppointmentId &&
               (a.PatientId == _currentUser.Id || a.DoctorId == _currentUser.Id)))
                throw new ForbiddenException("You are not part of this appointment.");

            var appointments = await _context.AppointmentMessages
                .Include(am=>am.Sender)
                .OrderByDescending(am=>am.MessagedOn)
                .Where(am => am.AppointmentId == request.AppointmentId)
                .PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<AppointmentMessageDto>>(appointments);
        }
    }
}
