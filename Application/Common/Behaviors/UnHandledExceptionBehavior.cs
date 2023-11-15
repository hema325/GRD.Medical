using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviors
{
    internal class UnHandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<UnHandledExceptionBehavior<TRequest, TResponse>> _logger;

        public UnHandledExceptionBehavior(ILogger<UnHandledExceptionBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch(Exception ex)
            {
                _logger.LogError("Un handled exception for request {requestName} with name {exceptionName} and message {exceptionMessage} at {dateTime}",
                    request.GetType().Name,
                    ex.GetType().Name,
                    ex.Message,
                    DateTime.UtcNow);

                throw;
            }
        }
    }
}
