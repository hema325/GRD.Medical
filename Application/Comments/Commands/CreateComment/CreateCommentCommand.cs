using Application.Comments.Queries;
using Microsoft.AspNetCore.Http;

namespace Application.Comments.Commands.CreateComment
{
    public class CreateCommentCommand: IRequest<CommentDto>
    {
        public string? Content { get; set; }
        public int? ReplyTo { get; set; }
        public int PostId { get; set; }
        public IFormFile Image { get; set; }
    }
}
