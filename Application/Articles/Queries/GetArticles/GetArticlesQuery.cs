using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Queries.GetArticles
{
    public class GetArticlesQuery : IRequest<PaginatedList<ArticleDto>>
    {
        public string? Title { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
