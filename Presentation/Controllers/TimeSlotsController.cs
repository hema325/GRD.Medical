using Application.Common.Models;
using Application.TimeSlots.Commands.CreateTimeSlot;
using Application.TimeSlots.Commands.DeleteTimeSlot;
using Application.TimeSlots.Queries;
using Application.TimeSlots.Queries.GetTimeSlots;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/timeSlots")]
    [HaveRoles(Roles.Doctor)]
    public class TimeSlotsController : ApiControllerBase
    {
        private readonly ISender _sender;

        public TimeSlotsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TimeSlotDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync(CreateTimeSlotCommand request)
        {
            return Ok(await _sender.Send(request));
        } 
        
        [HttpDelete("{Id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute]DeleteTimeSlotCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<TimeSlotDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetTimeSlotsQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
