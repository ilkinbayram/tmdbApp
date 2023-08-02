using Newtonsoft.Json;

namespace tmdb.Service.Integrations.TheMovieDb.Model
{
    public class FilmPosterResult
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("posters")]
        public List<PosterModel> Posters { get; set; }
    }
}
