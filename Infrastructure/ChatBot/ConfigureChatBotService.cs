using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ChatBot
{
    internal static class ConfigureChatBotService
    {
        public static IServiceCollection AddChatBotService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IChatBot, ChatBotService>(c =>
            {
                var settings = configuration.GetSection(ChatBotSettings.SectionName)
                .Get<ChatBotSettings>();

                c.BaseAddress = new Uri(settings!.BaseUrl);
            });

            services.Configure<ChatBotSettings>(configuration.GetSection(ChatBotSettings.SectionName));

            return services;
        }
    }
}
