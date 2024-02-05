namespace Application.Appointments.Commands.MarkAppointmentCompleted
{
    internal class MarkAppointmentCompletedCommandHandler: IRequestHandler<MarkAppointmentCompletedCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public MarkAppointmentCompletedCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(MarkAppointmentCompletedCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments.FindAsync(request.Id);

            if (appointment.PatientId != _currentUser.Id && appointment.DoctorId != _currentUser.Id)
                throw new ForbiddenException("You are not part of this appointment.");

            if (DateTime.UtcNow < appointment.EndDateTime)
                throw new ForbiddenException("This appointment is not completed yet.");


            appointment.Status = AppointmentStatuses.Completed;

            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
