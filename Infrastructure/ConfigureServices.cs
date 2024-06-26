﻿using Infrastructure.Authentication;
using Infrastructure.ChatBot;
using Infrastructure.Email;
using Infrastructure.FileStorage;
using Infrastructure.Notifications;
using Infrastructure.Payment;
using Infrastructure.Persistance;
using Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.AppointmentChat;
using Infrastructure.Cors;
using Infrastructure.BackgroundJobs;

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
            services.AddPaymentService(configuration);
            services.AddCorsService(configuration);
            services.AddBackgroundJobs(configuration);
            services.AddDistributedMemoryCache();
            services.AddFileStorageService();
            services.AddCommonServces();
            services.AddNotificationsService();
            services.AddSignalR();
            services.AddAppointmentChatService();

            return services;
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseCorsService();
            app.UseFileStorageService();
            app.UseAuthenticationService();

            return app;
        }

        public static async Task InitialiseDBAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>().InitialiseAsync();
        }

        public static IEndpointRouteBuilder MapInfrastructure(this IEndpointRouteBuilder endpoint)
        {
            endpoint.MapNotificationsService();
            endpoint.MapAppointmentChatService();

            return endpoint;
        }
    }
}