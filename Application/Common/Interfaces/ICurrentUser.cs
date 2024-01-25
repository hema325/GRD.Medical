namespace Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        int? Id { get; }
        string? Email { get; }
        Roles? Role { get; }
        string? Name { get; }
        int? DoctorId { get; }
    }
}
