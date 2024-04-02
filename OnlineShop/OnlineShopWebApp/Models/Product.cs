namespace OnlineShopWebApp.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public decimal Cost { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

        public Product() => Id = Guid.NewGuid();

        public Product(string name, string author, decimal cost, string description, string imageUrl)
        {
            Id = Guid.NewGuid();
            Name = name;
            Author = author;
            Cost = cost;
            Description = description;
            ImageUrl = imageUrl;
        }        
    }
}
