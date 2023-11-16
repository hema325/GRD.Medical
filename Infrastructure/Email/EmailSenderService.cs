using Application.Common.Models.EmailTemplates;
using Infrastructure.Email.EmailTemplateModels;
using Infrastructure.Email.TemplateParser;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Email
{
    internal class EmailSenderService: IEmailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly ITemplateParser _templateParser;

        public EmailSenderService(IOptions<EmailSettings> emailSettings, ITemplateParser templateParser)
        {
            _emailSettings = emailSettings.Value;
            _templateParser = templateParser;
        }

        private async Task SendAsync(string to, string subject, string body)
        {
            //create email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.UserName));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = body
            };

            //send email message
            var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(_emailSettings.Host,
                _emailSettings.Port,
                SecureSocketOptions.StartTls);
            await smtpClient.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
            await smtpClient.SendAsync(message);
            //await smtpClient.DisconnectAsync(true);
        }

        public async Task SendEmailConfirmationAsync(string to, EmailConfirmationTemplate emailConfirmation)
        {
            var template = await _templateParser.ParseAsync("EmailConfirmationTemplate", emailConfirmation);
            await SendAsync(to, "Confirming Email", template);
        }

        public async Task SendEmailResetPasswordAsync(string to, EmailResetPasswordTemplate emailResetPassword)
        {
            var template = await _templateParser.ParseAsync("EmailResetPasswordTemplate", emailResetPassword);
            await SendAsync(to, "Resating Password", template);
        }

    }
}
