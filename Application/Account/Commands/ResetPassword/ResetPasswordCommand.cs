namespace Application.Account.Commands.ResetPassword
{
    public class ResetPasswordCommand : IRequest
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
