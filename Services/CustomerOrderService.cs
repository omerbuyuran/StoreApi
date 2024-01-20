using StoreApi.Domain.Interfaces;
using StoreApi.Domain.Model;
using StoreApi.Domain.Request;
using StoreApi.Domain.Responses;

namespace StoreApi.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IProductRepository productRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ICustomerOrderRepository customerOrderRepository;
        public CustomerOrderService(IUnitOfWork unitOfWork, IProductRepository productRepository, ICustomerRepository CustomerRepository, ICustomerOrderRepository CustomerOrderRepository)
        {
            this.unitOfWork = unitOfWork;
            this.productRepository = productRepository;
            this.customerRepository = CustomerRepository;
            this.customerOrderRepository = CustomerOrderRepository;
        }

        public async Task<CustomerOrderResponse> GetOrderAsync(int customerId)
        {
            try
            {
                decimal? totalPrice = 0;
                IEnumerable<CustomerOrder> customerOrder = customerOrderRepository.GetCustomerOrderByCustomerId(customerId);

                Customer customer = customerRepository.GetCustomerById(customerId);
                List<Product> productList = new List<Product>();
                    foreach (CustomerOrder item in customerOrder)
                    {
                        //ürünler bulunuyor
                        Product product = await productRepository.GetProductByIdAsync(item.ProductId);
                        productList.Add(product);

                    totalPrice += item.TotalPrice;

                    }
                CustomerOrderResponse customerOrderResponse = new CustomerOrderResponse(productList, customer, totalPrice);

                return customerOrderResponse;
            }
            catch (Exception ex)
            {

                return new CustomerOrderResponse(ex.Message);
            }
            
        }

        public void RemoveAllOrder(int customerId)
        {
            try
            {
                customerOrderRepository.RemoveAllCustomerOrder(customerId);
                
                unitOfWork.Complete();
            }
            catch (Exception ex)
            {

                new CustomerOrderResponse(ex.Message);
            }
        }

        public void RemoveOrder(int customerOrderId)
        {
            try
            {
                customerOrderRepository.RemoveCustomerOrder(customerOrderId);

                unitOfWork.Complete();
            }
            catch (Exception ex)
            {

                new CustomerOrderResponse(ex.Message);
            }
        }

        public async Task<CustomerOrderResponse> SaveOrderAsync(List<CustomerOrder> request)
        {
            try
            {
                decimal? totalPrice = 0;
                List<Product> products = new List<Product>();
                Customer customer = customerRepository.GetCustomerById(request[0].CustomerId);

                for (int i = 0; i < request.Count; i++)
                {
                    CustomerOrder order = new CustomerOrder();
                    Product product = await productRepository.GetProductByIdAsync(request[i].ProductId);

                    order.CustomerId = request[i].CustomerId;
                    order.ProductId = request[i].ProductId;
                    order.Quantity = request[i].Quantity;
                    order.TotalPrice = Convert.ToDecimal(product.Price) * request[i].Quantity;
                    totalPrice += order.TotalPrice;
                    customerOrderRepository.AddCustomerOrder(order);
                    products.Add(product);
                }


                await unitOfWork.CompleteAsync();

                return new CustomerOrderResponse(products, customer, totalPrice);
            }
            catch (Exception ex)
            {

                return new CustomerOrderResponse(ex.Message);
            }
           



        }

        public async Task UpdateOrderAsync(int customerOrderId,CustomerOrder request)
        {
            try
            {
                Product product = await productRepository.GetProductByIdAsync(request.ProductId);
                CustomerOrder customerOrder = customerOrderRepository.GetCustomerOrderById(customerOrderId);

                customerOrder.ProductId = request.ProductId;
                customerOrder.Quantity = request.Quantity;
                customerOrder.CustomerId = request.CustomerId;
                customerOrder.TotalPrice = Convert.ToInt32(product.Price) * request.Quantity;

                customerOrderRepository.UpdateCustomerOrder(customerOrder);
                await unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {

                new CustomerOrderResponse(ex.Message);
            }
            
        }
    }
}
