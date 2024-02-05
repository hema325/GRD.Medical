using Application.Appointments.Commands.CreateAppointment;
using Application.Appointments.Commands.MarkAppointmentCompleted;
using Application.Appointments.Queries;
using Application.Appointments.Queries.GetAppointment;
using Application.Appointments.Queries.GetAppointments;
using Application.Common.Models;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/appointments")]
    [Authorize]
    public class AppointmentsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public AppointmentsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HaveRoles(Roles.Patient)]
        public async Task<IActionResult> CreateAsync(CreateAppointmentCommand request)
        {
            return Ok(await _sender.Send(request));
        }
        
        [HttpPost("markCompleted")]
        [ProducesResponseType(typeof(int), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> MarkAsCompletedAsync(MarkAppointmentCompletedCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<AppointmentDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetAppointmentsQuery request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] GetAppointmentQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
