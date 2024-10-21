using FakeItEasy;
using Pricing.PricingRules;
using Pricing.Strategy;
using Xunit;

namespace PricingTest.PricingRules
{
    public class PricingRuleTests
    {
        [Theory]
        [InlineData(1, 5, 10)]
        [InlineData(3, 7, 11)]
        public void PricingRule_WhenReferenced_ThenItShouldReturnCorrectPricingRule(int productId, decimal discountPerUnit, int minQuantityToQualityForDiscount)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();

            //Act
            var sut = new PricingRule(productId, discountPerUnit, minQuantityToQualityForDiscount);

            //Assert
            Assert.Equal(sut.ProductId, productId);
            Assert.Equal(sut.DiscountPerUnit, discountPerUnit);
            Assert.Equal(sut.MinQuantityToQualifyForDiscount, minQuantityToQualityForDiscount);
        }

        [Theory]
        [InlineData(1, 5, 10)]
        [InlineData(3, 7, 11)]
        public void PricingRule_WhenReferenced_ThenItShouldReturnCorrectType(int productId, decimal discountPerUnit, int minQuantityToQualityForDiscount)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();

            //Act
            var sut = new PricingRule(productId, discountPerUnit, minQuantityToQualityForDiscount);

            //Assert
            Assert.IsType<int>(sut.ProductId);
            Assert.IsType<decimal>(sut.DiscountPerUnit);
            Assert.IsType<int>(sut.MinQuantityToQualifyForDiscount);
        }
    }
}
