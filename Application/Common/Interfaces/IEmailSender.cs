using Application.Common.Models.EmailTemplates;
using Infrastructure.Email.EmailTemplateModels;

namespace Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailConfirmationAsync(string to, EmailConfirmationTemplate emailConfirmation);
        Task SendEmailResetPasswordAsync(string to, EmailResetPasswordTemplate emailResetPassword);
    }
}
