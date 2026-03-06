using Newtonsoft.Json;
using System.Text;
using NovaCore.Services.Interfaces;

namespace NovaCore.Services.Implementations
{
    public class ChatGptService : IChatGptService
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public ChatGptService(IConfiguration config)
        {
            _config = config;
            _httpClient = new HttpClient();
        }

        public async Task<string> GetResponse(string prompt)
        {
            var apiKey = _config["Groq:ApiKey"];

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var requestBody = new
            {
                model = "llama-3.1-8b-instant",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var json = JsonConvert.SerializeObject(requestBody);

            var response = await _httpClient.PostAsync(
                "https://api.groq.com/openai/v1/chat/completions",
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            var result = await response.Content.ReadAsStringAsync();

            dynamic data = JsonConvert.DeserializeObject(result);

            return data.choices[0].message.content.ToString();
        }
    }
}