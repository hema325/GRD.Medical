namespace Application.Common.Interfaces
{
    public interface ICurrentHttpRequest
    {
        string? Scheme { get; }
        string? Host { get; }
    }
}
