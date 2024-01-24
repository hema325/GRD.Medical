using Application.Common.Extensions;
using Application.Common.Models;

namespace Application.Languages.Queries.GetLanguages
{
    internal class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, PaginatedList<LanguageDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLanguagesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<LanguageDto>> Handle(GetLanguagesQuery request, CancellationToken cancellationToken)
        {
            var languages = await _context.Languages.PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<LanguageDto>>(languages);
        }
    }
}
