using Application.Common.Interfaces;

namespace Application.Account.Commands.ChangePassword
{
    internal class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
    {
        private readonly IAuthentication _auth;

        public ChangePasswordCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            await _auth.ChangePasswordAsync(request.OldPassword, request.NewPassword);
            return Unit.Value;
        }
    }
}
