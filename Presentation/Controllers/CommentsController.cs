using Application.Comments.Commands.CreateComment;
using Application.Comments.Commands.DeleteComment;
using Application.Comments.Queries;
using Application.Comments.Queries.GetComments;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/comments")]
    [Authorize]
    public class CommentsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public CommentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CommentDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromForm] CreateCommentCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteCommentCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<CommentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetCommentsQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
