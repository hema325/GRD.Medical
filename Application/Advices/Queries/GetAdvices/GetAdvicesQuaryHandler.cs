using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.MedicalAdvices.Queries.GetMedicalAdvices
{
    internal class GetAdvicesQuaryHandler : IRequestHandler<GetAdvicesQuary, PaginatedList<AdviceDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;

        public GetAdvicesQuaryHandler(IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PaginatedList<AdviceDto>> Handle(GetAdvicesQuary request, CancellationToken cancellationToken)
        {
            var query = _context.Advices.Include(a => a.Author).AsQueryable();

            if (request.Title != null)
                query = query.Where(a => a.Title.StartsWith(request.Title));

            var advices = await query.PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<AdviceDto>>(advices);
        }
    }
}
