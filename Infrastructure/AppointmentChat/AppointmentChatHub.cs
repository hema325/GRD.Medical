using Infrastructure.AppointmentChat.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.AppointmentChat
{
    [Authorize]
    internal class AppointmentChatHub: Hub
    {
        public async Task NotifyWritingStatus(int target, bool status)
        {
            await Clients.User(target.ToString()).SendAsync(AppointmentChatHubMethods.IsWriting, status);
        }
    }
}
