using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Areas.Administrator.Models;

namespace OnlineShopWebApp.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleViewModel>().ReverseMap();
        }
    }
}
