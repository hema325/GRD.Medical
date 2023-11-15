namespace Presentation.Errors
{
    public class ValidationResponse: ErrorResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
