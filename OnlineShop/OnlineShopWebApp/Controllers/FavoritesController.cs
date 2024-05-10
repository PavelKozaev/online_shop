using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoritesRepository favoritesRepository;
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;

        public FavoritesController(IFavoritesRepository favoritesRepository, IProductsRepository productsRepository, IMapper mapper)
        {
            this.favoritesRepository = favoritesRepository;
            this.productsRepository = productsRepository;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            var favorites = favoritesRepository.TryGetByUserName(User.Identity.Name);
            return View(mapper.Map<FavoritesViewModel>(favorites));
        }

        public IActionResult AddToFavorites(Guid productId)
        {
            var product = productsRepository.TryGetById(productId);
            favoritesRepository.Add(product, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RemoveFromFavorites(Guid productId)
        {
            var product = productsRepository.TryGetById(productId);
            favoritesRepository.Remove(product, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Clear()
        {
            favoritesRepository.Clear(User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}