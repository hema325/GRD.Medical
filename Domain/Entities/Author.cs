namespace Domain.Entities
{
    public class Author: EntityBase
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string ImageUrl { get; set; }
    }
}
