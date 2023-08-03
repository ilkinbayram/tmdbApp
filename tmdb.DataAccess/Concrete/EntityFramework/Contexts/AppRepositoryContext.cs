using Microsoft.EntityFrameworkCore;
using tmdb.Core.Entities.Concrete;
using tmdb.DataAccess.Concrete.EntityFramework.Configurations;

namespace tmdb.DataAccess.Concrete.EntityFramework.Contexts
{
    public class AppRepositoryContext : DbContext
    {
        public AppRepositoryContext(DbContextOptions<AppRepositoryContext> options) : base(options)
        {
        }

        public AppRepositoryContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EfWatchListConfig());
        }


        public DbSet<WatchList> WatchLists { get; set; }
    }
}
