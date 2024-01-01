using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Presentation.Errors;
using System.Text.Json;

namespace Presentation.Controllers
{
    [Route("api/Buggy")]
    public class BuggyController : ApiControllerBase
    {
        [HttpGet("notFound")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        public IActionResult NotFound()
        {
            throw new NotFoundException();
        }

        [HttpGet("unauthorized")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
        public IActionResult Unauthorized()
        {
            throw new UnauthorizedException();
        }

        [HttpGet("badRequest")]
        [ProducesResponseType(typeof(ValidationResponse), StatusCodes.Status400BadRequest)]
        public IActionResult BadRequest()
        {
            throw new ValidationException(new[]
            {
                "first validation error",
                "second validation error"
            });
        }

        [HttpGet("internalServerError")]
        [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult InternalServerError()
        {
            throw new Exception("Server Error");
        }

        [HttpGet("conflict")]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public IActionResult Conflict()
        {
            throw new ConflictException("There is a conflict.");
        }
    }
}
