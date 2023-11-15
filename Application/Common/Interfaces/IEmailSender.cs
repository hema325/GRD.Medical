namespace Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(string to, string subject, string body);
    }
}
