using Application.Authors.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Queries.GetArticleById
{
    public class GetArticleByIdQuery : IRequest<ArticleDto>
    {
        public int Id { get; set; }
    }
}
