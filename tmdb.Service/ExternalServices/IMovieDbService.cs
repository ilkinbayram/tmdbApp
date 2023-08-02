using tmdb.Service.Integrations.TheMovieDb.Model;

namespace tmdb.Service.ExternalServices
{
    public interface IMovieDbService
    {
        Task<List<FilmModel>> SearchByNameAsync(string name);
        Task<FilmModel> FindByIdAsync(int id);
        Task<List<string>> GetFilmPostersAsync(int id);
    }
}
