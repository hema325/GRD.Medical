namespace Domain.Entities
{
    public class UserChatBotMessage: EntityBase
    {
        public string Content { get; set; }
        public DateTime MessagedOn { get; set; }
        public int OwnerId { get; set; }
        public bool IsBotMessage { get; set; }

        public User Owner { get; set; }
    }
}
