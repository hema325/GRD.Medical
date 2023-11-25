using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/authros")]
    public class AuthrosController : ApiControllerBase
    {
        private readonly ISender _mediator;

        public AuthrosController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromForm] CreateAuthorCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateAuthorCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteAuthorCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
