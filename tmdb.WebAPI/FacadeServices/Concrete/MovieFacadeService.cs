using tmdb.Service.ExternalServices;
using tmdb.Service.Integrations.TheMovieDb.Model;
using tmdb.WebAPI.FacadeServices.Abstract;

namespace tmdb.WebAPI.FacadeServices.Concrete
{
    public class MovieFacadeService : IMovieFacadeService
    {
        private readonly IMovieDbService _movieDbService;
        public MovieFacadeService(IMovieDbService movieDbService)
        {
            _movieDbService = movieDbService;
        }

        public async Task<List<FilmModel>> SearchByNameAsync(string name)
        {
            var result = await _movieDbService.SearchByNameAsync(name);
            return result;
        }
    }
}
