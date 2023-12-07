using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Services
{
    internal static class ConfigureCommonServices
    {
        public static IServiceCollection AddCommonServces(this IServiceCollection services)
        {
            services.AddScoped<ICurrentHttpRequest, CurrentHttpRequest>();

            return services;
        }
    }
}
