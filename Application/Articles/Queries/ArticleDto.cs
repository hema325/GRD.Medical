using Application.Authors.Queries;
using Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Articles.Queries
{
    public class ArticleDto
    {
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public AuthorDto Author { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Article, ArticleDto>()
                    .ForMember(des => des.ImageUrl, opt => opt.MapFrom<ImageUrlResolver>()); 
            }
        }
    }
}
