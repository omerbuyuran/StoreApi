using StoreApi.Domain.Model;

namespace StoreApi.Domain.Responses
{
    public class CustomerOrderResponse:BaseResponse
    {
        public List<Product> ProductList { get; set; }
        public Customer Customer { get; set; }
        public decimal? TotalPrice { get; set; }
        private CustomerOrderResponse(bool success, string message, List<Product> productList, Customer customer,decimal? totalPrice) : base(success, message)
        {
            this.ProductList = productList;
            this.Customer = customer;
            this.TotalPrice = totalPrice;
        }
        //Başarılı ise
        public CustomerOrderResponse(List<Product> productList, Customer Customer, decimal? totalPrice) : this(true, string.Empty, productList, Customer, totalPrice) { }
        //Başarısız ise
        public CustomerOrderResponse(string message) : this(false, message, null,null,null) { }
    }
}
