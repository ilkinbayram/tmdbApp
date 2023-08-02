namespace tmdb.WebAPI.AppServices
{
    public interface IMovieService
    {
        Task CheckWatchlistAndSendEmailAsync();
    }
}
