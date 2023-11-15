namespace Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        int? Id { get; }
        string? Email { get; }
    }
}
