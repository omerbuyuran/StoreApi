using StoreApi.Domain.Model;

namespace StoreApi.Domain.Responses
{
    public class ProductResponse : BaseResponse
    {
        public Product Product { get; set; }
        private ProductResponse(bool success, string message,Product product) : base(success, message)
        {
            this.Product = product;
        }
        //Başarılı ise
        public ProductResponse(Product product) : this(true, string.Empty, product) { }
        //Başarısız ise
        public ProductResponse(string message):this(false, message, null) { }
    }
}
