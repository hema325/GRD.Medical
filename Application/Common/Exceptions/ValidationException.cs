using System.Net;

namespace Application.Common.Exceptions
{
    public class ValidationException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.BadRequest;

        public IEnumerable<string> Errors { get; set; }

        public ValidationException(IEnumerable<string> errors): base("One or more validations have occurred", ErrorCodes.InvalidData)
        {
            Errors = errors;
        }

    }
}
