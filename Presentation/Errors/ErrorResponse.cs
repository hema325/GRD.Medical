namespace Presentation.Errors
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public ErrorResponse()
        {
            
        }

        public ErrorResponse(int statusCode)
        {
            StatusCode = statusCode;
            Message = statusCode switch
            {
                401 => "You are not authorized",
                404 => "Resource wasn't found",
                500 => "Error occurred while processing your request",
                _ => null
            };
        }
    }
}
