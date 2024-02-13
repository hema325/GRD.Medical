using Application.Common.Models.EmailTemplates;
using Infrastructure.Email.EmailTemplateModels;
using Infrastructure.Email.TemplateParser;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Infrastructure.Email
{
    internal class EmailSenderService : IEmailSender
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
            using var smtpClient = new SmtpClient();
            smtpClient.ServerCertificateValidationCallback = CertificateCallback;
            await smtpClient.ConnectAsync(_emailSettings.Host,
                _emailSettings.Port,
                SecureSocketOptions.SslOnConnect);
            await smtpClient.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
            await smtpClient.SendAsync(message);
        }

        private bool CertificateCallback(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            if ((sslPolicyErrors & SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null && chain.ChainStatus != null)
                {
                    foreach (var status in chain.ChainStatus)
                    {
                        if ((certificate.Subject == certificate.Issuer) && (status.Status == X509ChainStatusFlags.UntrustedRoot))
                            continue;
                        else if (status.Status != X509ChainStatusFlags.NoError)
                            return false;
                    }
                }

                return true;
            }

            return false;
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
        
        public async Task SendEmailAppointmentSheduledAsync(string to, EmailAppointmentScheduledTemplate emailResetPassword)
        {
            var template = await _templateParser.ParseAsync("EmailAppointmentScheduledTemplate", emailResetPassword);
            await SendAsync(to, "Appointment Scheduling", template);
        }


    }
}
