namespace Application.Account.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest<AuthResultDto>
    {
        public int UserId { get; set; }
        public string Token { get; set; }
    }
}
