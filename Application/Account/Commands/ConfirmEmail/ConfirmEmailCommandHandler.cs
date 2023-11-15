using Application.Common.Interfaces;

namespace Application.Account.Commands.ConfirmEmail
{
    internal class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, AuthResultDto>
    {
        private readonly IAuthentication _auth;

        public ConfirmEmailCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<AuthResultDto> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            return await _auth.ConfirmEmailAsync(request.UserId, request.Token);
        }
    }
}
