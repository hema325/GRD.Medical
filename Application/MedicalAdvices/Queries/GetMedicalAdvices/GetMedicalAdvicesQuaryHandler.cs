using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Queries.GetMedicalAdvices
{
    internal class GetMedicalAdvicesQuaryHandler : IRequestHandler<GetMedicalAdvicesQuary, PaginatedList<MedicalAdviceDto>>
    {
        private readonly IMapper mapper;
        private readonly IApplicationDbContext context;

        public GetMedicalAdvicesQuaryHandler(IMapper mapper, IApplicationDbContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<PaginatedList<MedicalAdviceDto>> Handle(GetMedicalAdvicesQuary request, CancellationToken cancellationToken)
        {

            var query = context.MedicalAdvices.Include(a => a.Author).AsQueryable();

            if (request.Title != null)
                query = query.Where(a => a.Title.StartsWith(request.Title));
            return await query.PaginateAsync<MedicalAdvice, MedicalAdviceDto>(request.PageNumber, request.PageSize, mapper);

        }
    }
}
