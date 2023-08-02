using AutoMapper;
using System.Linq.Expressions;
using tmdb.Core.Aspects.Autofac.Validation;
using tmdb.Core.Entities.Concrete;
using tmdb.Core.Entities.Concrete.Dtos;
using tmdb.Core.Utilities.Results;
using tmdb.DataAccess.Abstract;
using tmdb.Service.Abstract;
using tmdb.Service.Constants;
using tmdb.Service.ExternalServices;
using tmdb.Service.ValidationRules.FluentValidation;

namespace tmdb.Service.Concrete
{
    public class WatchListService : IWatchListService
    {
        private readonly IWatchListDal _watchListDal;
        private readonly IMovieDbService _movieDbService;
        private readonly IMapper _mapper;
        public WatchListService(IWatchListDal watchListDal, 
                                IMapper mapper,
                                IMovieDbService movieDbService)
        {
            _watchListDal = watchListDal;
            _movieDbService = movieDbService;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(WatchListValidator), Priority = 1)]
        public async Task<IResult> AddAsync(AddUpdateWatchListDto dto)
        {
            var film = await _movieDbService.FindByIdAsync(dto.MovieId);

            if (film == null) return new ErrorResult(Messages.FilmIsNotFoundById);

            var existEntity = await _watchListDal.GetAsync(x=>x.UserId == dto.UserId && x.MovieId == dto.MovieId);

            if(existEntity != null) return new ErrorResult(Messages.SameEntityExistError);

            var entity = _mapper.Map<WatchList>(dto);

            entity.MovieRating = film.Rating;

            await _watchListDal.AddAsync(entity);

            return new SuccessResult(Messages.WatchListIsAdded);
        }

        public async Task<IDataResult<List<GetWatchListDto>>> GetAllByUserAsync(int userId)
        {
            var result = new List<GetWatchListDto>();
            var entities = await _watchListDal.GetListAsync(x=>x.UserId == userId);

            foreach (var entity in entities)
            {
                result.Add(_mapper.Map<GetWatchListDto>(entity));
            }

            return new SuccessDataResult<List<GetWatchListDto>>(result);
        }

        public async Task<List<WatchList>> GetAllWatchListsAsync(bool isWatched = false)
        {
            return await _watchListDal.GetListAsync(x=>x.IsWatched == isWatched);
        }

        public async Task<IDataResult<GetWatchListDto>> GetAsync(Expression<Func<WatchList, bool>> expression)
        {
            var entity = await _watchListDal.GetAsync(expression);

            if(entity == null) return new ErrorDataResult<GetWatchListDto>(Messages.WatchListNotFound);

            var result = _mapper.Map<GetWatchListDto>(entity);

            return new SuccessDataResult<GetWatchListDto>(result);
        }

        public async Task<IResult> SetAsWatchedAsync(AddUpdateWatchListDto dto)
        {
            var entity = await _watchListDal.GetAsync(x=>x.UserId == dto.UserId && x.MovieId == dto.MovieId && !x.IsWatched);

            if (entity == null) return new ErrorResult(Messages.WatchListNotFound);

            entity.IsWatched = true;

            await _watchListDal.UpdateAsync(entity);

            return new SuccessResult(Messages.FilmSettledAsWatched);
        }
    }
}
