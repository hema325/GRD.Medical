using Application.Common.Models;
using Application.Languages.Queries;
using Application.Languages.Queries.GetLanguages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/languages")]
    public class LanguagesController : ApiControllerBase
    {
        private readonly ISender _sender;

        public LanguagesController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        [ProducesResponseType(typeof(PaginatedList<LanguageDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetLanguagesQuery request)
        {
            return Ok(await _sender.Send(request));
        }
    }
}
