namespace OnlineShop.Db.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public UserDeliveryInfo UserDeliveryInfo { get; set; }
        public List<CartItem> Items { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}