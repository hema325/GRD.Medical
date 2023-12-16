using Microsoft.AspNetCore.Http;

namespace Application.Articles.Commands.UpdateArticle
{
    public class UpdateArticleCommand : IRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public int AuthorId { get; set; }
    }
}
