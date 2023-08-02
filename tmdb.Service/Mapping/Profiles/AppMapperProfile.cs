using AutoMapper;
using tmdb.Core.Entities.Concrete;
using tmdb.Core.Entities.Concrete.Dtos;

namespace tmdb.Service.Mapping.Profiles
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            #region WatchList
            CreateMap<AddUpdateWatchListDto, WatchList>();
            CreateMap<WatchList, GetWatchListDto>();
            #endregion
        }
    }
}
