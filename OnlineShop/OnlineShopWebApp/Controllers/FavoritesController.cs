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

        public async Task<IActionResult> Index()
        {
            var favorites = await favoritesRepository.TryGetByUserNameAsync(User.Identity.Name);
            return View(mapper.Map<FavoritesViewModel>(favorites));
        }

        public async Task<IActionResult> AddToFavorites(Guid productId)
        {
            var product = await productsRepository.TryGetByIdAsync(productId);
            await favoritesRepository.AddAsync(product, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveFromFavorites(Guid productId)
        {
            var product = await productsRepository.TryGetByIdAsync(productId);
            await favoritesRepository.RemoveAsync(product, User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Clear()
        {
            await favoritesRepository.ClearAsync(User.Identity.Name);
            return RedirectToAction(nameof(Index));
        }
    }
}