namespace Application.UserChatBotMessages.Queries
{
    public class UserChatBotMessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime MessagedOn { get; set; }
        public bool IsBotMessage { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<UserChatBotMessage, UserChatBotMessageDto>();
            }
        }
    }
}
