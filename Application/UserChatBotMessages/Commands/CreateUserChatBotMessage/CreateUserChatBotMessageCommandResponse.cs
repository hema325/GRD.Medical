using Application.UserChatBotMessages.Queries;

namespace Application.UserChatBotMessages.Commands.CreateUserChatBotMessage
{
    public class CreateUserChatBotMessageCommandResponse
    {
        public UserChatBotMessageDto UserMessage { get; set; }
        public UserChatBotMessageDto ChatBotMessage { get; set; }
        public bool IsFailedToGenerateResponse { get; set; }
    }
}
