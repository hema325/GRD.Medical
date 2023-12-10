using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.MedicalAdvices.Queries.GetMedicalAdvices
{
    internal class GetAdvicesQuaryHandler : IRequestHandler<GetAdvicesQuary, PaginatedList<AdviceDto>>
    {
        private readonly IMapper mapper;
        private readonly IApplicationDbContext context;

        public GetAdvicesQuaryHandler(IMapper mapper, IApplicationDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<PaginatedList<AdviceDto>> Handle(GetAdvicesQuary request, CancellationToken cancellationToken)
        {
            var query = context.Advices.Include(a => a.Author).AsQueryable();

            if (request.Title != null)
                query = query.Where(a => a.Title.StartsWith(request.Title));

            return await query.PaginateAsync<Advice, AdviceDto>(request.PageNumber, request.PageSize, mapper);
        }
    }
}
