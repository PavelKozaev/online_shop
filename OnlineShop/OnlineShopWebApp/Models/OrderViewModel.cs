namespace OnlineShopWebApp.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public UserDeliveryInfoViewModel User { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public OrderStatusViewModel Status { get; set; }
        public DateTime CreateDateTime { get; set; }
        public OrderViewModel()
        {
            Id = Guid.NewGuid();
            Status = OrderStatusViewModel.Created;
            CreateDateTime = DateTime.Now;
        }
        public decimal Cost => Items?.Sum(x => x.Cost) ?? 0;
    }
}
