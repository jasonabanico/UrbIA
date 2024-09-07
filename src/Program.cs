using Urbia.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Urbia
{
    public class Program
    {
        private static Dictionary<string, string> _insightInputFiles = new Dictionary<string, string>()
        {
            //{ "Safety Insights", "VICTORIAN_ROAD_CRASH_DATA - excerpt.geojson" },
            { "Capacity Forcest Insights", "Traffic_Volume - excerpt.geojson" }
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

            foreach (var insightType in _insightInputFiles.Keys)
            {
                var filename = _insightInputFiles[insightType];
                Console.WriteLine($"Sample {insightType} from data {filename}.");
                Console.WriteLine();
                (var data, var output) = insightsProcessor.ProcessInsightAsync(insightType, filename).Result;
                Console.WriteLine($"Data: \n{data}");
                Console.WriteLine($"{insightType} Suggestions: \n{output}");
            }
        }
    }
}
