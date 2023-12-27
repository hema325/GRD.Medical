using Application.Account.Commands;
using Application.Account.Commands.Authenticate;
using Application.Account.Commands.ChangePassword;
using Application.Account.Commands.ConfirmEmail;
using Application.Account.Commands.Register;
using Application.Account.Commands.RemoveAccountImage;
using Application.Account.Commands.RequestJWTToken;
using Application.Account.Commands.ResetPassword;
using Application.Account.Commands.RevokeRefreshToken;
using Application.Account.Commands.UpdateUser;
using Application.Account.Commands.UploadAccountImage;
using Application.Account.Queries;
using Application.Account.Queries.CheckEmailDuplication;
using Application.Account.Queries.GetCurrentUser;
using Application.Account.Queries.GetUser;
using Application.Account.Queries.SendEmailConfirmation;
using Application.Account.Queries.SendEmailResetPassword;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/account")]
    public class AccountController : ApiControllerBase
    {
        private readonly ISender _sender;

        public AccountController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RegisterAsync(RegisterCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> AuthenticateAsync(AuthenticateCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpPost("requestJwt")]
        [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> RequestJWTAsync(RequestJWTTokenCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpPost("revokeRefreshToken")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RevokeRefreshTokenAsync(RevokeRefreshTokenCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpPost("sendEmailConfirmation")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SendEmailConfirmationAsync(SendEmailConfirmationCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpPost("confirmEmail")]
        [ProducesResponseType(typeof(AuthResultDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmEmailAsync(ConfirmEmailCommand request)
        {
            return Ok(await _sender.Send(request));
        }
        
        [HttpPost("sendEmailResetPassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> SendEmailResetPasswordAsync(SendEmailResetPasswordCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpPost("resetPassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ResetPasswordAsync(ResetPasswordCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpPost("changePassword")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpGet("isEmailDuplicated")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> IsEmailExistsAsync([FromQuery] CheckEmailDuplicationCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDto),StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> CurrentUserAsync()
        {
            return Ok(await _sender.Send(new GetCurrentUserQuery()));
        }
        
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(UserDto),StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> CurrentUserAsync([FromRoute] GetUserQuery request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpPost("uploadImage")]
        [ProducesResponseType(typeof(MediaDto), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> UploadImage([FromForm] UploadAccountImageCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpPost("removeImage")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<IActionResult> RemoveImage()
        {
            await _sender.Send(new RemoveAccountImageCommand());
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAsync(UpdateUserCommand request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
