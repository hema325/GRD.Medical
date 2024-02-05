using Application.AppointmentMessages.Commands.CreateAppointmentMessage;
using Application.AppointmentMessages.Commands.Queries;
using Application.AppointmentMessages.Commands.Queries.GetAppointmentMessages;
using Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/appointmentMessages")]
    [Authorize]
    public class AppointmentMessagesController : ApiControllerBase
    {
        private readonly ISender _sender;

        public AppointmentMessagesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AppointmentMessageDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateMessageAsync([FromForm] CreateAppointmentMessageCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<AppointmentMessageDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetAppointmentMessagesQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
