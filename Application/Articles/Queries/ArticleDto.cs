using Application.Authors.Queries;
using Application.Common.Mappings.Reslovers;

namespace Application.Articles.Queries
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublishedOn { get; set; }
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
