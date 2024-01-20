using StoreApi.Domain.Model;

namespace StoreApi.Domain.Interfaces
{
    public interface ICustomerOrderRepository
    {
        //EntityFramework kullanacağımdan dolayı işlemler async olarak kullanacağım. Dönüşler bu yüzden Task
        Task<IEnumerable<CustomerOrder>> ListAsync();
        void AddCustomerOrder(CustomerOrder customerOrder);
        void UpdateCustomerOrder(CustomerOrder customerOrder);
        void RemoveCustomerOrder(int customerOrderId);
        void RemoveAllCustomerOrder(int CustomerId);
        CustomerOrder GetCustomerOrderById(int customerOrderId);
        IEnumerable<CustomerOrder> GetCustomerOrderByCustomerId(int customerId);
    }
}
