using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.ApiClients;
using OnlineShopWebApp.Helpers;

namespace OnlineShopWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsRepository productsRepository;
        private readonly IReviewsApiClient reviewsApiClient;
        private readonly UserManager<User> userManager;

        public ProductsController(IProductsRepository productsRepository, IReviewsApiClient reviewsApiClient, UserManager<User> userManager)
        {
            this.productsRepository = productsRepository;
            this.reviewsApiClient = reviewsApiClient;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            var reviews = await reviewsApiClient.GetByProductIdAsync(id);
            var rating = await reviewsApiClient.GetRatingByProductIdAsync(id);
            var productViewModel = product.ToProductViewModel();
            productViewModel.Reviews = reviews;
            productViewModel.Rating = rating;
            return View(productViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddReview(Guid productId, string text, int grade)
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            if (!Guid.TryParse(currentUser.Id, out Guid userId))
            {
                ModelState.AddModelError("", "Произошла ошибка при идентификации пользователя.");
                return RedirectToAction("Details", new { id = productId });
            }

            try
            {
                await reviewsApiClient.CreateAsync(productId, userId, text, grade);
                return RedirectToAction("Index", new { id = productId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Произошла ошибка при добавлении отзыва: " + ex.Message);
                return RedirectToAction("Details", new { id = productId });
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteReview(Guid reviewId, Guid productId)
        {
            var currentUser = await userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            if (!Guid.TryParse(currentUser.Id, out Guid userId))
            {
                ModelState.AddModelError("", "Произошла ошибка при идентификации пользователя.");
                return RedirectToAction("Index", new { id = productId });
            }

            try
            {
                var result = await reviewsApiClient.DeleteAsync(reviewId);
                if (!result)
                {
                    ModelState.AddModelError("", "Не удалось удалить отзыв.");
                    return RedirectToAction("Index", new { id = productId });
                }

                return RedirectToAction("Index", new { id = productId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Произошла ошибка при удалении отзыва: " + ex.Message);
                return RedirectToAction("Index", new { id = productId });
            }
        }
    }
}