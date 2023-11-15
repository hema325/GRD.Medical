using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Common.EventHandlers
{
    internal class EntityCreatedEventHandler : INotificationHandler<EntityCreatedEvent>
    {
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<EntityCreatedEvent> _logger;

        public EntityCreatedEventHandler(ICurrentUser currentUser, ILogger<EntityCreatedEvent> logger)
        {
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {id} is created by user {userName} with id {id} at {dateTime}",
                notification.Entity.GetType().Name,
                notification.Entity.Id,
                _currentUser.Email,
                _currentUser.Id,
                DateTime.UtcNow);

            return Task.CompletedTask;
        }
    }
}
