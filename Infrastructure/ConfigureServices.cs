using Infrastructure.Authentication;
using Infrastructure.Email;
using Infrastructure.FileStorage;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistanceService(configuration);
            services.AddAuthenticationService(configuration);
            services.AddEmailService(configuration);
            services.AddDistributedMemoryCache();
            services.AddFileStorageService();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseFileStorageService();
            app.UseAuthenticationService();

            return app;
        }
    }
}