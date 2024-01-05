using Application.Common.Models;

namespace Application.Articles.Queries.GetArticles
{
    public class GetArticlesQuery : PaginationBase, IRequest<PaginatedList<ArticleDto>>
    {
        public string? Title { get; set; }
    }
}
