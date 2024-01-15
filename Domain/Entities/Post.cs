using Domain.Entities.OwnedEntities;

namespace Domain.Entities
{
    public class Post : EntityBase
    {
        public string? Content { get; set; }
        public DateTime PostedOn { get; set; }
        public int OwnerId { get; set; }

        public User? Owner { get; set; }
        public ICollection<Media> Medias { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
