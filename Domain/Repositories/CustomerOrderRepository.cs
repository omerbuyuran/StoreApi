using Microsoft.EntityFrameworkCore;
using StoreApi.Domain.Entities;
using StoreApi.Domain.Interfaces;
using StoreApi.Domain.Model;

namespace StoreApi.Domain.Repositories
{
    public class CustomerOrderRepository : BaseRepository, ICustomerOrderRepository
    {
        public CustomerOrderRepository(StoreDbContext context) : base(context)
        {
        }

        public void AddCustomerOrder(CustomerOrder CustomerOrder)
        {
            context.CustomerOrders.Add(CustomerOrder);
        }

        public IEnumerable<CustomerOrder> GetCustomerOrderByCustomerId(int customerId)
        {
            return context.CustomerOrders.ToList().Where(x => x.CustomerId == customerId);
        }

        public CustomerOrder GetCustomerOrderById(int CustomerOrderId)
        {
            return context.CustomerOrders.Find(CustomerOrderId);
        }

        public async Task<IEnumerable<CustomerOrder>> ListAsync()
        {
            return await context.CustomerOrders.ToListAsync();
        }

        public void RemoveCustomerOrder(int CustomerOrderId)
        {
            var CustomerOrder = GetCustomerOrderById(CustomerOrderId);
            context.CustomerOrders.Remove(CustomerOrder);
        }

        public void RemoveAllCustomerOrder(int CustomerId)
        {
            IEnumerable<CustomerOrder> CustomerOrder = GetCustomerOrderByCustomerId(CustomerId);
            foreach (CustomerOrder order in CustomerOrder)
            {
                context.CustomerOrders.Remove(order);
            }
        }

        public void UpdateCustomerOrder(CustomerOrder CustomerOrder)
        {
            context.CustomerOrders.Update(CustomerOrder);
        }
    }
}
