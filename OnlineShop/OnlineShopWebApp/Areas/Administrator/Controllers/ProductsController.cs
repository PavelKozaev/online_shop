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


        public IActionResult Index()
        {
            var products = productsRepository.GetAll();
            return View(products.ToProductViewModels());
        }


        public IActionResult Details(Guid id)
        {
            var product = productsRepository.TryGetById(id);
            return View(product.ToProductViewModel());
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(AddProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var imagesPaths = imagesProvider.SaveFiles(productViewModel.UploadedFiles, ImageFolders.Products);
                productsRepository.Add(productViewModel.ToProduct(imagesPaths));
                return RedirectToAction(nameof(Index));
            }

            return View(productViewModel);
        }


        public IActionResult Edit(Guid id)
        {
            var product = productsRepository.TryGetById(id);
            return View(product.ToEditProductViewModel());
        }


        [HttpPost]
        public IActionResult Edit(EditProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var addedImagesPaths = imagesProvider.SaveFiles(productViewModel.UploadedFiles, ImageFolders.Products);
                productViewModel.ImagesPaths = addedImagesPaths;  
                productsRepository.Edit(productViewModel.ToProduct());
                return RedirectToAction(nameof(Index));
            }

            return View(productViewModel);
        }
        

        public IActionResult Delete(Guid id)
        {
            productsRepository.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
