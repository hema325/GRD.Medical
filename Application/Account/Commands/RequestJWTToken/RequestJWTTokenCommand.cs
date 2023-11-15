namespace Application.Account.Commands.RequestJWTToken
{
    public class RequestJWTTokenCommand : IRequest<AuthResultDto>
    {
        public string RefreshToken { get; set; }
    }
}
