using Application.Notifications.Queries;

namespace Application.Notifications.Commands.CreateNotification
{
    internal class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, NotificationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public CreateNotificationCommandHandler(IApplicationDbContext context, ICurrentUser currentUser, IMapper mapper)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<NotificationDto> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notification = new Notification
            {
                Content = request.Content,
                ReferenceId = request.ReferenceId,
                ReferenceType = request.ReferenceType,
                OwnerId = request.OwnerId,
                InitiatorId = _currentUser.Id!.Value,
                NotifiedOn = DateTime.UtcNow,
                IsRead = false
            };

            notification.AddDomainEvent(new EntityCreatedEvent(notification));
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            notification.Initiator = await _context.Users.FindAsync(_currentUser.Id);

            return _mapper.Map<NotificationDto>(notification);
        }
    }
}
