using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
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
            var existingCompareProduts = favoritesRepository.GetAll(Constants.UserId);
            var response = mapper.Map<List<FavoritesViewModel>>(existingCompareProduts);
            return View(response);
        }

        public IActionResult AddToFavorites(Guid productId)
        {
            var product = productsRepository.TryGetById(productId);
            favoritesRepository.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromFavorites(Guid productId)
        {
            var product = productsRepository.TryGetById(productId);
            favoritesRepository.Remove(product, Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}