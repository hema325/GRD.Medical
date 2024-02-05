using Application.Notifications.Commands.CreateNotification;

namespace Application.Comments.Events
{
    internal class CommentCreatedEventHandler : INotificationHandler<CommentCreatedEvent>
    {
        private readonly IApplicationDbContext _context;
        private readonly ISender _sender;
        private readonly INotificationSender _notificationSender;
        private readonly ICurrentUser _currentUser;

        public CommentCreatedEventHandler(ISender sender, INotificationSender notificationSender, ICurrentUser currentUser, IApplicationDbContext context)
        {
            _sender = sender;
            _notificationSender = notificationSender;
            _currentUser = currentUser;
            _context = context;
        }

        public async Task Handle(CommentCreatedEvent notification, CancellationToken cancellationToken)
        {
            var notificationCommand = new CreateNotificationCommand
            {
                ReferenceId = notification.Comment.Id,
                InitiatorId = _currentUser.Id
            };

            if(notification.Comment.ReplyTo != null)
            {
                var parentComment = await _context.Comments.FindAsync(notification.Comment.ReplyTo);
                notificationCommand.Content = string.Format(NotificationTemplates.Reply, _currentUser.Name);
                notificationCommand.ReferenceType = ReferenceTypes.Reply;
                notificationCommand.OwnerId = parentComment!.OwnerId;
            }
            else
            {
                var post = await _context.Posts.FindAsync(notification.Comment.PostId);
                notificationCommand.Content = string.Format(NotificationTemplates.Comment, _currentUser.Name);
                notificationCommand.ReferenceType = ReferenceTypes.Comment;
                notificationCommand.OwnerId = post!.OwnerId;
            }

            if (notificationCommand.OwnerId != _currentUser.Id)
            {
                var notificationDto = await _sender.Send(notificationCommand);
                await _notificationSender.SendToUserAsync(notificationCommand.OwnerId, notificationDto);
            }
        }
    }
}
