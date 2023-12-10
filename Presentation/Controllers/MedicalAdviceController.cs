using Application.MedicalAdvices.Commands.CretaeMedicalAdvice;
using Application.MedicalAdvices.Commands.DeleteMedicalAdvice;
using Application.MedicalAdvices.Commands.UpdateMedicalAdvice;
using Application.MedicalAdvices.Queries.GetMedicalAdviceByID;
using Application.MedicalAdvices.Queries.GetMedicalAdvices;
using Application.MedicalAdvices.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/medicaladvice")]
    public class MedicalAdviceController : ApiControllerBase
    {
        private readonly IMediator _sender;

        public MedicalAdviceController(IMediator mediator)
        {
            this._sender = mediator;
        }
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateAsync([FromForm] CreateMedicalAdviceCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteAsync([FromQuery] DeleteMedicalAdviceCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateMedicalAdviceCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(MedicalAdviceDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByIdAsync([FromRoute]GetMedicalAdviceByIdQuary request)
        {
            return Ok(await _sender.Send(request));
        }
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MedicalAdviceDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetMedicalAdvicesQuary request)
        {
            return Ok(await _sender.Send(request));

        }
    }
}
