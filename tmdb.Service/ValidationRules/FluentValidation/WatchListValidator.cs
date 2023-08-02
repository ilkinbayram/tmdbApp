using FluentValidation;
using tmdb.Core.Entities.Concrete;
using tmdb.Core.Entities.Concrete.Dtos;

namespace tmdb.Service.ValidationRules.FluentValidation
{
    public class WatchListValidator : AbstractValidator<AddUpdateWatchListDto>
    {
        public WatchListValidator()
        {
        }
    }
}
