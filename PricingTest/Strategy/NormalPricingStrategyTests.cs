using FakeItEasy;
using Pricing.Models;
using Pricing.Strategy;
using Xunit;

namespace PricingTest.Strategy
{
    public class NormalPricingStrategyTests
    {
        [Theory]
        [InlineData(1, "Apple", 1.0, 5, 6)]
        [InlineData(3, "Banana", 5.0, 6, 7)]
        public void GetTotal_WhenCalled_ThenItShouldPriceCorrectType(int productId, string productName, decimal UnitPrice, int quantity, int expectedQuantity)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();
            var product = new Product(productId, productName, UnitPrice);
            var orderItem = new OrderItem(product, pricingStrategyMock, quantity);

            //Act
            var sut = new NormalPricingStrategy();
            var result = sut.GetTotal(orderItem);

            //Assert
            Assert.IsType<Decimal>(result);
        }

        [Theory]
        [InlineData("Quantity : 5, Unit Price 1, So Gross = 5.",
                    1, "Apple", 1.0, 5, 5.0)]
        [InlineData("Quantity : 6, Unit Price 5, So Gross = 30.",
                    3, "Banana", 5.0, 6, 30)]
        public void GetTotal_WhenCalled_ShouldReturnCorrectValue(string description, int productId, string productName, decimal UnitPrice, int quantity, decimal expectedPrice)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();
            var product = new Product(productId, productName, UnitPrice);
            var orderItem = new OrderItem(product, pricingStrategyMock, quantity);

            //Act
            var sut = new NormalPricingStrategy();
            var result = sut.GetTotal(orderItem);

            //Assert
            Assert.Equal(expectedPrice, result);
        }
    }
}
