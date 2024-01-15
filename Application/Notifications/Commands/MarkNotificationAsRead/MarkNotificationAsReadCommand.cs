namespace Application.Notifications.Commands.MarkAsRead
{
    public class MarkNotificationAsReadCommand: IRequest
    {
        public int Id { get; set; }
    }
}
