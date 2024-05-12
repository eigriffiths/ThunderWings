using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThunderWings.Core.Services;
using ThunderWings.Core.Services.Interfaces;
using ThunderWings.Repo.Models;

namespace ThunderWings.Tests.Checkout
{
    public class CheckoutServiceTests
    {
        private readonly Mock<IBasketService> _basketServiceMock;
        private readonly ICheckoutService _checkoutService;

        public CheckoutServiceTests()
        {
            _basketServiceMock = new Mock<IBasketService>();
            _checkoutService = new CheckoutService(_basketServiceMock.Object);
        }

        [Fact]
        public void ValidateBasket_ReturnsBasket_WhenBasketExistsAndIsNotEmpty()
        {
            // Arrange
            var testBasket = new Basket { Items = new List<BasketItem> { new BasketItem() } };

            _basketServiceMock.Setup(service => service.GetPersistedBasket(It.IsAny<int>()))
                .ReturnsAsync(testBasket);

            // Act
            var result = _checkoutService.ValidateBasket(1);

            // Assert
            Assert.Equal(testBasket, result);
        }

        [Fact]
        public void ValidateBasket_ThrowsException_WhenBasketDoesNotExist()
        {
            // Arrange
            _basketServiceMock.Setup(service => service.GetPersistedBasket(It.IsAny<int>()))
                .ReturnsAsync((Basket)null);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _checkoutService.ValidateBasket(1));

            Assert.Equal("This basket is empty or cannot be found.", exception.Message);
        }

        [Fact]
        public void ValidateBasket_ThrowsException_WhenBasketIsEmpty()
        {
            // Arrange
            var emptyBasket = new Basket { Items = new List<BasketItem>() };

            _basketServiceMock.Setup(service => service.GetPersistedBasket(It.IsAny<int>()))
                .ReturnsAsync(emptyBasket);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _checkoutService.ValidateBasket(1));

            Assert.Equal("This basket is empty or cannot be found.", exception.Message);
        }

        [Fact]
        public void CalculateBasketTotal_CalculatesTotalCorrectly()
        {
            // Arrange
            var testBasket = new Basket
            {
                Items = new List<BasketItem>
            {
                new BasketItem { Quantity = 2, Price = 50 },
                new BasketItem { Quantity = 1, Price = 100 }
            }
            };

            // Act
            var result = _checkoutService.CalculateBasketTotal(testBasket);

            // Assert
            Assert.Equal(200, result.Total);
        }

        [Fact]
        public void GenerateReceipt_GeneratesReceiptCorrectly()
        {
            // Arrange
            var testBasket = new Basket { Total = 200 };

            // Act
            var result = _checkoutService.GenerateReceipt(testBasket);

            // Assert
            Assert.Equal("Thank you for your purchase.", result.Description);
            Assert.Equal(200, result.Total);
        }
    }
}
