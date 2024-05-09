namespace OnlineShopWebApp.Models
{
    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsRememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
