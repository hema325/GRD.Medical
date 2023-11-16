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
                var validationResponse = new ValidationResponse (
                    vldEx.StatusCode,
                    vldEx.Message,
                    vldEx.Errors
                );

                context.Result = new ObjectResult(validationResponse) { StatusCode = vldEx.StatusCode };
            }
            else if(context.Exception is ExceptionBase exBase)
            {
                var errorResponse = new ErrorResponse (
                    exBase.StatusCode,
                    exBase.Message
                );

                context.Result = new ObjectResult(errorResponse) { StatusCode = exBase.StatusCode };
            }
            else
            {
                var exceptionResponse = new ExceptionResponse (
                    StatusCodes.Status500InternalServerError,
                    context.Exception.InnerException?.Message ?? context.Exception.Message,
                    context.Exception.StackTrace?.ToString()
                );

                context.Result = new ObjectResult(exceptionResponse) { StatusCode = exceptionResponse.StatusCode };
            }      
        }
    }
}
