using Application.Account.Queries;
using Application.Common.Models;

namespace Application.Posts.Queries
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PostedOn { get; set; }

        public UserDto Owner { get; set; }
        public ICollection<MediaDto> Medias { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Post, PostDto>();
            }
        }
    }
}
