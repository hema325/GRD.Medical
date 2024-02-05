using System.Net;

namespace Application.Common.Exceptions
{
    public class PaymentRequiredException: ExceptionBase
    {
        public override int StatusCode => (int)HttpStatusCode.PaymentRequired;

        public PaymentRequiredException(string message = "Payment is Required."): base(message, ErrorCodes.PaymentRequired) { }
    }
}
