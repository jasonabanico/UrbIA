using Urbia.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Urbia
{
    public class Program
    {
        private static List<(string, string)> _insightInputFiles = new List<(string, string)>()
        {
            ( "Safety Insights", "VICTORIAN_ROAD_CRASH_DATA - excerpt.geojson" ),
            ( "Capacity Forcest Insights", "Traffic_Volume - excerpt.geojson" ),
            ( "Sustainability Insights", "Principal_Bicycle_Network_(PBN) - excerpt.geojson" ),
            ( "Sustainability Insights", "train-passenger-service-counts.json" ),
            ( "Public Utilisation Insights", "Mary Cairncross Scenic Reserve Dingtek PIR Counter - excerpt.txt" )
        };

        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceCollection = new ServiceCollection();

            var serviceProvider = serviceCollection
                .AddLogging()
                .AddSingleton(config)
                .AddSingleton<IChatService, ChatService>()
                .AddSingleton<IInsightsProcessor, InsightsProcessor>()
                .BuildServiceProvider();

            Console.WriteLine("Urbia Proof-of-Concept");
            Console.WriteLine("----------------------");
            Console.WriteLine();

            var insightsProcessor = serviceProvider.GetService<IInsightsProcessor>();

            var index = 0;
            foreach (var insightInputFile in _insightInputFiles)
            {
                index++;
                var insightType = insightInputFile.Item1;
                var filename = insightInputFile.Item2;
                var displayText = string.Empty;
                displayText += $"Sample {insightType} from data {filename}." + System.Environment.NewLine;
                displayText += System.Environment.NewLine;
                (var data, var output) = insightsProcessor.ProcessInsightAsync(insightType, filename).Result;
                displayText += $"Data: \n{data}" + System.Environment.NewLine;
                displayText += System.Environment.NewLine;
                displayText += $"{insightType} Suggestions: \n{output}";

                Console.WriteLine(displayText);
                WriteToFile($"insight-{index} {insightType}.txt", displayText);
            }
        }

        private static void WriteToFile(string filename, string text)
        {
            try
            {
                var folderPath = $"{AppDomain.CurrentDomain.BaseDirectory}/Output Data";
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                var filePath = Path.Combine(folderPath, filename);
                File.WriteAllText(filePath, text);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
