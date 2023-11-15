namespace Application.Account.Commands.RevokeRefreshToken
{
    public class RevokeRefreshTokenCommand : IRequest
    {
        public string RefreshToken { get; set; }
    }
}
