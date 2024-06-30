using System.Diagnostics;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Redis;
using OnlineShopWebApp.ReviewMicroservice.ApiClients;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IReviewsApiClient reviewsApiClient;
        private readonly IMapper mapper;
        private readonly IRedisCacheService redisCacheService;

        public HomeController(IProductsRepository productsRepository, IMapper mapper, IReviewsApiClient reviewsApiClient, IRedisCacheService redisCacheService)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
            this.reviewsApiClient = reviewsApiClient;
            this.redisCacheService = redisCacheService;
        }

        public async Task<IActionResult> Index()
        {            
            var cachedProducts = await redisCacheService.TryGetAsync(OnlineShop.Db.Constants.RedisCacheKey);

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
                await redisCacheService.SetAsync(OnlineShop.Db.Constants.RedisCacheKey, productsJson);
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