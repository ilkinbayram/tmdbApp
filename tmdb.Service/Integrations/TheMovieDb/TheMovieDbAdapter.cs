using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using tmdb.Service.ExternalServices;
using tmdb.Service.Integrations.TheMovieDb.Model;

namespace tmdb.Service.Integrations.TheMovieDb
{
    public class TheMovieDbAdapter : IMovieDbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;
        private readonly string _imagePrefixPath;
        public TheMovieDbAdapter(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _token = configuration["TheMovieDb:BearerToken"];
            _imagePrefixPath = configuration["TheMovieDb:ImagePrefixPath"];
        }

        public async Task<List<FilmModel>> SearchByNameAsync(string name)
        {
            string encodedMovieName = System.Net.WebUtility.UrlEncode(name);
            string requestUri = $"https://api.themoviedb.org/3/search/movie?query={encodedMovieName}&include_adult=false&language=en-US&page=1";
            var request = PrepareRequest(requestUri);
            using (var response = await _httpClient.SendAsync(request))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FilmSearchResultModel>(body);
                    var listResult = result.Results;
                    return listResult;
                }
                catch (Exception ex)
                {
                    return new List<FilmModel>();
                }
            }
        }

        public async Task<FilmModel> FindByIdAsync(int id)
        {
            string requestUri = $"https://api.themoviedb.org/3/movie/{id}?language=en-US";
            var request = PrepareRequest(requestUri);
            using (var response = await _httpClient.SendAsync(request))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<FilmModel>(body);
                    return result;
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
        }

        public async Task<List<string>> GetFilmPostersAsync(int id)
        {
            string requestUri = $"https://api.themoviedb.org/3/movie/{id}/images?include_image_language=en&language=en";
            var request = PrepareRequest(requestUri);
            var result = new List<string>();
            using (var response = await _httpClient.SendAsync(request))
            {
                try
                {
                    response.EnsureSuccessStatusCode();
                    var body = await response.Content.ReadAsStringAsync();
                    var posterResult = JsonConvert.DeserializeObject<FilmPosterResult>(body);
                    foreach (var poster in posterResult.Posters)
                    {
                        result.Add($"{_imagePrefixPath}/{poster.Path}");
                    }
                    return result;
                }
                catch (Exception ex)
                {
                    return new List<string>();
                }
            }
        }

        private HttpRequestMessage PrepareRequest(string uri)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{uri}"),
                Headers =
                {
                    { "accept", "application/json" },
                    { "Authorization", $"Bearer {_token}" },
                },
            };

            return request;
        }
    }
}
