using Microsoft.AspNetCore.Mvc;
using tmdb.WebAPI.FacadeServices.Abstract;
using tmdb.WebAPI.Filters;

namespace tmdb.WebAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    // [BasicAuthentication]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieFacadeService _movieFacadeService;
        public MoviesController(IMovieFacadeService movieFacadeService)
        {
            _movieFacadeService = movieFacadeService;
        }


        [HttpGet("search-by-name/{name}")]
        public async Task<IActionResult> GetMovieByNameAsync(string name)
        {
            var response = await _movieFacadeService.SearchByNameAsync(name);
            return Ok(response);
        }
    }
}
