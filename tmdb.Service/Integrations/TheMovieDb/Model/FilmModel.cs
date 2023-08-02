using Newtonsoft.Json;

namespace tmdb.Service.Integrations.TheMovieDb.Model
{
    public class FilmModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("original_title")]
        public string Name { get; set; }

        [JsonProperty("overview")]
        public string Description { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("vote_average")]
        public float Rating { get; set; }

        [JsonProperty("vote_count")]
        public int RatedCount { get; set; }
    }
}
