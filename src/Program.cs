using Urbia.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Urbia
{
    public class Program
    {
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
                .BuildServiceProvider();

            Console.WriteLine("Urbia Proof-of-Concept");
            Console.WriteLine("--------------------------");
            Console.WriteLine();
            Console.WriteLine("Retrieval Augmented Generation");
            Console.WriteLine();
            Console.WriteLine("Traffice Accident Data");
            Console.WriteLine();
            Console.WriteLine("Patronage by Day Type and by Mode");
            Console.WriteLine();
            Console.WriteLine();

            var chatService = new ChatService(config, null);
            var result = chatService.ChatAsync("", "give me a test message").Result;
        }
    }
}
