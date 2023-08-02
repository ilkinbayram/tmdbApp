using System.Text;
using tmdb.Core.Entities.Concrete;
using tmdb.Service.Abstract;
using tmdb.Service.ExternalServices;
using tmdb.Service.Integrations.GmailSmtp.Model;
using tmdb.Service.Integrations.TheMovieDb.Model;

namespace tmdb.WebAPI.AppServices
{
    public class MovieService : IMovieService
    {
        private readonly IEmailService _emailService;
        private readonly IWatchListService _watchListService;
        private readonly IMovieDbService _movieDbService;
        private readonly IWikipediaService _wikipediaService;
        private readonly string _imagePrefix;

        public MovieService(IEmailService emailService,
                            IWatchListService watchListService,
                            IMovieDbService movieDbService,
                            IWikipediaService wikipediaService,
                            IConfiguration configuration)
        {
            _emailService = emailService;
            _watchListService = watchListService;
            _movieDbService = movieDbService;
            _wikipediaService = wikipediaService;
            _imagePrefix = configuration["TheMovieDb:ImagePrefixPath"];
        }

        public async Task CheckWatchlistAndSendEmailAsync()
        {
            List<WatchList> watchLists = await _watchListService.GetAllWatchListsAsync();

            var result = watchLists
                .GroupBy(w => w.UserId)
                .Select(g => new
                {
                    UserId = g.Key,
                    TopRatedMovies = g.OrderByDescending(w => w.MovieRating).Take(3)
                });

            foreach (var group in result)
            {
                foreach (var watchList in group.TopRatedMovies)
                {
                    // Here the emai should be sent to the User's email which could be found by group.UserId

                    var film = await _movieDbService.FindByIdAsync(watchList.MovieId);

                    var wikiDescription = await _wikipediaService.GetWikipediaSummary(film.Name);

                    var threeSentenceWikipedia = wikiDescription.Split('.').Take(3).ToArray();

                    var description = string.Join(" ", threeSentenceWikipedia);

                    JobTemplateModel templateModel = new JobTemplateModel
                    {
                        film_name = film.Name,
                        poster_uri = $"{_imagePrefix}/{film.PosterPath}",
                        rating = $"Film Rating:  {film.Rating}",
                        wiki_description = description
                    };

                    await _emailService.SendEmailAsync("ilkinbayramsoy@gmail.com", templateModel);
                }
            }
        }
    }
}
