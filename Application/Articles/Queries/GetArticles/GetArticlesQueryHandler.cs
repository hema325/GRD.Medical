using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Queries.GetArticles
{
    public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, PaginatedList<ArticleDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetArticlesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ArticleDto>> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Articles.Include(a=>a.Author).AsQueryable();

            if(request.Title != null)
                query = query.Where(a=>a.Title == request.Title);

            return await query.PaginateAsync<Article,ArticleDto>(request.pageNumber,request.pageSize, _mapper);
        }
    }
}
