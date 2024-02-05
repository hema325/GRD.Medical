using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Cors
{
    internal static class ConfigureCorsService
    {
        public static IServiceCollection AddCorsService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(o =>
            {
                var settings = configuration.GetSection(CorsSettings.SectionName).Get<CorsSettings>();

                o.AddDefaultPolicy(b =>
                {
                    b.WithOrigins(settings.Origins);
                    b.AllowAnyMethod();
                    b.AllowAnyHeader();
                    b.AllowCredentials();
                });
            });

            services.Configure<CorsSettings>(configuration.GetSection(CorsSettings.SectionName));

            return services;
        }

        public static IApplicationBuilder UseCorsService(this IApplicationBuilder app)
        {
            app.UseCors();

            return app;
        }
    }
}
