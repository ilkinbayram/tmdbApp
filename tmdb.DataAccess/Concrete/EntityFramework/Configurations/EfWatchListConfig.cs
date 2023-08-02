using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using tmdb.Core.Entities.Concrete;

namespace tmdb.DataAccess.Concrete.EntityFramework.Configurations
{
    public class EfWatchListConfig : IEntityTypeConfiguration<WatchList>
    {
        public void Configure(EntityTypeBuilder<WatchList> builder)
        {
            builder.ToTable("WatchLists");
            builder.HasKey(k => k.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x=>x.AddedDate).HasDefaultValue(DateTime.UtcNow);

            builder.HasIndex(u => new { u.UserId, u.MovieId }).IsUnique();
        }
    }
}
