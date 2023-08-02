using Newtonsoft.Json;

namespace tmdb.Service.Integrations.TheMovieDb.Model
{
    public class FilmSearchResultModel
    {
        [JsonProperty("results")]
        public List<FilmModel> Results { get; set; }
    }
}
