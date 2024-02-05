using Application.Common.Models;
using Application.TimeSlots.Commands.CreateTimeSlot;
using Application.TimeSlots.Commands.DeleteTimeSlot;
using Application.TimeSlots.Queries;
using Application.TimeSlots.Queries.GetAvailableTimeSlots;
using Application.TimeSlots.Queries.GetTimeSlots;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/timeSlots")]
    public class TimeSlotsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public TimeSlotsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TimeSlotDto), StatusCodes.Status200OK)]
        [HaveRoles(Roles.Doctor)]
        public async Task<IActionResult> CreateAsync(CreateTimeSlotCommand request)
        {
            return Ok(await _sender.Send(request));
        } 
        
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HaveRoles(Roles.Doctor)]
        public async Task<IActionResult> DeleteAsync([FromRoute]DeleteTimeSlotCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<TimeSlotDto>), StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetAsync([FromQuery] GetTimeSlotsQuery request)
        {
            return Ok(await _sender.Send(request));
        }
        
        [HttpGet("availableTimeSlots")]
        [ProducesResponseType(typeof(IEnumerable<TimeSlotDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableTimeSlotsAsync([FromQuery] GetAvailableTimeSlotsQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
