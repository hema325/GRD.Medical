using Application.Common.Models;

namespace Application.MedicalAdvices.Queries.GetMedicalAdvices
{
    public class GetAdvicesQuary : PaginationBase, IRequest<PaginatedList<AdviceDto>>
    {
        public string? Title { get; set; }
    }
}
