using Application.Common.Models;

namespace Application.UserChatBotMessages.Queries.GetUserChatBotMessages
{
    public class GetUserChatBotMessagesQuery: PaginationBase, IRequest<PaginatedList<UserChatBotMessageDto>>
    {
        public DateTime? Before { get; set; }
    }
}
