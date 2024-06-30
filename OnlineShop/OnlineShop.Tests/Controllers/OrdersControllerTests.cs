using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineShop.Db.Models;
using OnlineShop.Db.Repositories.Interfaces;
using OnlineShopWebApp.Controllers;
using OnlineShopWebApp.Models;
using System.Security.Claims;

namespace OnlineShop.Tests.Controllers
{
    public class OrdersControllerTests
    {
        private readonly Mock<ICartsRepository> mockCartsRepository;
        private readonly Mock<IOrdersRepository> mockOrdersRepository;
        private readonly Mock<IMapper> mockMapper;
        private readonly OrdersController ordersController;

        public OrdersControllerTests()
        {
            mockCartsRepository = new Mock<ICartsRepository>();
            mockOrdersRepository = new Mock<IOrdersRepository>();
            mockMapper = new Mock<IMapper>();

            ordersController = new OrdersController(
                mockCartsRepository.Object,
                mockOrdersRepository.Object,
                mockMapper.Object
            );
                        
            var context = new DefaultHttpContext();
            context.User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "testuser@example.com")                
            }));
            ordersController.ControllerContext = new ControllerContext()
            {
                HttpContext = context,
            };
        }

        [Fact]
        public async Task Buy_RedirectsToThankYouPage()
        {
            // Arrange
            var userViewModel = new UserDeliveryInfoViewModel();
            var existingCart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { Id = Guid.NewGuid(), Product = new Product(), Amount = 1 },
                    new CartItem { Id = Guid.NewGuid(), Product = new Product(), Amount = 2 }
                }
            };

            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };

            mockCartsRepository.Setup(r => r.TryGetByUserNameAsync("testuser@example.com")).ReturnsAsync(existingCart);
            mockMapper.Setup(m => m.Map<UserDeliveryInfo>(userViewModel)).Returns(new UserDeliveryInfo());
            mockOrdersRepository.Setup(r => r.AddAsync(It.IsAny<Order>())).Callback<Order>(o => { o.Id = orderId; }).Returns(Task.CompletedTask);
            mockCartsRepository.Setup(r => r.ClearAsync("testuser@example.com")).Returns(Task.CompletedTask);

            // Act
            var result = await ordersController.Buy(userViewModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal(nameof(OrdersController.ThankYouPage), redirectToActionResult.ActionName);
            Assert.Equal(orderId, redirectToActionResult.RouteValues["id"]);
        }

        [Fact]
        public async Task ThankYouPage_ReturnsViewWithOrderViewModel()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var order = new Order { Id = orderId };

            mockOrdersRepository.Setup(r => r.TryGetByIdAsync(orderId)).ReturnsAsync(order);
            mockMapper.Setup(m => m.Map<OrderViewModel>(order)).Returns(new OrderViewModel { Id = orderId }); 

            // Act
            var result = await ordersController.ThankYouPage(orderId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<OrderViewModel>(viewResult.Model);
            Assert.Equal(orderId, model.Id);
        }
    }
}
