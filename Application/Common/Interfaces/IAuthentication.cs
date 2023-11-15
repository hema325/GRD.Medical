using Application.Account.Commands;

namespace Application.Common.Interfaces
{
    public interface IAuthentication
    {
        Task<AuthResultDto> AuthenticateAsync(string email, string password);
        Task ChangePasswordAsync(string oldPassword, string newPassword);
        Task<AuthResultDto> ConfirmEmailAsync(int userId, string token);
        Task RegisterAsync(User user, string password);
        Task<AuthResultDto> RequestJwtTokenAsync(string rfToken);
        Task ResetPasswordAsync(int userId, string token, string newPassword);
        Task RevokeRefreshTokenAsync(string rfToken);
        Task SendEmailConfirmationAsync(string email);
        Task SendEmailResetPasswordAsync(string email);
    }
}
