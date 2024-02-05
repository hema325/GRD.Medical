using Application.Notifications.Commands.CreateNotification;
using Microsoft.EntityFrameworkCore;

namespace Application.Reviews.Events
{
    internal class ReviewCreatedHandler : INotificationHandler<ReviewCreated>
    {
        private readonly IApplicationDbContext _context;
        private readonly INotificationSender _notificationSender;
        private readonly ISender _sender;
        private readonly ICurrentUser _currentUser;

        public ReviewCreatedHandler(IApplicationDbContext context,
                                    INotificationSender notificationSender,
                                    ISender sender,
                                    ICurrentUser currentUser)
        {
            _context = context;
            _notificationSender = notificationSender;
            _sender = sender;
            _currentUser = currentUser;
        }
        public async Task Handle(ReviewCreated notification, CancellationToken cancellationToken)
        {
            var review = notification.Review;

            var doctorId = await _context.Doctors
                .Where(d => d.Id == review.DoctorId)
                .Select(d => d.UserId)
                .FirstOrDefaultAsync();

            var notificationCommand = new CreateNotificationCommand
            {
                ReferenceId = review.Id,
                Content = string.Format(NotificationTemplates.Review, _currentUser.Name),
                ReferenceType = ReferenceTypes.Review,
                OwnerId = doctorId,
                InitiatorId = _currentUser.Id
            };

            var notificationDto = await _sender.Send(notificationCommand);
            await _notificationSender.SendToUserAsync(notificationCommand.OwnerId, notificationDto);
        }
    }
}
