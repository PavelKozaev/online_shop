namespace OnlineShop.Db.Models
{
    public class Favorites
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public List<Product> Products { get; set; }
    }
}