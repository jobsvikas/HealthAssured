using FakeItEasy;
using Pricing.Models;
using Pricing.Strategy;
using Xunit;

namespace PricingTest.Models
{
    public class OrderItemTests
    {
        [Theory]
        [InlineData(1, "Apple", 1.0, 5)]
        [InlineData(3, "Banana", 5.0, 6)]
        public void OrderItem_WhenReferenced_ThenItShouldReturnCorrectProduct(int productId, string productName, decimal UnitPrice, int quantity)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();
            var product = new Product(productId, productName, UnitPrice);
            var orderItem = new OrderItem(product, pricingStrategyMock, quantity);

            //Act
            var sut = new OrderItem(product, pricingStrategyMock, quantity);

            //Assert
            Assert.Equal(sut.Product, product);
            Assert.Equal(sut.Quantity, quantity);
        }

        [Theory]
        [InlineData(1, "Apple", 1.0, 5, 6)]
        [InlineData(3, "Banana", 5.0, 6, 7)]
        public void Add_WhenCalled_ThenItShouldIncrementTheValueOfQuantity(int productId, string productName, decimal UnitPrice, int quantity, int expectedQuantity)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();
            var product = new Product(productId, productName, UnitPrice);
            var orderItem = new OrderItem(product, pricingStrategyMock, quantity);

            //Act
            var sut = new OrderItem(product, pricingStrategyMock, quantity);
            sut.Add();

            //Assert
            Assert.Equal(sut.Quantity, expectedQuantity);
        }

        [Theory]
        [InlineData(1, "Apple", 1.0, 5, 6)]
        [InlineData(3, "Banana", 5.0, 6, 7)]
        public void GetPrice_WhenCalled_ThenItShouldPriceStratgyGetTotalOnce(int productId, string productName, decimal UnitPrice, int quantity, int expectedQuantity)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();
            var product = new Product(productId, productName, UnitPrice);
            var orderItem = new OrderItem(product, pricingStrategyMock, quantity);

            //Act
            var sut = new OrderItem(product, pricingStrategyMock, quantity);
            sut.GetPrice();

            //Assert
            var a = A.CallTo(() => pricingStrategyMock.GetTotal(A<OrderItem>.Ignored)).MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData(1, "Apple", 1.0, 5)]
        [InlineData(3, "Banana", 5.0, 6)]
        public void GetPrice_WhenCalled_ThenItShouldReturnCorrectType(int productId, string productName, decimal UnitPrice, int quantity)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();
            var product = new Product(productId, productName, UnitPrice);
            var orderItem = new OrderItem(product, pricingStrategyMock, quantity);

            //Act
            var sut = new OrderItem(product, pricingStrategyMock, quantity);
            var result= sut.GetPrice();

            //Assert
            Assert.IsType<decimal>(result);
        }
    }
}
