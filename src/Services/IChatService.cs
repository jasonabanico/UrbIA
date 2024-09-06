namespace Urbia.Services
{
    public interface IChatService
    {
        Task<string> ChatAsync(string systemMessage, string userMessage);
    }
}
