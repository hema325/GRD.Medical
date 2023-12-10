using Application.MedicalAdvices.Commands.CretaeMedicalAdvice;
using Application.MedicalAdvices.Commands.DeleteMedicalAdvice;
using Application.MedicalAdvices.Commands.UpdateMedicalAdvice;
using Application.MedicalAdvices.Queries.GetMedicalAdviceByID;
using Application.MedicalAdvices.Queries.GetMedicalAdvices;
using Application.MedicalAdvices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Domain.Enums;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/advices")]
    public class AdvicesController : ApiControllerBase
    {
        private readonly IMediator _sender;

        public AdvicesController(IMediator mediator)
        {
            this._sender = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> CreateAsync([FromForm] CreateAdviceCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteAdviceCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HaveRoles(Roles.Admin)]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateAdviceCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(AdviceDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync([FromRoute]GetAdviceByIdQuary request)
        {
            return Ok(await _sender.Send(request));
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AdviceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetAdvicesQuary request)
        {
            return Ok(await _sender.Send(request));

        }
    }
}
