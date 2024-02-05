using Application.Common.Models;

namespace Application.Reviews.Queries.GetReviews
{
    public class GetReviewsQuery: PaginationBase, IRequest<PaginatedList<ReviewDto>>
    {
        public int DoctorId { get; set; }
    }
}
