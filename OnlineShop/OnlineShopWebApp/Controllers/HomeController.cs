using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.ApiClients;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly ReviewsApiClient reviewsApiClient;
        private readonly IMapper mapper;

        public HomeController(IProductsRepository productsRepository, IMapper mapper, ReviewsApiClient reviewsApiClient)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
            this.reviewsApiClient = reviewsApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var products = await productsRepository.GetAllAsync();
            var productViewModels = new List<ProductViewModel>();

            foreach (var product in products)
            {
                var rating = await reviewsApiClient.GetRatingByProductIdAsync(product.Id);
                var productViewModel = product.ToProductViewModel();
                productViewModel.Rating = rating;
                productViewModels.Add(productViewModel);
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