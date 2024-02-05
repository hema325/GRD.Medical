using Application.Common.Extensions;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Reviews.Queries.GetReviews
{
    internal class GetReviewsQueryHandler : IRequestHandler<GetReviewsQuery, PaginatedList<ReviewDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetReviewsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginatedList<ReviewDto>> Handle(GetReviewsQuery request, CancellationToken cancellationToken)
        {
            var reviews = await _context.Reviews
                .Include(r => r.Owner)
                .OrderByDescending(r=>r.ReviewedOn)
                .Where(r => r.Doctor.UserId == request.DoctorId)
                .PaginateAsync(request.PageNumber, request.PageSize);

            return _mapper.Map<PaginatedList<ReviewDto>>(reviews);
        }
    }
}
