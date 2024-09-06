using Azure;
using Azure.AI.Inference;
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
            var endpoint = new Uri(_configuration["AzureAiChatEndpoint"]);
            var credential = new AzureKeyCredential(_configuration["AzureAiChatKey"]);

            var client = new ChatCompletionsClient(endpoint, credential, new ChatCompletionsClientOptions());

            var requestOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                    new ChatRequestSystemMessage(systemMessage),
                    new ChatRequestUserMessage(userMessage),
                },
            };

            try
            {
                Response<ChatCompletions> response = client.Complete(requestOptions);
                return response.Value.Choices[0].Message.Content;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return string.Empty;
            }
        }
    }
}
