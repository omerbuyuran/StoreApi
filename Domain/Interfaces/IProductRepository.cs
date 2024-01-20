using StoreApi.Domain.Model;

namespace StoreApi.Domain.Interfaces
{
    public interface IProductRepository
    {
        //EntityFramework kullanacağımdan dolayı işlemler async olarak kullanacağım. Dönüşler bu yüzden Task
        Task<IEnumerable<Product>> ListAsync();
        Task AddProductAsync(Product product);
        void UpdateProduct(Product product);
        Task RemoveProductAsync(int productId);
        Task<Product> GetProductByIdAsync(int productId);

    }
}
