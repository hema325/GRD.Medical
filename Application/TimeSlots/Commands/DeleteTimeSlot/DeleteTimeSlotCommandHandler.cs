using Microsoft.EntityFrameworkCore;

namespace Application.TimeSlots.Commands.DeleteTimeSlot
{
    internal class DeleteTimeSlotCommandHandler : IRequestHandler<DeleteTimeSlotCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public DeleteTimeSlotCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(DeleteTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var timeSlot = await _context.TimeSlots.FindAsync(request.Id);

            if (timeSlot == null)
                throw new NotFoundException();

            if (timeSlot.DoctorId != _currentUser.DoctorId.Value)
                throw new ForbiddenException("You are not the owner of this time slot");

            _context.TimeSlots.Remove(timeSlot);
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
