namespace Presentation.Errors
{
    public class ValidationResponse: ErrorResponse
    {
        public IEnumerable<string> Errors { get; }

        public ValidationResponse(int statusCode, string message, IEnumerable<string> errors) : base(statusCode,message)
        {
            Errors = errors;
        }
    }
}
