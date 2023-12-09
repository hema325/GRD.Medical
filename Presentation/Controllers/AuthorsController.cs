using Application.Authors.Commands.CreateAuthor;
using Application.Authors.Commands.DeleteAuthor;
using Application.Authors.Commands.UpdateAuthor;
using Application.Authors.Queries;
using Application.Authors.Queries.GetAuthorById;
using Application.Authors.Queries.GetAuthors;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/authors")]
    public class AuthorsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public AuthorsController(ISender mediator)
        {
            _sender = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync(CreateAuthorCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> UpdateAsync(UpdateAuthorCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteAuthorCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] GetAuthorByIdQuery request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<AuthorDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetAuthorsQuery request)
        {
            return Ok(await _sender.Send(request));
        }

    }
}
