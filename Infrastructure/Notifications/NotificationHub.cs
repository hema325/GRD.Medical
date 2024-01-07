using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Notifications
{
    [Authorize]
    public class NotificationHub: Hub
    {
    }
}
