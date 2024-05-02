namespace OnlineShopWebApp.Models
{
    public class FavoritesViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
