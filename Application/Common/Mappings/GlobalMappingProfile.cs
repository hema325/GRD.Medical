using Application.Common.Mappings.Reslovers;
using Application.Common.Models;
using Domain.Entities.OwnedEntities;

namespace Application.Common.Mappings
{
    internal class GlobalMappingProfile: Profile
    {
        public GlobalMappingProfile()
        {
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>));

            CreateMap<Media, MediaDto>()
                .ForMember(dto => dto.Url, opt => opt.MapFrom<MediaUrlResolver>());
        }
    }
}
