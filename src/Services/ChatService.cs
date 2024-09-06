using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Urbia.Services
{
    public class ChatService : IChatService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IChatService> _logger;

        public ChatService(IConfiguration configuration,
            ILogger<IChatService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<string> ChatAsync(string systemMessage, string userMessage)
        {
            var uri = new Uri(_configuration["AzureOpenAiEndpoint"]);
            var credential = new AzureKeyCredential(_configuration["AzureOpenAiKey"]);
            var deployment = _configuration["AzureOpenAiDeployment"];

            var client = new OpenAIClient(uri, credential);

            ChatCompletionsOptions options = new ChatCompletionsOptions()
            {
                Messages = { 
                    new ChatMessage(ChatRole.System, systemMessage),
                    new ChatMessage(ChatRole.User, userMessage)
                },
                Temperature = (float)0.7,
                MaxTokens = 800,
                NucleusSamplingFactor = (float)0.95,
                FrequencyPenalty = 0,
                PresencePenalty = 0,
            };

            Response<ChatCompletions> response =
                await client.GetChatCompletionsAsync(
                    deploymentOrModelName: deployment,
                    options);

            var completions = response.Value;
            return completions.Choices[0].Message.Content;
        }
    }
}
