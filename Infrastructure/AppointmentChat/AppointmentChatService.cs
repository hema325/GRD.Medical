using Application.AppointmentMessages.Commands.Queries;
using Infrastructure.AppointmentChat.Constants;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.AppointmentChat
{
    internal class AppointmentChatService: IAppointmentChat
    {
        private readonly IHubContext<AppointmentChatHub> _hubContext;

        public AppointmentChatService(IHubContext<AppointmentChatHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public Task SendToUserAsync(int to, AppointmentMessageDto message)
        { 
             return _hubContext.Clients
                .User(to.ToString())
                .SendAsync(AppointmentChatHubMethods.ReceiveMessage, message);
        }
    }
}
