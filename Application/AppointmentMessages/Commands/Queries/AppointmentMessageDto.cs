using Application.Common.Models;
using Application.Users.Queries;

namespace Application.AppointmentMessages.Commands.Queries
{
    public class AppointmentMessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public IEnumerable<MediaDto> Medias { get; set; }
        public DateTime MessagedOn { get; set; }
        public UserDto Sender { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<AppointmentMessage, AppointmentMessageDto>();
            }
        }
    }
}
