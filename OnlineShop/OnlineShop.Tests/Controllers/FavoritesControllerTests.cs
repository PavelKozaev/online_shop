using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Controllers;
using OnlineShopWebApp.Models;

namespace OnlineShop.Tests.Controllers
{
    public class FavoritesControllerTests
    {
        private readonly Mock<IFavoritesRepository> mockFavoritesRepository;
        private readonly Mock<IProductsRepository> mockProductsRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly FavoritesController favoritesController;

        public FavoritesControllerTests()
        {
            mockFavoritesRepository = new Mock<IFavoritesRepository>();
            mockProductsRepository = new Mock<IProductsRepository>();
            mockMapper = new Mock<IMapper>();

            favoritesController = new FavoritesController(
                mockFavoritesRepository.Object,
                mockProductsRepository.Object,
                mockMapper.Object
            );
                        
            var context = new DefaultHttpContext();
            context.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "testuser@example.com")
            }));
            favoritesController.ControllerContext = new ControllerContext()
            {
                HttpContext = context,
            };
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithFavoritesViewModel()
        {
            // Arrange
            var favorites = new Favorites();
            var mappedFavoritesViewModel = new FavoritesViewModel();

            mockFavoritesRepository.Setup(r => r.TryGetByUserNameAsync("testuser@example.com")).ReturnsAsync(favorites);
            mockMapper.Setup(m => m.Map<FavoritesViewModel>(favorites)).Returns(mappedFavoritesViewModel);

            // Act
            var result = await favoritesController.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<FavoritesViewModel>(viewResult.Model);
            Assert.Equal(mappedFavoritesViewModel, model);
        }

        [Fact]
        public async Task AddToFavorites_ReturnsRedirectToIndex()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };

            mockProductsRepository.Setup(r => r.TryGetByIdAsync(productId)).ReturnsAsync(product);
            mockFavoritesRepository.Setup(r => r.AddAsync(product, "testuser@example.com")).Returns(Task.CompletedTask);

            // Act
            var result = await favoritesController.AddToFavorites(productId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(FavoritesController.Index), redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task RemoveFromFavorites_ReturnsRedirectToIndex()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };

            mockProductsRepository.Setup(r => r.TryGetByIdAsync(productId)).ReturnsAsync(product);
            mockFavoritesRepository.Setup(r => r.RemoveAsync(product, "testuser@example.com")).Returns(Task.CompletedTask);

            // Act
            var result = await favoritesController.RemoveFromFavorites(productId);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(FavoritesController.Index), redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Clear_ReturnsRedirectToIndex()
        {
            // Arrange

            mockFavoritesRepository.Setup(r => r.ClearAsync("testuser@example.com")).Returns(Task.CompletedTask);

            // Act
            var result = await favoritesController.Clear();

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(FavoritesController.Index), redirectToActionResult.ActionName);
        }
    }
}
