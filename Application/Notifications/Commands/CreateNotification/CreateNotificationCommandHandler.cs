using Application.Notifications.Queries;

namespace Application.Notifications.Commands.CreateNotification
{
    internal class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, NotificationDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateNotificationCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
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
                InitiatorId = request.InitiatorId,
                NotifiedOn = DateTime.UtcNow,
                IsRead = false
            };

            notification.AddDomainEvent(new EntityCreatedEvent(notification));
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            if(request.InitiatorId != null)
                notification.Initiator = await _context.Users.FindAsync(notification.InitiatorId);

            return _mapper.Map<NotificationDto>(notification);
        }
    }
}
