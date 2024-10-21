using FakeItEasy;
using Pricing.Models;
using Pricing.PricingRules;
using Pricing.Strategy;
using Xunit;

namespace PricingTest.Strategy
{
    public class DiscountedPricingStrategyTests
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

            var pricingRulesMock = A.Fake<IPricingRule>();
            A.CallTo(() => pricingRulesMock.ProductId).Returns(productId);

            //Act
            var sut = new DiscountedPricingStrategy(pricingRulesMock);
            var result= sut.GetTotal(orderItem);

            //Assert
            Assert.IsType<Decimal>(result);
        }
    }
}
