﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Notifications
{
    internal static class ConfigureNotificationsService
    {
        public static IServiceCollection AddNotificationsService(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddScoped<INotificationSender, NotificationSenderService>();

            return services;
        }

        public static IEndpointRouteBuilder MapNotificationsService(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapHub<NotificationHub>("/hubs/notifications", options=>
            {
                options.CloseOnAuthenticationExpiration = true;
            });

            return endpoint;
        }
    }
}
