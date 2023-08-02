namespace tmdb.Core.Entities.Concrete.Dtos
{
    public class GetWatchListDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public bool IsWatched { get; set; }
        public float MovieRating { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
