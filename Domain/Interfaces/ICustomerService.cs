using StoreApi.Domain.Model;
using StoreApi.Domain.Responses;

namespace StoreApi.Domain.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerListResponse> ListAsync();
        CustomerResponse AddCustomer(Customer customer);
        CustomerResponse UpdateCustomer(Customer customer, int customerId);
        CustomerResponse RemoveCustomer(int customerId);
        CustomerResponse GetCustomerById(int customerId);
        CustomerResponse GetCustomerWithEmailAndPassword(string email, string password);
        void SaveRefreshToken(int customerId, string refreshToken);
        CustomerResponse GetCustomerWithRefreshToken(string refreshToken);
        void RemoveRefreshToken(Customer customer);
    }
}
