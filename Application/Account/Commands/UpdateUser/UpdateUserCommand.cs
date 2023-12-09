namespace Application.Account.Commands.UpdateUser
{
    public class UpdateUserCommand: IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
