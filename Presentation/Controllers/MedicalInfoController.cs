
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.MedicalInformation.Queries.GetMedicalInfo;
using Microsoft.AspNetCore.Authorization;
using Application.MedicalInformation.Commands.UpdateMedicalInfo;
using Application.MedicalInformation.Queries;

namespace Presentation.Controllers
{
    [Route("api/medicalInfo")]
    public class MedicalInfoController : ApiControllerBase
    {
        private readonly ISender _sender;
        public MedicalInfoController(ISender sender)
        {
            _sender = sender;
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateMedicalInfoCommand request)
        {
            return Ok(await _sender.Send(request));
        }


        [HttpGet]
        [ProducesResponseType(typeof(MedicalInfoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _sender.Send(new GetMedicalInfoQuery()));
        }

    }
}