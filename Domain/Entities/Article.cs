namespace Domain.Entities
{
    public class Article : EntityBase
    {
        public string Title { get; set; }
        public DateTime PublishedOn {  get; set; } 
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
    }
}
