using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace Infrastructure.FileStorage
{
    internal static class ConfigureFileStorageService
    {
        public static IServiceCollection AddFileStorageService(this IServiceCollection services)
        {
            services.AddScoped<IFileStorage, LocalFileStorageService>();

            return services;
        }

        public static IApplicationBuilder UseFileStorageService(this IApplicationBuilder app)
        {
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"Files")),
                RequestPath = "/Files"
            });

            return app;
        }
    }
}
