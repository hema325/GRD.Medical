using Microsoft.AspNetCore.Http;

namespace Application.Articles.Commands.CreateArticle
{
    public class CreateArticleCommand : IRequest<int>
    {
        public string Title { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Content { get; set; }
        public IFormFile Image { get; set; }
        public int AuthorId { get; set; }
    }
}
