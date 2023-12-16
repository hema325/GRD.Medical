using Microsoft.EntityFrameworkCore;

namespace Application.Articles.Queries.GetArticleById
{
    public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ArticleDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetArticleByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ArticleDto> Handle(GetArticleByIdQuery request, CancellationToken cancellationToken)
        {
            var article = await _context.Articles.Include(a=>a.Author).FirstOrDefaultAsync(a=>a.Id == request.Id);
            
            if (article == null)
                throw new NotFoundException(nameof(Article));

            return _mapper.Map<ArticleDto>(article);
        }
    }
}
