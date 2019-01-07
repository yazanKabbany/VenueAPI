using VenuesApi.Models;

namespace VenuesApi.Data.Repositories
{
    public abstract class BaseRepository
    {
        public VenuesDbContext Context { get; }
        protected BaseRepository(VenuesDbContext context)
        {
            Context = context;
        }
    }
}