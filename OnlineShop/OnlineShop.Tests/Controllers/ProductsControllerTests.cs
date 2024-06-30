using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.ApiClients;
using OnlineShopWebApp.ApiModels;
using OnlineShopWebApp.Controllers;
using OnlineShopWebApp.Models;

namespace OnlineShop.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private readonly Mock<IProductsRepository> mockProductsRepository;
        private readonly Mock<IReviewsApiClient> mockReviewsApiClient;
        private readonly Mock<UserManager<User>> mockUserManager;
        private readonly ProductsController productsController;

        public ProductsControllerTests()
        {
            mockProductsRepository = new Mock<IProductsRepository>();
            mockReviewsApiClient = new Mock<IReviewsApiClient>();
            mockUserManager = new Mock<UserManager<User>>(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);

            productsController = new ProductsController(
                mockProductsRepository.Object,
                mockReviewsApiClient.Object,
                mockUserManager.Object
            );
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithProductViewModel()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Убить отца" };
            var reviews = new List<ReviewApiModel> { new ReviewApiModel { Id = Guid.NewGuid(), ProductId = productId, Text = "Тестовый отзыв", Grade = 5 } };
            var rating = new RatingApiModel { ProductId = productId, AverageGrade = 4.5, ReviewsCount = 1 };

            mockProductsRepository.Setup(r => r.TryGetByIdAsync(productId)).ReturnsAsync(product);
            mockReviewsApiClient.Setup(r => r.GetByProductIdAsync(productId)).ReturnsAsync(reviews);
            mockReviewsApiClient.Setup(r => r.GetRatingByProductIdAsync(productId)).ReturnsAsync(rating);

            // Act
            var result = await productsController.Index(productId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(viewResult.Model);
            Assert.Equal(product.Id, model.Id);
            Assert.Equal(reviews.Count, model.Reviews.Count);
            Assert.Equal(rating.AverageGrade, model.Rating.AverageGrade);
        }

        [Fact]
        public async Task AddReview_ReturnsUnauthorized_WhenUserIsNull()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var viewModel = new ProductViewModel();

            mockUserManager.Setup(u => u.GetUserAsync(null)).ReturnsAsync((User)null);

            // Act
            var result = await productsController.AddReview(productId, "Тестовый отзыв", 5);

            // Assert
            var unauthorizedResult = Assert.IsType<UnauthorizedResult>(result);
            Assert.Equal(StatusCodes.Status401Unauthorized, unauthorizedResult.StatusCode);
        }

        [Fact]
        public async Task AddReview_ReturnsRedirectToDetails_WhenUserIdIsInvalid()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var currentUser = new User { Id = "invalidUserId" };

            mockUserManager.Setup(u => u.GetUserAsync(null)).ReturnsAsync(currentUser);

            // Act
            var result = await productsController.AddReview(productId, "Тестовое описание", 5);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Details", redirectToActionResult.ActionName);
            Assert.Equal(productId, redirectToActionResult.RouteValues["id"]);
        }
    }
}
