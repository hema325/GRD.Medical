
using Application.Notifications.Queries;

namespace Application.Common.Interfaces
{
    public interface INotificationSender
    {
        Task SendToUserAsync(int to, NotificationDto notification);
    }
}
