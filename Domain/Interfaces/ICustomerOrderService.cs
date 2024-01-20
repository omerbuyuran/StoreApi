using StoreApi.Domain.Model;
using StoreApi.Domain.Request;
using StoreApi.Domain.Responses;

namespace StoreApi.Domain.Interfaces
{
    public interface ICustomerOrderService
    {
        Task<CustomerOrderResponse> GetOrderAsync(int customerOrderId);
        Task<CustomerOrderResponse> SaveOrderAsync(List<CustomerOrder> customerOrderRequests);
        Task UpdateOrderAsync(int customerOrderId, CustomerOrder request);
        void RemoveOrder(int customerId);
        void RemoveAllOrder(int customerId);
    }
}
