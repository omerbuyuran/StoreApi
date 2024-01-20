using StoreApi.Domain.Model;
using System.ComponentModel.DataAnnotations;

namespace StoreApi.Domain.Request
{
    public class CustomerOrderRequest
    {
        public int customerId { get; set; }

        public int productId { get; set; }

        public int quantity { get; set; }

        public decimal? totalPrice { get; set; }
    }
}
