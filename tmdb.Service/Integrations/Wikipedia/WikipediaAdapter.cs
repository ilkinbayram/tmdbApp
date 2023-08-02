using Newtonsoft.Json.Linq;
using tmdb.Service.ExternalServices;

namespace tmdb.Service.Integrations.Wikipedia
{
    public class WikipediaAdapter : IWikipediaService
    {
        private readonly HttpClient _httpClient;
        public WikipediaAdapter()
        {
            _httpClient = new HttpClient();
        }
        public async Task<string> GetWikipediaSummary(string query)
        {
            string url = $"https://en.wikipedia.org/w/api.php?action=query&format=json&list=search&srsearch={query}";

            HttpResponseMessage response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            JObject searchResult = JObject.Parse(responseBody);
            JArray searchArray = (JArray)searchResult["query"]["search"];

            if (searchArray.Count > 0)
            {
                string pageId = searchArray[0]["pageid"].ToString();
                url = $"https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&exintro=true&explaintext=true&pageids={pageId}";

                response = await _httpClient.GetAsync(url);

                try
                {
                    response.EnsureSuccessStatusCode();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                responseBody = await response.Content.ReadAsStringAsync();

                JObject pageResult = JObject.Parse(responseBody);
                return (string)pageResult["query"]["pages"][pageId]["extract"];
            }
            else
            {
                return "No results found.";
            }
        }
    }
}
