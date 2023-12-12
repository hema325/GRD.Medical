using System.Net;

namespace Application.Common.Exceptions
{
    public class ConflictException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.Conflict;

        public ConflictException(string? message = null): base(message) { }
    }
}
