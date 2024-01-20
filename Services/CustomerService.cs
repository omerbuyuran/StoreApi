using StoreApi.Domain.Interfaces;
using StoreApi.Domain.Model;
using StoreApi.Domain.Responses;

namespace StoreApi.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository CustomerRepository;
        private readonly IUnitOfWork unitOfWork;
        public CustomerService(ICustomerRepository CustomerRepository, IUnitOfWork unitOfWork)
        {
            this.CustomerRepository = CustomerRepository;
            this.unitOfWork = unitOfWork;
        }

        public CustomerResponse AddCustomer(Customer Customer)
        {
            try
            {
                CustomerRepository.AddCustomer(Customer);
                unitOfWork.Complete();
                return new CustomerResponse(Customer);
            }
            catch (Exception ex)
            {
                return new CustomerResponse(ex.Message);
            }
        }

        public CustomerResponse GetCustomerById(int CustomerId)
        {
            try
            {
                Customer Customer = CustomerRepository.GetCustomerById(CustomerId);
                if (Customer == null)
                {
                    return new CustomerResponse("Customer not found");
                }
                else
                {
                    return new CustomerResponse(Customer);
                }
            }
            catch (Exception ex)
            {
                return new CustomerResponse(ex.Message);
            }
        }

        public CustomerResponse GetCustomerWithEmailAndPassword(string email, string password)
        {
            Customer Customer = CustomerRepository.GetCustomerByEmailandPassword(email,password);
            if (Customer == null)
            {
                return new CustomerResponse("Customer not found");
            }
            else
            {
                return new CustomerResponse(Customer);
            }
        }

        public CustomerResponse GetCustomerWithRefreshToken(string refreshToken)
        {
            Customer Customer = CustomerRepository.GetCustomerWithRefreshToken(refreshToken);
            if (Customer == null)
            {
                return new CustomerResponse("Customer not found");
            }
            else
            {
                return new CustomerResponse(Customer);
            }
        }

        public async Task<CustomerListResponse> ListAsync()
        {
            try
            {
                IEnumerable<Customer> Customers = await CustomerRepository.ListAsync();
                return new CustomerListResponse(Customers);
            }
            catch (Exception ex)
            {
                return new CustomerListResponse(ex.Message);
            }
        }

        public CustomerResponse RemoveCustomer(int CustomerId)
        {
            try
            {
                Customer Customer = CustomerRepository.GetCustomerById(CustomerId);
                if (Customer == null)
                {
                    return new CustomerResponse("Customer not found");
                }
                else
                {
                    CustomerRepository.RemoveCustomer(CustomerId);
                    unitOfWork.Complete();
                    return new CustomerResponse(Customer);
                }
            }
            catch (Exception ex)
            {
                return new CustomerResponse(ex.Message);
            }
        }

        public void RemoveRefreshToken(Customer customer)
        {
            try
            {
                CustomerRepository.RemoveRefreshToken(customer);
                unitOfWork.Complete();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SaveRefreshToken(int customerId, string refreshToken)
        {
            try
            {
                CustomerRepository.SaveRefreshToken(customerId, refreshToken);
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public CustomerResponse UpdateCustomer(Customer Customer, int CustomerId)
        {
            try
            {
                Customer firstCustomer = CustomerRepository.GetCustomerById(CustomerId);
                if (firstCustomer == null)
                {
                    return new CustomerResponse("Customer not found");
                }
                else
                {
                    firstCustomer.Name = Customer.Name;
                    firstCustomer.Surname = Customer.Surname;
                    firstCustomer.Address = Customer.Address;
                    firstCustomer.Email = Customer.Email;
                    firstCustomer.Password = Customer.Password;
                    CustomerRepository.UpdateCustomer(firstCustomer);
                    unitOfWork.Complete();

                    return new CustomerResponse(firstCustomer);
                }
            }
            catch (Exception ex)
            {
                return new CustomerResponse(ex.Message);
            }
        }
    }
}
