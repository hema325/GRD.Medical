using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System;

namespace Application.Common.EventHandlers
{
    internal class EntityDeletedEventHandler : INotificationHandler<EntityDeletedEvent>
    {
        private readonly ICurrentUser _currentUser;
        private readonly ILogger<EntityDeletedEventHandler> _logger;

        public EntityDeletedEventHandler(ICurrentUser currentUser, ILogger<EntityDeletedEventHandler> logger)
        {
            _currentUser = currentUser;
            _logger = logger;
        }

        public Task Handle(EntityDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{entityName} with id {id} is deleted by user {userName} with id {id} at {dateTime}",
               notification.Entity.GetType().Name,
               notification.Entity.Id,
               _currentUser.Email,
               _currentUser.Id,
               DateTime.UtcNow);

            return Task.CompletedTask;
        }
    }
}
