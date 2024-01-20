using StoreApi.Domain.Model;
using StoreApi.Domain.Responses;

namespace StoreApi.Domain.Interfaces
{
    public interface IProductService
    {
        Task<ProductListResponse> ListAsync();
        Task<ProductResponse> AddProduct(Product product);
        Task<ProductResponse> UpdateProduct(Product product,int productId);
        Task<ProductResponse> RemoveProduct(int productId);
        Task<ProductResponse> GetProductByIdAsync(int productId);
    }
}
