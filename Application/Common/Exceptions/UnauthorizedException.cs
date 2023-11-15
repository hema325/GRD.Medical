using System.Net;

namespace Application.Common.Exceptions
{
    public class UnauthorizedException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.Unauthorized;

        public UnauthorizedException(string message = "You are not authorized"): base(message)
        {
            
        }
    }
}
