using Domain.Entities.OwnedEntities;

namespace Domain.Entities
{
    public class Comment: EntityBase
    {
        public string? Content { get; set; }
        public DateTime CommentedOn { get; set; }
        public int OwnerId { get; set; }
        public int? ReplyTo { get; set; }
        public int PostId { get; set; }

        public Media? Media { get; set; }
        public User Owner { get; set; }
        public Post Post { get; set; }
        public ICollection<Comment> Replies { get; set; }

    }
}
