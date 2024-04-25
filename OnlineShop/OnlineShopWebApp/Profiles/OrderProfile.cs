using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<UserDeliveryInfo, UserDeliveryInfoViewModel>().ReverseMap();
            CreateMap<OrderStatus, OrderStatusViewModel>().ReverseMap();
        }
    }
}
