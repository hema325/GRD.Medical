using System.Net;

namespace Application.Common.Exceptions
{
    public class ForbiddenException : ExceptionBase
    {
        public override int StatusCode => (int) HttpStatusCode.Forbidden;

        public ForbiddenException(string? message = "You can not perform this operation.") : base(message, ErrorCodes.AccessDenied) { }
    }
}
