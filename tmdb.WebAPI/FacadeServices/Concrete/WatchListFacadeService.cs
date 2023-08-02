using tmdb.Core.Entities.Concrete.Dtos;
using tmdb.Service.Abstract;
using tmdb.WebAPI.FacadeServices.Abstract;
using myResult = tmdb.Core.Utilities.Results;

namespace tmdb.WebAPI.FacadeServices.Concrete
{
    public class WatchListFacadeService : IWatchListFacadeService
    {
        private readonly IWatchListService _watchListService;
        public WatchListFacadeService(IWatchListService watchListService)
        {
            _watchListService = watchListService;
        }


        public Task<myResult.IDataResult<List<GetWatchListDto>>> GetAllWatchListByUserAsync(int userId)
        {
            var result = _watchListService.GetAllByUserAsync(userId);
            return result;
        }

        public async Task<myResult.IResult> AddAsync(int movieId, int userId)
        {
            var dto = new AddUpdateWatchListDto { MovieId = movieId, UserId = userId };
            var result = await _watchListService.AddAsync(dto);
            return result;
        }

        public async Task<myResult.IResult> SetAsWatchedAsync(int movieId, int userId)
        {
            var updateDto = new AddUpdateWatchListDto {  UserId = movieId, MovieId = userId};
            var result = await _watchListService.SetAsWatchedAsync(updateDto);
            return result;
        }
    }
}
