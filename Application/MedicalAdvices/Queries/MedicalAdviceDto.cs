using Application.Authors.Queries;
using Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MedicalAdvices.Queries
{
    public class MedicalAdviceDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string? ImgUrl { get; set; }
        public DateTime PublishedON { get; set; }
        public AuthorDto? Author { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<MedicalAdvice, MedicalAdviceDto>()
                    .ForMember(dest => dest.ImgUrl, opt => opt.MapFrom<ImageUrlResolver>());
            }
        }
    }
}
