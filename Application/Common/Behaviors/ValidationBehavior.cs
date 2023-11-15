using FluentValidation;

namespace Application.Common.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                var validationsResult = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request)));
                var errors = validationsResult.SelectMany(v => v.Errors).Select(e=>e.ErrorMessage);

                if (errors.Any())
                    throw new Exceptions.ValidationException(errors);
            }

            return await next();
        }
    }
}
