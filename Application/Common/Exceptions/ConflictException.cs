using System.Net;

namespace Application.Common.Exceptions
{
    public class ConflictException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.Conflict;

        public ConflictException(string? message = "There is a conflict happened while processing your request.", ErrorCodes errorCode = ErrorCodes.UnSpecified): base(message, errorCode) 
        {
        }
    }
}
