using Domain.Enums;

namespace Presentation.Errors
{
    public class ErrorResponse
    {
        public ErrorCodes ErrorCode { get; set; }
        public int StatusCode { get; }
        public string? Message { get; }

        public ErrorResponse(int statusCode, string? message = null, ErrorCodes? errorCode = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultStatusMessage(statusCode);
            ErrorCode = errorCode ?? GetDefaultErrorCode(statusCode);
        }

        private string? GetDefaultStatusMessage(int statusCode)
            => statusCode switch
            {
                400 => "One or more validations have occurred",
                401 => "You are not authorized",
                404 => "Resource wasn't found",
                500 => "Error occurred while processing your request",
                403 => "You are not allowed to do this action",
                _ => null
            };
        
        private ErrorCodes GetDefaultErrorCode(int statusCode)
            => statusCode switch
            {
                400 => ErrorCodes.InvalidData,
                401 => ErrorCodes.UnAuthorized,
                404 => ErrorCodes.NotFound,
                500 => ErrorCodes.ServerError,
                403 => ErrorCodes.AccessDenied,
                _ => ErrorCodes.UnSpecified
            };
    }
}
