namespace Urbia.Services
{
    public interface IInsightsProcessor
    {
        Task<(string, string)> ProcessInsightAsync(string insightType, string filename);
    }
}
