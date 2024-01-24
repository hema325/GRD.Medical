using Application.Users.Queries;

namespace Application.Notifications.Queries
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; }
        public DateTime NotifiedOn { get; set; }
        public int ReferenceId { get; set; }
        public string ReferenceType { get; set; }
        public UserDto Initiator { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Notification, NotificationDto>();
            }
        }
    }
}
