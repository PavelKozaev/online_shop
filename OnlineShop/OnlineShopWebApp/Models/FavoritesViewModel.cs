namespace OnlineShopWebApp.Models
{
    public class FavoritesViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ProductViewModel Product { get; set; }
    }
}
