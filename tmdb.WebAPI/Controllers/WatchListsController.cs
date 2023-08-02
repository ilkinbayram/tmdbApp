using Microsoft.AspNetCore.Mvc;
using tmdb.WebAPI.FacadeServices.Abstract;
using tmdb.WebAPI.Filters;

namespace tmdb.WebAPI.Controllers
{
    [Route("api/watchlists")]
    [ApiController]
    // [BasicAuthentication]
    public class WatchListsController : ControllerBase
    {
        private readonly IWatchListFacadeService _watchListFacadeService;
        public WatchListsController(IWatchListFacadeService watchListFacadeService)
        {
            _watchListFacadeService = watchListFacadeService;
        }

        [HttpGet("get-all-by-user/{userid}")]
        public async Task<IActionResult> GetAllWatchListsByUserAsync(int userid)
        {
            var response = await _watchListFacadeService.GetAllWatchListByUserAsync(userid);
            return Ok(response);
        }

        [HttpPost("add-film-to-watchlist/{userid}")]
        public async Task<IActionResult> AddWatchListForUser(int filmId, int userid)
        {
            var response = await _watchListFacadeService.AddAsync(filmId, userid);
            return Ok(response);
        }

        [HttpPost("mark-film-as-watched/{filmId}/{userid}")]
        public async Task<IActionResult> MarkWatchedFilmFromWatchList(int userid, int filmId)
        {
            var response = await _watchListFacadeService.SetAsWatchedAsync(userid, filmId);
            return Ok(response);
        }
    }
}
