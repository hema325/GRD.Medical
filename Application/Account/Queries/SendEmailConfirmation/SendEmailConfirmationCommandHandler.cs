using Application.Common.Interfaces;

namespace Application.Account.Queries.SendEmailConfirmation
{
    internal class SendEmailConfirmationCommandHandler : IRequestHandler<SendEmailConfirmationCommand>
    {
        private readonly IAuthentication _auth;

        public SendEmailConfirmationCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<Unit> Handle(SendEmailConfirmationCommand request, CancellationToken cancellationToken)
        {
            await _auth.SendEmailConfirmationAsync(request.Email);
            return Unit.Value;
        }
    }
}
