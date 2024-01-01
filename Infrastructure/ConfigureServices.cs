using Infrastructure.Authentication;
using Infrastructure.ChatBot;
using Infrastructure.Email;
using Infrastructure.FileStorage;
using Infrastructure.Persistance;
using Infrastructure.Services;
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
            services.AddChatBotService(configuration);
            services.AddDistributedMemoryCache();
            services.AddFileStorageService();
            services.AddCommonServces();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseFileStorageService();
            app.UseAuthenticationService();

            return app;
        }

        public static async Task InitialiseDBAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>().InitialiseAsync();
        }
    }
}