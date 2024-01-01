using Application.Common.Models;

namespace Application.UserChatBotMessages.Queries.GetUserChatBotMessages
{
    public class GetUserChatBotMessagesQuery: IRequest<PaginatedList<UserChatBotMessageDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
