namespace Application.Common.Models
{
    public class ChatBotResult
    {
        public bool Succeeded { get; init; }
        public string Message { get; init; }

        public static ChatBotResult Success(string message) => new ChatBotResult{ Succeeded = true, Message = message };
        public static ChatBotResult Failure() => new ChatBotResult{ Succeeded = false };
    }
}
