﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApp.Models;
using OnlineShopWebApp.Repositories.Interfaces;

namespace OnlineShopWebApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProductController : Controller
    {
        private readonly IProductsRepository productsRepository;


        public ProductController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }


        public IActionResult Index()
        {
            var products = productsRepository.GetAll();

            if (products == null)
            {
                return NotFound("Книги не найдены");
            }

            return View(products);
        }


        public IActionResult Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Неверный идентификатор книги");
            }

            var product = productsRepository.TryGetById(id);

            if (product == null)
            {
                return NotFound("Книга не найдена");
            }

            return View(product);
        }


        public IActionResult Add()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                productsRepository.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public IActionResult Edit(Guid id)
        {
            var product = productsRepository.TryGetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]
        public IActionResult Edit(Product product)
        {           

            if (ModelState.IsValid)
            {

                productsRepository.Edit(product);
                return RedirectToAction(nameof(Index));

            }
            return View(product);
        }
        

        public IActionResult Remove(Guid id)
        {
            productsRepository.Remove(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
