using System.Net;

namespace Application.Common.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.NotFound;

        public NotFoundException(string key = "Resource") : base($"{key} wasn't found", ErrorCodes.NotFound)
        {
            
        }
    }
}
