using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Urbia.Services
{
    public class InsightsProcessor : IInsightsProcessor
    {
        private readonly IChatService _chatService;

        public InsightsProcessor(IChatService chatService) 
        { 
            _chatService = chatService;
        }

        /// <summary>
        /// Returns 2 strings: input data, output generated insights text
        /// </summary>
        /// <param name="insightType"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public async Task<(string, string)> ProcessInsightAsync(string insightType, string filename)
        {
            var data = ReadFile(filename);
            var systemMessage = $"You are providing {insightType} insight suggestion for this data.";
            var userMessage = $"In 3 sentences, provide {insightType} insight suggestions for this data, taken from file {filename}: {data}";
            var output = await _chatService.ChatAsync(systemMessage, userMessage);
            return (data, output);
        }

        private string ReadFile(string filename)
        {
            var line = string.Empty;
            var filePath = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}/Data", filename);
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    line = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return line;
        }
    }
}
