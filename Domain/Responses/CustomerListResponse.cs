using StoreApi.Domain.Model;

namespace StoreApi.Domain.Responses
{
    public class CustomerListResponse: BaseResponse
    {
        public IEnumerable<Customer> CustomerList { get; set; }
        private CustomerListResponse(bool success, string message, IEnumerable<Customer> CustomerList) : base(success, message)
        {
            this.CustomerList = CustomerList;
        }
        //Başarılı ise
        public CustomerListResponse(IEnumerable<Customer> CustomerList) : this(true, string.Empty, CustomerList) { }
        //Başarısız ise
        public CustomerListResponse(string message) : this(false, message, null) { }
    }
}
