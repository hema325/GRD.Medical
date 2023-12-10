using Infrastructure.Persistance.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistance
{
    internal static class ConfigurePersistanceServices
    {
        public static IServiceCollection AddPersistanceService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("Default")));
            
            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddScoped<ModelSeeder>();

            return services;
        }
    }
}
