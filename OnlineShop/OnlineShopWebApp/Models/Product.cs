namespace OnlineShopWebApp.Models
{
    public class Product(int id, string name, decimal cost, string description = "")
    {
        public int Id { get; } = id;
        public string Name { get; } = name;
        public decimal Cost { get; } = cost;
        public string Description { get; } = description;

        public override string ToString() => $"{Id}\n{Name}\n{Cost}";
    }
}
