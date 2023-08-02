namespace tmdb.Service.ExternalServices
{
    public interface IWikipediaService
    {
        Task<string> GetWikipediaSummary(string query);
    }
}
