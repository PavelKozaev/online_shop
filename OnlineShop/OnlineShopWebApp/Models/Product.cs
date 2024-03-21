namespace OnlineShopWebApp.Models
{
    public class Product(string name, decimal cost, string description = "")
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public decimal Cost { get; set; } = cost;
        public string Description { get; set; } = description;

        public override string ToString() => $"{Id}\n{Name}\n{Cost}";
    }
}
