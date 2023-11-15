using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Errors;

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
            throw new UnauthorizedException("You are not authorized");
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
            throw new Exception("this is an internal server error");
        }

    }
}
