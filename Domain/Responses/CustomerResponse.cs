using StoreApi.Domain.Model;

namespace StoreApi.Domain.Responses
{
    public class CustomerResponse : BaseResponse
    {
        public Customer Customer { get; set; }
        private CustomerResponse(bool success, string message, Customer Customer) : base(success, message)
        {
            this.Customer = Customer;
        }
        //Başarılı ise
        public CustomerResponse(Customer Customer) : this(true, string.Empty, Customer) { }
        //Başarısız ise
        public CustomerResponse(string message) : this(false, message, null) { }
    }
}
