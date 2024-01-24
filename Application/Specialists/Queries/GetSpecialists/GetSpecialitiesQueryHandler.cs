using Application.Common.Extensions;
using Application.Common.Models;

namespace Application.Specialists.Queries.GetSpecialists
{
    internal class GetSpecialitiesQueryHandler : IRequestHandler<GetSpecialitiesQuery, PaginatedList<SpecialityDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetSpecialitiesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<SpecialityDto>> Handle(GetSpecialitiesQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Specialities.AsQueryable();

            if (request.Name != null)
                query = query.Where(s => s.Name.StartsWith(request.Name));

            var scpecialists = await query.PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<SpecialityDto>>(scpecialists);
        }
    }
}
