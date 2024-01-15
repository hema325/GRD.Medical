using Application.Common.Models;
using Application.Posts.Commands.CreatePost;
using Application.Posts.Commands.DeletePost;
using Application.Posts.Queries;
using Application.Posts.Queries.GetByCommentId;
using Application.Posts.Queries.GetPost;
using Application.Posts.Queries.GetPosts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/posts")]
    [Authorize]
    public class PostsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public PostsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromForm] CreatePostCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeletePostCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<PostDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetPostsQuery request)
        {
            return Ok(await _sender.Send(request));
        }
        
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] GetPostQuery request)
        {
            return Ok(await _sender.Send(request));
        }
        
        [HttpGet("postForComment/{Id}")]
        [ProducesResponseType(typeof(PostDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByCommentIdAsync([FromRoute] GetByCommentIdQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
