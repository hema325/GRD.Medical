using Application.Account.Commands;
using Application.Account.Commands.Authenticate;
using Application.Account.Commands.ChangePassword;
using Application.Account.Commands.ConfirmEmail;
using Application.Account.Commands.Register;
using Application.Account.Commands.RegisterDoctor;
using Application.Account.Commands.RemoveAccountImage;
using Application.Account.Commands.RequestJWTToken;
using Application.Account.Commands.ResetPassword;
using Application.Account.Commands.RevokeRefreshToken;
using Application.Account.Commands.UpdateDoctor;
using Application.Account.Commands.UpdateUser;
using Application.Account.Commands.UploadAccountImage;
using Application.Account.Queries.CheckEmailDuplication;
using Application.Account.Queries.GetCurrentUser;
using Application.Account.Queries.SendEmailConfirmation;
using Application.Account.Queries.SendEmailResetPassword;
using Application.Users.Queries;
using Application.Users.Queries.GetUser;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

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

        [HttpPost("registerUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RegisterUserAsync(RegisterUserCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpPost("registerDoctor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RegisterDoctorAsync(RegisterDoctorCommand request)
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

        [HttpPost("isEmailDuplicated")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> IsEmailExistsAsync(CheckEmailDuplicationCommand request)
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

        [HttpPost("uploadImage")]
        [ProducesResponseType(typeof(UploadAccountImageCommandResponse), StatusCodes.Status200OK)]
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

        [HttpPut("user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HaveRoles(Roles.Patient)]
        public async Task<IActionResult> UpdateCurrentUserAsync(UpdateUserCommand request)
        {
            return Ok(await _sender.Send(request));
        }
        
        [HttpPut("doctor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HaveRoles(Roles.Doctor)]
        public async Task<IActionResult> UpdateCurrentDoctorAsync(UpdateDoctorCommand request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
