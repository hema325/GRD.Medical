using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Common.EventHandlers
{
    internal class EntityUpdatedEventHandler : INotificationHandler<EntityUpdatedEvent>
    {
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<EntityUpdatedEventHandler> _logger;

        public EntityUpdatedEventHandler(ICurrentUser currentUser, ILogger<EntityUpdatedEventHandler> logger)
        {
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityUpdatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {id} is updated by user {userName} with id {id} at {dateTime}",
                notification.Entity.GetType().Name,
                notification.Entity.Id,
                _currentUser.Email,
                _currentUser.Id,
                DateTime.UtcNow);

            return Task.CompletedTask;
        }
    }
}
