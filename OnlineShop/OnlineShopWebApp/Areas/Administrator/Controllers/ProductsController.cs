using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.ApiClients;
using OnlineShopWebApp.Areas.Administrator.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Redis;
using Serilog;
using System.Text.Json;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(OnlineShop.Db.Constants.AdminRoleName)]
    [Authorize(Roles = (OnlineShop.Db.Constants.AdminRoleName))]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;
        private readonly ImagesProvider imagesProvider;
        private readonly IRedisCacheService redisCacheService;
        private readonly IReviewsApiClient reviewsApiClient;

        public ProductsController(IProductsRepository productsRepository, IMapper mapper, ImagesProvider imagesProvider, IRedisCacheService redisCacheService, IReviewsApiClient reviewsApiClient)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
            this.imagesProvider = imagesProvider;
            this.redisCacheService = redisCacheService;
            this.reviewsApiClient = reviewsApiClient;
        }


        public async Task<IActionResult> Index()
        {
            var products = await productsRepository.GetAllAsync();
            return View(products.ToProductViewModels());
        }


        public async Task<IActionResult> Details(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            return View(product.ToProductViewModel());
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(AddProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var imagesPaths = imagesProvider.SaveFiles(productViewModel.UploadedFiles, ImageFolders.Products);
                await productsRepository.AddAsync(productViewModel.ToProduct(imagesPaths));
                await RemoveCacheAsync();
                await UpdateCacheAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productViewModel);
        }


        public async Task<IActionResult> Edit(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            return View(product.ToEditProductViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var addedImagesPaths = imagesProvider.SaveFiles(productViewModel.UploadedFiles, ImageFolders.Products);
                productViewModel.ImagesPaths = addedImagesPaths;
                await productsRepository.EditAsync(productViewModel.ToProduct());
                await RemoveCacheAsync();
                await UpdateCacheAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(productViewModel);
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            await productsRepository.RemoveAsync(id);
            await RemoveCacheAsync();
            await UpdateCacheAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task UpdateCacheAsync()
        {
            try
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

                var productsJson = JsonSerializer.Serialize(productViewModels);
                await redisCacheService.SetAsync(OnlineShop.Db.Constants.RedisCacheKey, productsJson);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка обновления кеша Redis");
            }
        }

        private async Task RemoveCacheAsync()
        {
            try
            {
                await redisCacheService.RemoveAsync(OnlineShop.Db.Constants.RedisCacheKey);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка удаления кеша Redis");
            }
        }
    }
}