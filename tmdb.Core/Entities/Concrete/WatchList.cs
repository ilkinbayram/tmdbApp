namespace tmdb.Core.Entities.Concrete
{
    public class WatchList : BaseEntity
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public bool IsWatched { get; set; }
        public float MovieRating { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
