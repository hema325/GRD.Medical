namespace Application.Account.Queries.SendEmailResetPassword
{
    public class SendEmailResetPasswordCommand : IRequest
    {
        public string Email { get; set; }
    }
}
