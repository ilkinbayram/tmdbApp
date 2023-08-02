using tmdb.Core.Entities.Abstract;

namespace tmdb.Core.Entities.Concrete
{
    public class BaseEntity : IEntity
    {
        public int Id {get; set;}
    }
}
