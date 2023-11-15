using Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Presentation.Errors;

namespace Presentation.Filters
{
    public class GlobalExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if(context.Exception is ValidationException vldEx)
            {
                var validationResponse = new ValidationResponse
                {
                    StatusCode = vldEx.StatusCode,
                    Message = vldEx.Message,
                    Errors = vldEx.Errors
                };

                context.Result = new ObjectResult(validationResponse) { StatusCode = vldEx.StatusCode };
            }
            else if(context.Exception is ExceptionBase exBase)
            {
                var errorResponse = new ErrorResponse
                {
                    StatusCode = exBase.StatusCode,
                    Message = exBase.Message
                };

                context.Result = new ObjectResult(errorResponse) { StatusCode = exBase.StatusCode };
            }
            else
            {
                var exceptionResponse = new ExceptionResponse
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = context.Exception.InnerException?.Message ?? context.Exception.Message,
                    Details = context.Exception.StackTrace?.ToString()
                };

                context.Result = new ObjectResult(exceptionResponse) { StatusCode = exceptionResponse.StatusCode };
            }

            
        }
    }
}
