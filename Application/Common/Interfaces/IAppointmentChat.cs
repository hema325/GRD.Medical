using Application.AppointmentMessages.Commands.Queries;

namespace Application.Common.Interfaces
{
    public interface IAppointmentChat
    {
        Task SendToUserAsync(int to, AppointmentMessageDto message);
    }
}
