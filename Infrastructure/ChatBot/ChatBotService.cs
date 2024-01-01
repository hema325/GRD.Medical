using Application.Common.Models;
using Infrastructure.ChatBot.Models;
using System.Text;
using System.Text.Json;

namespace Infrastructure.ChatBot
{
    internal class ChatBotService : IChatBot
    {
        private readonly HttpClient _httpClient;

        public ChatBotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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
                //noting to do
            }

            return ChatBotResult.Failure();
        }
    }
}
