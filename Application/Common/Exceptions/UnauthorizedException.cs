using System.Net;

namespace Application.Common.Exceptions
{
    public class UnauthorizedException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public UnauthorizedException(string? message = null, ErrorCodes errorCode = ErrorCodes.Conflict) : base(message ?? "You are not authorized", errorCode)
        {
            
        }
    }
}
