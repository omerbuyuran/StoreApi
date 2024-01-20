namespace StoreApi.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
        void Complete();
    }
}
