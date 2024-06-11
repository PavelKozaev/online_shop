using System.Diagnostics;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.ApiClients;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Redis;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly ReviewsApiClient reviewsApiClient;
        private readonly IMapper mapper;
        private readonly RedisCacheService redisCacheService;

        public HomeController(IProductsRepository productsRepository, IMapper mapper, ReviewsApiClient reviewsApiClient, RedisCacheService redisCacheService)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
            this.reviewsApiClient = reviewsApiClient;
            this.redisCacheService = redisCacheService;
        }

        public async Task<IActionResult> Index()
        {
            string cacheKey = "products_list";
            var cachedProducts = await redisCacheService.GetAsync(Constants.redisCacheKey);

            List<ProductViewModel> productViewModels;

            if (!string.IsNullOrEmpty(cachedProducts))
            {
                productViewModels = JsonSerializer.Deserialize<List<ProductViewModel>>(cachedProducts);
            }
            else
            {
                var products = await productsRepository.GetAllAsync();
                productViewModels = new List<ProductViewModel>();

                foreach (var product in products) 
                {
                    var rating = await reviewsApiClient.GetRatingByProductIdAsync(product.Id);
                    var productViewModel = product.ToProductViewModel();
                    productViewModel.Rating = rating;
                    productViewModels.Add(productViewModel);
                }

                var productsJson = JsonSerializer.Serialize(productViewModels);
                await redisCacheService.SetAsync(Constants.redisCacheKey, productsJson);
            }

            return View(productViewModels);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                var products = await productsRepository.GetAllAsync();
                var findProducts = products.Where(product => product.Name.ToLower().Contains(name.ToLower())).ToList();
                return View(mapper.Map<List<ProductViewModel>>(findProducts));
            }

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}