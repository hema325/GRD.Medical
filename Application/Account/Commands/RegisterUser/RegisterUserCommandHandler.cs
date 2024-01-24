using Application.Common.Interfaces;

namespace Application.Account.Commands.Register
{
    internal class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly IAuthentication _auth;

        public RegisterUserCommandHandler(IAuthentication auth)
        {
            _auth = auth;
        }

        public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                IsEmailConfirmed = false,
                JoinedOn = DateTime.UtcNow,
                Role = Roles.Patient
            };

            await _auth.RegisterAsync(user, request.Password);

            return Unit.Value;
        }
    }
}
