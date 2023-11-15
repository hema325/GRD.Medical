using Infrastructure.Email.TemplateParser;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Email
{
    internal static class ConfigureEmailService
    {
        public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IEmailSender, EmailSenderService>();
            services.AddScoped<ITemplateParser, TemplateParserService>();

            services.Configure<EmailSettings>(configuration.GetSection(EmailSettings.SectionName));

            return services;
        }
    }
}
