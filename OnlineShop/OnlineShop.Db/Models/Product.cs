namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal Cost { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }
        public List<CartItem> CartItems { get; set;}
        public Product(string Name, string Author, decimal Cost, string Description, string ImagePath)
        {
            Id = Guid.NewGuid();
            this.Name = Name;
            this.Author = Author;
            this.Cost = Cost;
            this.Description = Description;
            this.ImagePath = ImagePath;
            CartItems = new List<CartItem>();
        }

        public Product(Guid Id, string Name, string Author, decimal Cost, string Description, string ImagePath)
        {
            this.Id = Id;
            this.Name = Name;
            this.Author = Author;
            this.Cost = Cost;
            this.Description = Description;
            this.ImagePath = ImagePath;
            CartItems = new List<CartItem>();
        }
    }
}
