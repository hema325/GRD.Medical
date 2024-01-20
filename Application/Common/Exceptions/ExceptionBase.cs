
namespace Application.Common.Exceptions
{
    public abstract class ExceptionBase: Exception
    {
        public  ErrorCodes ErrorCode { get; }
        public abstract int StatusCode { get; }

        public ExceptionBase(string? message = null, ErrorCodes errorCode = ErrorCodes.UnSpecified): base(message)
        {
            ErrorCode = errorCode;  
        }

    }
}
