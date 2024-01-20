using StoreApi.Domain.Entities;
using StoreApi.Domain.Interfaces;
using StoreApi.Domain.Repositories;

namespace StoreApi.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext context;
        public UnitOfWork(StoreDbContext context)
        {
            this.context = context;
        }

        public void Complete()
        {
            this.context.SaveChanges();
        }

        public async Task CompleteAsync()
        {
            await this.context.SaveChangesAsync();
        }
    }
}
