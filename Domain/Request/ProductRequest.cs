using System.ComponentModel.DataAnnotations;

namespace StoreApi.Domain.Request
{
    public class ProductRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal? Price { get; set; }
    }
}
