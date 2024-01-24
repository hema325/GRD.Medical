using Application.Users.Queries.GetUser;
using Application.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.Users.Queries.GetDoctors;

namespace Presentation.Controllers
{
    [Route("api/users")]
    public class UsersController : ApiControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync([FromRoute] GetUserQuery request)
        {
            return Ok(await _sender.Send(request));
        }
        
        [HttpGet("doctors")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDoctorsAsync([FromQuery] GetDoctorsQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
