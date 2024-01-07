using Application.Notifications.Queries;
using Infrastructure.Notifications.Constants;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications
{
    internal class NotificationSenderService: INotificationSender
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationSenderService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task SendToUserAsync(int to, NotificationDto notification) 
        {
            return _hubContext.Clients.User(to.ToString())
                .SendAsync(NotificationHubMethods.ServerNotification, notification);
        }
    }
}
