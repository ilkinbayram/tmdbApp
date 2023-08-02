using tmdb.Core.Entities.Concrete.Dtos;
using myResult = tmdb.Core.Utilities.Results;

namespace tmdb.WebAPI.FacadeServices.Abstract
{
    public interface IWatchListFacadeService
    {
        Task<myResult.IResult> AddAsync(int movieId, int userId);
        Task<myResult.IResult> SetAsWatchedAsync(int movieId, int userId);
        Task<myResult.IDataResult<List<GetWatchListDto>>> GetAllWatchListByUserAsync(int userId);
    }
}
