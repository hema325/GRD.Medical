namespace Application.Account.Queries.GetUser
{
    public class GetUserQuery: IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
