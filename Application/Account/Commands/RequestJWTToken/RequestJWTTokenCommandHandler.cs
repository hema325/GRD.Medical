using Application.Common.Interfaces;

namespace Application.Account.Commands.RequestJWTToken
{
    internal class RequestJWTTokenCommandHandler : IRequestHandler<RequestJWTTokenCommand, AuthResultDto>
    {
        private readonly IAuthentication _auth;

        public RequestJWTTokenCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<AuthResultDto> Handle(RequestJWTTokenCommand request, CancellationToken cancellationToken)
        {
            return await _auth.RequestJwtTokenAsync(request.RefreshToken);
        }
    }
}
