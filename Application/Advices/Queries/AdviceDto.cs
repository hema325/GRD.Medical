using Application.Authors.Queries;
using Application.Common.Mappings.Reslovers;

namespace Application.MedicalAdvices.Queries
{
    public class AdviceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime PublishedOn { get; set; }
        public AuthorDto? Author { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Advice, AdviceDto>()
                    .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom<ImageUrlResolver>());
            }
        }
    }
}
