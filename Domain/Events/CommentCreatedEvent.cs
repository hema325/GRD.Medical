using Domain.Entities;

namespace Domain.Events
{
    public class CommentCreatedEvent : EventBase
    {
        public Comment Comment { get; }

        public CommentCreatedEvent(Comment comment)
        {
            Comment = comment;
        }
    }
}
