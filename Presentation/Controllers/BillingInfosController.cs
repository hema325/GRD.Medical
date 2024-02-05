using Application.BillingInfos.Command;
using Application.BillingInfos.Command.CreatePaymentIntent;
using Application.BillingInfos.Queries;
using Application.BillingInfos.Queries.GetBillingInfos;
using Application.Common.Models;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;

namespace Presentation.Controllers
{
    [Route("api/billings")]
    [Authorize]
    public class BillingInfosController : ApiControllerBase
    {
        private readonly ISender _sender;

        public BillingInfosController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("createPaymentIntentId")]
        [ProducesResponseType(typeof(PaymentIntentDto), StatusCodes.Status200OK)]
        [HaveRoles(Roles.Patient)]
        public async Task<IActionResult> GetPaymentIntentIdAsync(CreatePaymentIntentCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<BillingInfoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetBillingInfosQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
