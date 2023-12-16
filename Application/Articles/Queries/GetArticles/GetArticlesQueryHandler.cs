using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

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
                query = query.Where(a=>a.Title.StartsWith(request.Title));

            var articles = await query.PaginateAsync(request.pageNumber,request.pageSize);

            return _mapper.Map<PaginatedList<ArticleDto>>(articles);
        }
    }
}
