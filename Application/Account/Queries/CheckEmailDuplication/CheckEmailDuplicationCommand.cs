namespace Application.Account.Queries.CheckEmailDuplication
{
    public class CheckEmailDuplicationCommand: IRequest<bool>
    {
        public string Email { get; set; }
    }
}
