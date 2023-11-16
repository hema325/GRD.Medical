namespace Presentation.Errors
{
    public class ExceptionResponse: ErrorResponse
    {
        public string? Details { get; }

        public ExceptionResponse(int statusCode, string message, string? details) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
