using System.Linq.Expressions;
using tmdb.Core.Entities.Abstract;

namespace Core.DataAccess
{
    public interface IEntityQueryableRepository<T> 
        where T: class, IEntity, new() 
    {
        IQueryable<T> Query(Expression<Func<T, bool>> expression = null);
        Task<IQueryable<T>> QueryAsync(Expression<Func<T, bool>> filter = null);
    }
}
