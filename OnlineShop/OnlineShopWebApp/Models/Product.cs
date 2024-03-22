namespace OnlineShopWebApp.Models
{
    public class Product(string name, decimal cost, string description = "")
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; } = name;
        public decimal Cost { get; } = cost;
        public string Description { get; } = description;

        public override string ToString() => $"{Id}\n{Name}\n{Cost}";
    }
}
