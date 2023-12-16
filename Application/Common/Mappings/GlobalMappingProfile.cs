using Application.Common.Models;

namespace Application.Common.Mappings
{
    internal class GlobalMappingProfile: Profile
    {
        public GlobalMappingProfile()
        {
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));
        }
    }
}
