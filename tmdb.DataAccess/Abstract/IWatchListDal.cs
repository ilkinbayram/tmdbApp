using Core.DataAccess;
using tmdb.Core.Entities.Concrete;

namespace tmdb.DataAccess.Abstract
{
    public interface IWatchListDal : IEntityRepository<WatchList>
    {
    }
}
