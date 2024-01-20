using System.ComponentModel.DataAnnotations;

namespace StoreApi.Domain.Request
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}
