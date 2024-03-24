namespace OnlineShopWebApp.Models
{
    public class Product(string name, string author, decimal cost, string description, string imageUrl)
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; } = name;
        public string Author { get; } = author;
        public decimal Cost { get; } = cost;
        public string Description { get; } = description;
        public string ImageUrl { get; } = imageUrl;

        public override string ToString() => $"{Id}\n{Name}\n{Author}\n{Cost}";
    }
}
