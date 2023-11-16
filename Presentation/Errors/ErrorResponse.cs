namespace Presentation.Errors
{
    public class ErrorResponse
    {
        public int StatusCode { get; }
        public string? Message { get; }

        public ErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultStatusMessage(statusCode);
        }

        private string? GetDefaultStatusMessage(int statusCode)
            => statusCode switch
            {
                400 => "One or more validations have occurred",
                401 => "You are not authorized",
                404 => "Resource wasn't found",
                500 => "Error occurred while processing your request",
                _ => null
            };
    }
}
