using tmdb.Core.Entities.Abstract;

namespace tmdb.Core.Entities.Concrete.Dtos
{
    public class AddUpdateWatchListDto : IDto
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
    }
}
