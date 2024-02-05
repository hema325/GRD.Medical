using Application.Common.Extensions;
using Application.Common.Models;
using AutoMapper.QueryableExtensions;

namespace Application.BillingInfos.Queries.GetBillingInfos
{
    internal class GetBillingInfosQueryHandler : IRequestHandler<GetBillingInfosQuery, PaginatedList<BillingInfoDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public GetBillingInfosQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUser currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<PaginatedList<BillingInfoDto>> Handle(GetBillingInfosQuery request, CancellationToken cancellationToken)
        {
            return await _context.BillingInfos
                .Where(b => b.Appointment.PatientId == _currentUser.Id.Value ||
                b.Appointment.DoctorId == _currentUser.Id.Value)
                .ProjectTo<BillingInfoDto>(_mapper.ConfigurationProvider)
                .PaginateAsync(request.PageNumber, request.PageSize);
        }
    }
}
