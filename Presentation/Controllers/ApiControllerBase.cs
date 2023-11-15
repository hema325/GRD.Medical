using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;

namespace Presentation.Controllers
{
    [ApiController]
    [GlobalExceptionFilter]
    public class ApiControllerBase : ControllerBase
    {
    }
}
