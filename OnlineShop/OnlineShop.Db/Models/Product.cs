namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }

        public List<Image> Images { get; set; }

        public List<CartItem> CartItems { get; set;}

        public List<Favorites> Favorites { get; set; }
               

        public Product(Guid id, string name, string author, decimal cost, string description) : this()
        {
            Id = id;
            Name = name;
            Author = author;
            Cost = cost;
            Description = description;            
        }

        public Product() 
        {
            Images = new List<Image>();
            CartItems = new List<CartItem>();
            Favorites = new List<Favorites>();
        }
    }
}
