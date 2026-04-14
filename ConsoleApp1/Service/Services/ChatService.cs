using Service.Services.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Service.Services
{
    public class ChatService : IChatService
    {
        private readonly HttpClient _httpClient;

        public ChatService()
        {
            _httpClient = new HttpClient();

            var apiKey = Environment.GetEnvironmentVariable("GROQ_API_KEY");

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", apiKey);
        }

        public async Task<string> AskAsync(string message)
        {
            var requestBody = new
            {
                model = "llama-3.1-8b-instant",
                messages = new[]
                {
                    new
                    {
                        role = "system",
                        content = "Sən Azərbaycan dilində cavab verən ağıllı asistentsən."
                    },
                    new
                    {
                        role = "user",
                        content = message
                    }
                },
                temperature = 0.7
            };

            var json = JsonSerializer.Serialize(requestBody);

            var response = await _httpClient.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions",
                new StringContent(json, Encoding.UTF8, "application/json"));

            var result = await response.Content.ReadAsStringAsync();

           
            using var doc = JsonDocument.Parse(result);
            var root = doc.RootElement;

            if (root.TryGetProperty("error", out var error))
            {
                return $" Groq Error: {error.GetProperty("message").GetString()}";
            }

           
            if (!root.TryGetProperty("choices", out var choices))
            {
                return " Invalid response from Groq API";
            }

            if (choices.GetArrayLength() == 0)
            {
                return " Empty response";
            }

            var content = choices[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return content ?? " No content returned";
        }
    }
}