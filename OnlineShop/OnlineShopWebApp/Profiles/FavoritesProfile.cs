using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Profiles
{
    public class FavoritesProfile : Profile
    {
        public FavoritesProfile()
        {
            CreateMap<Favorites, FavoritesViewModel>().ReverseMap();
        }
    }
}
