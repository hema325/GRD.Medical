using Application.Common.Models;
using Infrastructure.ChatBot.Models;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Infrastructure.ChatBot
{
    internal class ChatBotService : IChatBot
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ChatBotService> _logger;
        public ChatBotService(HttpClient httpClient, ILogger<ChatBotService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ChatBotResult> GetResponseAsync(string message)
        {
            try { 
                var content = new StringContent(JsonSerializer.Serialize(new { message }),Encoding.UTF8, "application/json");
                var postResponse = await _httpClient.PostAsync("chatBot", content);

                if (postResponse.IsSuccessStatusCode)
                {
                    var response = JsonSerializer.Deserialize<ChatBotApiResponse>(await postResponse.Content.ReadAsStringAsync(), new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });

                    return ChatBotResult.Success(response.Response);
                }
            }
            catch
            {
                _logger.LogError("Failed to generate chatbot message");
            }

            return ChatBotResult.Failure();
        }
    }
}
