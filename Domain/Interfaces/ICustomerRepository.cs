using StoreApi.Domain.Model;

namespace StoreApi.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> ListAsync();
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void RemoveCustomer(int customerId);
        Customer GetCustomerById(int customerId);
        Customer GetCustomerByEmailandPassword(string email,string password);
        void SaveRefreshToken(int customerId, string refreshToken);
        Customer GetCustomerWithRefreshToken(string refreshToken);
        void RemoveRefreshToken(Customer customer);
    }
}
