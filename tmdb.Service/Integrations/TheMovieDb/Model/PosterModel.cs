using Newtonsoft.Json;

namespace tmdb.Service.Integrations.TheMovieDb.Model
{
    public class PosterModel
    {
        [JsonProperty("file_path")]
        public string Path { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }
    }
}
