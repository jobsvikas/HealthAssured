using FakeItEasy;
using Pricing.PricingRules;
using Pricing.Repository;
using Xunit;

namespace PricingTest.Repository
{
    public class PricingRulesRepositoryTests
    {

        [Theory]
        [InlineData(1, "Apple", 1.0, 5)]
        [InlineData(3, "Banana", 5.0, 6)]
        public void GetPricing_WhenCalled_ThenItShouldReturnCorrectPricingRule(int productId, string productName, decimal UnitPrice, int quantity)
        {
            //Arrange
            var pricingRulesMock = A.Fake<IPricingRule>();
            A.CallTo(() => pricingRulesMock.ProductId).Returns(productId);

            //Act
            var pricingRule = new List<IPricingRule>() { pricingRulesMock } ;
            var sut = new PricingRulesRepository(pricingRule);

            //Assert
            Assert.NotNull(sut.GetPricingRule(productId));
        }

        [Theory]
        [InlineData(1, "Apple", 1.0, 5)]
        [InlineData(3, "Banana", 5.0, 6)]
        public void GetPricing_WhenCalled_ThenItShouldReturnNull(int productId, string productName, decimal UnitPrice, int quantity)
        {
            //Arrange
            var pricingRulesMock = A.Fake<IPricingRule>();
            A.CallTo(() => pricingRulesMock.ProductId).Returns(productId);

            //Act
            var pricingRule = new List<IPricingRule>() { pricingRulesMock };
            var sut = new PricingRulesRepository(pricingRule);

            //Assert
            //calling Getpricing with incorrect productId should return null
            Assert.Null(sut.GetPricingRule(productId+1));
        }
    }
}