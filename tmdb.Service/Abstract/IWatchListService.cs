using System.Linq.Expressions;
using tmdb.Core.Entities.Concrete;
using tmdb.Core.Entities.Concrete.Dtos;
using tmdb.Core.Utilities.Results;

namespace tmdb.Service.Abstract
{
    public interface IWatchListService
    {
        Task<IResult> AddAsync(AddUpdateWatchListDto dto);
        Task<IDataResult<GetWatchListDto>> GetAsync(Expression<Func<WatchList, bool>> expression);
        Task<IResult> SetAsWatchedAsync(AddUpdateWatchListDto dto);
        Task<IDataResult<List<GetWatchListDto>>> GetAllByUserAsync(int userId);
        Task<List<WatchList>> GetAllWatchListsAsync(bool isWatched = false);
    }
}
