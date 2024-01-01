using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IChatBot
    {
        Task<ChatBotResult> GetResponseAsync(string message);
    }
}
