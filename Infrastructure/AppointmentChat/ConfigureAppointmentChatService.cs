using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.AppointmentChat
{
    internal static class ConfigureAppointmentChatService
    {
        public static IServiceCollection AddAppointmentChatService(this IServiceCollection services)
        {
            services.AddScoped <IAppointmentChat, AppointmentChatService>();

            return services;
        }

        public static IEndpointRouteBuilder MapAppointmentChatService(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapHub<AppointmentChatHub>("/api/hubs/appointmentChat", options =>
            {
                options.CloseOnAuthenticationExpiration = true;
            });

            return endpoint;
        }
    }
}
