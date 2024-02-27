
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.MedicalInformation.Queries.GetMedicalInfo;
using Microsoft.AspNetCore.Authorization;
using Application.MedicalInfos.Commands.UpdateMedicalInfo;
using Application.MedicalInformation.Queries;
using Domain.Enums;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/medicalInfos")]
    [HaveRoles(Roles.Patient)]
    public class MedicalInfosController : ApiControllerBase
    {
        private readonly ISender _sender;
        public MedicalInfosController(ISender sender)
        {
            _sender = sender;
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateMedicalInfoCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }


        [HttpGet]
        [ProducesResponseType(typeof(MedicalInfoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _sender.Send(new GetMedicalInfoQuery()));
        }

    }
}