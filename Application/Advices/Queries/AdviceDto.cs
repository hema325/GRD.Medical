using Application.Authors.Queries;
using Application.Common.Mappings.Reslovers;

namespace Application.MedicalAdvices.Queries
{
    public class AdviceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime PublishedOn { get; set; }
        public AuthorDto? Author { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Advice, AdviceDto>()
                    .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<ImageUrlResolver>());
            }
        }
    }
}
