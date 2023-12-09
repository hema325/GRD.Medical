using Microsoft.AspNetCore.Mvc;
using Presentation.Errors;

namespace Presentation.Controllers
{
    [Route("errors/{statusCode}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : ApiControllerBase
    {
        public IActionResult Error(int statusCode)
        {
            return StatusCode(statusCode, new ErrorResponse(statusCode));
        }
    }
}
