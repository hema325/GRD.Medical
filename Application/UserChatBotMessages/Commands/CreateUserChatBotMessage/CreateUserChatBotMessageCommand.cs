namespace Application.UserChatBotMessages.Commands.CreateUserChatBotMessage
{
    public class CreateUserChatBotMessageCommand: IRequest<CreateUserChatBotMessageCommandResponse>
    {
        public string Message { get; set; }
    }
}
