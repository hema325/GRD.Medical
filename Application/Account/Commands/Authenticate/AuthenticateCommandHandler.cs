using Application.Common.Interfaces;

namespace Application.Account.Commands.Authenticate
{
    internal class AuthenticateCommandHandler : IRequestHandler<AuthenticateCommand, AuthResultDto>
    {
        private readonly IAuthentication _auth;

        public AuthenticateCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<AuthResultDto> Handle(AuthenticateCommand request, CancellationToken cancellationToken)
        {
            return await _auth.AuthenticateAsync(request.Email, request.Password);
        }
    }
}
