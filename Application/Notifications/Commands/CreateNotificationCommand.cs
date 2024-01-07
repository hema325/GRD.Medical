using Application.Notifications.Queries;

namespace Application.Notifications.Commands
{
    public class CreateNotificationCommand: IRequest<NotificationDto>
    {
        public string Content { get; set; }
        public int ReferenceId { get; set; }
        public ReferenceTypes ReferenceType { get; set; }
        public int OwnerId { get; set; }
    }
}
