using Application.Common.Models;

namespace Application.Users.Queries.GetDoctors
{
    public class GetDoctorsQuery: PaginationBase, IRequest<PaginatedList<UserDto>>
    {
        public string? Name { get; set; }
        public int? Experience { get; set; }
        public int? SpecialityId { get; set; }
        public string? OrderBy { get; set; }
    }
}
