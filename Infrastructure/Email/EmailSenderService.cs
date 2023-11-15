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

        public EmailSenderService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendAsync(string to, string subject, string body)
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

    }
}
