using Application.Authors.Queries;
using Application.Common.Mappings;

namespace Application.MedicalAdvices.Queries
{
    public class MedicalAdviceDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime PublishedON { get; set; }
        public AuthorDto? Author { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Advice, MedicalAdviceDto>()
                    .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom<ImageUrlResolver>());
            }
        }
    }
}
