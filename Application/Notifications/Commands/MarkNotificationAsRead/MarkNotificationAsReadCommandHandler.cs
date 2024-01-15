namespace Application.Notifications.Commands.MarkAsRead
{
    internal class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public MarkNotificationAsReadCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await _context.Notifications.FindAsync(request.Id);

            if (notification == null)
                throw new NotFoundException(nameof(Notification));

            if (_currentUser.Id != notification.OwnerId && _currentUser.Role != Roles.Admin)
                throw new ForbiddenException("You are not the owner of this notification");

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
