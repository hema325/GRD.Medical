using Application.Common.Models;

namespace Application.Notifications.Queries.GetNotifications
{
    public class GetNotificationsQuery: PaginationBase, IRequest<PaginatedList<NotificationDto>>
    {
        public DateTime? Before { get; set; }
    }
}
