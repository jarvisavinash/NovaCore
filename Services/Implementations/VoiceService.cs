using System.Text;
using System.Text.Json;
using NovaCore.Models.Request;
using NovaCore.Services.Interfaces;

namespace NovaCore.Services
{
    public class VoiceService : IVoiceService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public VoiceService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> ProcessText(VoiceRequest request)
        {
            var apiKey = _configuration["Groq:ApiKey"];

            var payload = new
            {
                model = "llama-3.1-8b-instant",
                messages = new[]
                {
                    new { role = "user", content = request.Text }
                }
            };

            var json = JsonSerializer.Serialize(payload);

            var httpRequest = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.groq.com/openai/v1/chat/completions"
            );

            httpRequest.Headers.Add("Authorization", $"Bearer {apiKey}");

            httpRequest.Content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.SendAsync(httpRequest);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"NOVACORE: Error calling Groq API: {error}");
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            var result = JsonDocument.Parse(responseContent);

            var message = result
                .RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            return message;
        }
    }
}