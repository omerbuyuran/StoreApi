using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StoreApi.Domain.Entities;
using StoreApi.Domain.Interfaces;
using StoreApi.Domain.Model;
using StoreApi.Security.Token;

namespace StoreApi.Domain.Repositories
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        private readonly TokenOptions tokenOptions;
        public CustomerRepository(StoreDbContext context, IOptions<TokenOptions> tokenOptions) : base(context)
        {
            this.tokenOptions = tokenOptions.Value;
        }
        public void AddCustomer(Customer customer)
        {
            context.Customers.Add(customer);
        }

        public Customer GetCustomerByEmailandPassword(string email, string password)
        {
            return context.Customers.Where(c => c.Email == email && c.Password == password).FirstOrDefault();
        }

        public Customer GetCustomerById(int customerId)
        {
            return context.Customers.Find(customerId);
        }

        public Customer GetCustomerWithRefreshToken(string refreshToken)
        {
            return context.Customers.FirstOrDefault(c => c.RefreshToken == refreshToken);
        }

        public async Task<IEnumerable<Customer>> ListAsync()
        {
            return await context.Customers.ToListAsync();
        }

        public void RemoveCustomer(int CustomerId)
        {
            var Customer = GetCustomerById(CustomerId);
            context.Customers.Remove(Customer);
        }

        public void RemoveRefreshToken(Customer customer)
        {
            Customer newCustomer = this.GetCustomerById(customer.Id);
            newCustomer.RefreshToken = null;
            newCustomer.RefreshTokenEndDate = null;
        }

        public void SaveRefreshToken(int customerId, string refreshToken)
        {
            Customer newCustomer = this.GetCustomerById(customerId);
            newCustomer.RefreshToken = refreshToken;
            newCustomer.RefreshTokenEndDate= DateTime.Now.AddMinutes(tokenOptions.RefreshTokenExpiration);
        }

        public void UpdateCustomer(Customer Customer)
        {
            context.Customers.Update(Customer);
        }
    }
}
