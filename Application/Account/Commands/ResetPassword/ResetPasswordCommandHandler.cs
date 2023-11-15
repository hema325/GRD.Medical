using Application.Common.Interfaces;

namespace Application.Account.Commands.ResetPassword
{
    internal class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand>
    {
        private readonly IAuthentication _auth;

        public ResetPasswordCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            await _auth.ResetPasswordAsync(request.UserId, request.Token, request.Password);
            return Unit.Value;
        }
    }
}
