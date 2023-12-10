namespace Domain.Entities
{
    public class Advice:EntityBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishedOn { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
    }
}
