using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class Register
    {
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
		public string ConfirmPassword { get; set; }
		public string PhoneNumber { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
