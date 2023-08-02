using Core.DataAccess.EntityFramework;
using tmdb.Core.Entities.Concrete;
using tmdb.DataAccess.Abstract;
using tmdb.DataAccess.Concrete.EntityFramework.Contexts;

namespace tmdb.DataAccess.Concrete.EntityFramework
{
    public class WatchListDal : EfEntityRepositoryBase<WatchList, AppRepositoryContext>, IWatchListDal
    {
        public WatchListDal(AppRepositoryContext applicationContext) : base(applicationContext)
        {
        }
    }
}
