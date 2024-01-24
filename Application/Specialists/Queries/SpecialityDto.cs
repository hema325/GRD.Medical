using Application.Common.Mappings.Reslovers;

namespace Application.Specialists.Queries
{
    public class SpecialityDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Speciality, SpecialityDto>()
                    .ForMember(dto => dto.ImageUrl, opt => opt.MapFrom<ImageUrlResolver>());
            }
        }
    }
}
