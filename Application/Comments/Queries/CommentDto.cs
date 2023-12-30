using Application.Account.Queries;
using Application.Common.Models;

namespace Application.Comments.Queries
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CommentedOn { get; set; }
        public int PostId { get; set; }
        public int ReplyTo { get; set; }

        public MediaDto Media { get; set; }
        public UserDto Owner { get; set; }
        public IEnumerable<CommentDto> Replies { get; set; }
        public int TotalRepliesCount { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<Comment, CommentDto>()
                    .ForMember(dto => dto.Replies, opt => opt.MapFrom(c => c.Replies.OrderByDescending(c => c.CommentedOn).Take(1)))
                    .ForMember(dto=>dto.TotalRepliesCount, opt=> opt.MapFrom(c=>c.Replies.Count())); // reslover won't work using this approach
            }
        }
    }
}
