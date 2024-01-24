using Application.Common.Models;

namespace Application.Specialists.Queries.GetSpecialists
{
    public class GetSpecialitiesQuery: PaginationBase, IRequest<PaginatedList<SpecialityDto>>
    {
        public string? Name { get; set; }
    }
}
