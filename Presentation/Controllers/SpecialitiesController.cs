using Application.Common.Models;
using Application.Specialists.Queries;
using Application.Specialists.Queries.GetSpecialists;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/specialities")]
    public class SpecialitiesController : ControllerBase
    {
        private readonly ISender _sender;

        public SpecialitiesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<SpecialityDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetSpecialitiesQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
