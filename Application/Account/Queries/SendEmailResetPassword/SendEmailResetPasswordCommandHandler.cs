using Application.Common.Interfaces;

namespace Application.Account.Queries.SendEmailResetPassword
{
    internal class SendEmailResetPasswordCommandHandler : IRequestHandler<SendEmailResetPasswordCommand>
    {
        private readonly IAuthentication _auth;

        public SendEmailResetPasswordCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<Unit> Handle(SendEmailResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _auth.SendEmailResetPasswordAsync(request.Email);
            return Unit.Value;
        }
    }
}
