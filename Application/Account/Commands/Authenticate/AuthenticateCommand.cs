namespace Application.Account.Commands.Authenticate
{
    public class AuthenticateCommand : IRequest<AuthResultDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
