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
            var result = sut.GetTotal(orderItem);

            //Assert
            Assert.IsType<Decimal>(result);
        }

        [Theory]
        [InlineData(1, "Apple", 1.0, 5, 6)]
        [InlineData(3, "Banana", 5.0, 6, 7)]
        public void GetTotal_WhenCalled_ThenPricingRuleMockMinQualifyForDiscountShouldBeReferencedOnce(int productId, string productName, decimal UnitPrice, int quantity, int expectedQuantity)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();
            var product = new Product(productId, productName, UnitPrice);
            var orderItem = new OrderItem(product, pricingStrategyMock, quantity);

            var pricingRulesMock = A.Fake<IPricingRule>();
            A.CallTo(() => pricingRulesMock.ProductId).Returns(productId);

            //Act
            var sut = new DiscountedPricingStrategy(pricingRulesMock);
            var result = sut.GetTotal(orderItem);

            //Assert
            A.CallTo(() => pricingRulesMock.MinQuantityToQualifyForDiscount).MustHaveHappenedOnceExactly();
        }

        [Theory]
        [InlineData("Quantity : 5, Unit Price 1, So Gross = 5. Discount per unit - 0.25.  [5 X 1 = 5 - (5 X 0.25 = 1.25 ) ==> 3.75]",
                    1, "Apple", 1.0, 5, 3, 0.25, 3.75)]
        [InlineData("Quantity : 6, Unit Price 5, So Gross = 30. Discount per unit - 0.5.  [6 X 5 = 30 - (6 X .5 = 3.0 ) ==> 27]",
                    3, "Banana", 5.0, 6, 3, 0.5, 27)]
        public void GetTotal_WhenCalled_ShouldReturnCorrectValue(string description, int productId, string productName, decimal UnitPrice, int quantity, int minQuantityToQualifyForDiscount, decimal discountPerUnit, decimal expectedPrice)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();
            var product = new Product(productId, productName, UnitPrice);
            var orderItem = new OrderItem(product, pricingStrategyMock, quantity);

            var pricingRulesMock = A.Fake<IPricingRule>();
            A.CallTo(() => pricingRulesMock.ProductId).Returns(productId);
            A.CallTo(() => pricingRulesMock.MinQuantityToQualifyForDiscount).Returns(minQuantityToQualifyForDiscount);
            A.CallTo(() => pricingRulesMock.DiscountPerUnit).Returns(discountPerUnit);

            //Act
            var sut = new DiscountedPricingStrategy(pricingRulesMock);
            var result = sut.GetTotal(orderItem);

            //Assert
            Assert.Equal(expectedPrice, result);
        }
    }
}

