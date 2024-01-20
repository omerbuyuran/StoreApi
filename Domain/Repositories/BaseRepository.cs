using StoreApi.Domain.Entities;

namespace StoreApi.Domain.Repositories
{
    public class BaseRepository
    {
        protected readonly StoreDbContext context;
        public BaseRepository(StoreDbContext context)
        {
            this.context = context;
        }
    }
}
