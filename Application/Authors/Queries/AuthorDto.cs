using Application.Common.Mappings;

namespace Application.Authors.Queries
{
    public class AuthorDto
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string? ImageUrl { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Author, AuthorDto>()
                    .ForMember(dest=>dest.ImageUrl, opt=> opt.MapFrom<ImageUrlResolver>());
            }
        }
    }
}
