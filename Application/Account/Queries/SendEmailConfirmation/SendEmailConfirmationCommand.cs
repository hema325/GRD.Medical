namespace Application.Account.Queries.SendEmailConfirmation
{
    public class SendEmailConfirmationCommand : IRequest
    {
        public string Email { get; set; }
    }
}
