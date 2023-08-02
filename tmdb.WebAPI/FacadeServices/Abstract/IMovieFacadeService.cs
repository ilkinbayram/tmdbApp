using tmdb.Service.Integrations.TheMovieDb.Model;

namespace tmdb.WebAPI.FacadeServices.Abstract
{
    public interface IMovieFacadeService
    {
        Task<List<FilmModel>> SearchByNameAsync(string name);
    }
}
