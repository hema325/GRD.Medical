using Application.Common.Models;

namespace Application.BillingInfos.Queries.GetBillingInfos
{
    public class GetBillingInfosQuery: PaginationBase ,IRequest<PaginatedList<BillingInfoDto>>
    {
    }
}
