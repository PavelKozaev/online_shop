using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Areas.Administrator.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IMapper mapper;
        private readonly ImagesProvider imagesProvider;

        public ProductsController(IProductsRepository productsRepository, IMapper mapper, ImagesProvider imagesProvider)
        {
            this.productsRepository = productsRepository;
            this.mapper = mapper;
            this.imagesProvider = imagesProvider;
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
                return RedirectToAction(nameof(Index));
            }

            return View(productViewModel);
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            await productsRepository.RemoveAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
