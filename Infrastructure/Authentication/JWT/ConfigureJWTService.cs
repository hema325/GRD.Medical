using Infrastructure.Authentication.JWT.JWTManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure.Authentication.JWT
{
    internal static class ConfigureJWTService
    {
        public static IServiceCollection AddJWTService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                var jwtSettings = configuration.GetSection(JWTSettings.SectionName).Get<JWTSettings>();

                o.TokenValidationParameters = new()
                {
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings?.Issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.Key!)),
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            services.AddScoped<IJWTManager, JWTManager.JWTManager>();

            services.Configure<JWTSettings>(configuration.GetSection(JWTSettings.SectionName));

            return services;
        }
    }
}
