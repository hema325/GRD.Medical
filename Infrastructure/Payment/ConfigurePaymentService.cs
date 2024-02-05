using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Payment
{
    internal static class ConfigurePaymentService
    {
        public static IServiceCollection AddPaymentService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPayment, StripeService>();

            services.Configure<StripeSettings>(configuration.GetSection(StripeSettings.SectionName));

            return services;
        }
    }
}
