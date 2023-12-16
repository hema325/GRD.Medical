using Application.Common.Extensions;
using Application.Common.Models;

namespace Application.Authors.Queries.GetAuthors
{
    internal class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, PaginatedList<AuthorDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<AuthorDto>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Authors.AsQueryable();

            if (request.Name != null)
                query = query.Where(a => a.Name.StartsWith(request.Name));

            var authors = await query.PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<AuthorDto>>(authors);
        }
    }
}
