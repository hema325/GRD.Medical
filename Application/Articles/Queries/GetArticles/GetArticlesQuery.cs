using Application.Common.Models;

namespace Application.Articles.Queries.GetArticles
{
    public class GetArticlesQuery : IRequest<PaginatedList<ArticleDto>>
    {
        public string? Title { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
