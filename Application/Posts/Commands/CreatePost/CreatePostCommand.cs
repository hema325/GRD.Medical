using Application.Posts.Queries;
using Microsoft.AspNetCore.Http;

namespace Application.Posts.Commands.CreatePost
{
    public class CreatePostCommand : IRequest<PostDto>
    {
        public string? Content { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
