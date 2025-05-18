using Api.Data;

namespace Api.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly BaseDbContext context;
        protected BaseRepository(BaseDbContext context)
        {
            this.context = context;
        }
    }
}
