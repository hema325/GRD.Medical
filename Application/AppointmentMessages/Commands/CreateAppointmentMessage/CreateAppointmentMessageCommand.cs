using Application.AppointmentMessages.Commands.Queries;
using Microsoft.AspNetCore.Http;

namespace Application.AppointmentMessages.Commands.CreateAppointmentMessage
{
    public class CreateAppointmentMessageCommand: IRequest<AppointmentMessageDto>
    {
        public string? Content { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
        public int AppointmentId { get; set; }
    }
}
