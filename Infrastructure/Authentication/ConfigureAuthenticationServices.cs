using Infrastructure.Authentication.JWT;
using Infrastructure.Authentication.PasswordHasher;
using Infrastructure.Authentication.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Authentication
{
    internal static class ConfigureAuthenticationServices
    {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDataProtection();
            services.AddJWTService(configuration);

            services.AddScoped<ICurrentUser, CurrentUserService>();
            services.AddScoped<IAuthentication, AuthenticationService>();
            services.AddScoped<IPasswordHasher, PasswordHasherService>();
            services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            services.AddScoped<IClientSettings>(sp => sp.GetRequiredService <IOptions<ClientSettings>>().Value);

            services.Configure<RefreshTokenSettings>(configuration.GetSection(RefreshTokenSettings.SectionName));
            services.Configure<ClientSettings>(configuration.GetSection(ClientSettings.SectionName));

            return services;
        }

        public static IApplicationBuilder UseAuthenticationService(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();

            return app;
        }
    }
}
