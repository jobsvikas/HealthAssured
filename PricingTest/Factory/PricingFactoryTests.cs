using FakeItEasy;
using Pricing.Factory;
using Pricing.Strategy;
using Pricing.Repository;
using Xunit;

namespace PricingTest.Factory
{
    public class PricingFactoryTests
    {
        public PricingFactoryTests()
        {
            
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        public void Create_WhenCalled_ThenShouldReturnCorrectType(int productId) 
        {
            //Arrange
            var pricingRuleRepositoryMock = A.Fake<IPricingRulesRepository>();
            var sut = new PricingFactory(pricingRuleRepositoryMock);

            //Act
            var strategy = sut.Create(productId);
            
            //Assert
            var exceptions = new List<Type>()
            {
                typeof(IPricingFactory),
                typeof(PricingFactory),
                typeof(DiscountedPricingStrategy),
            };
            Assert.Contains(strategy.GetType(),exceptions);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        public void Create_WhenCalledAndGetPriceRuleReturnsRule_ThenShouldReturnCorrectTypeOfNormalPricingStrategy(int productId)
        {
            //Arrange
            var pricingRuleRepositoryMock = A.Fake<IPricingRulesRepository>();
            var sut = new PricingFactory(pricingRuleRepositoryMock);
            A.CallTo(() => pricingRuleRepositoryMock.GetPricingRule(productId)).Returns(null);

            //Act
            var strategy = sut.Create(productId);

            //Assert
            Assert.IsType<NormalPricingStrategy>(strategy);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        public void Create_WhenCalledAndGetPriceRuleReturnsRule_ThenShouldReturnCorrectTypeOfDiscountedPricingStrategy(int productId)
        {
            //Arrange
            var pricingRuleRepositoryMock = A.Fake<IPricingRulesRepository>();
            var sut = new PricingFactory(pricingRuleRepositoryMock);
            A.CallTo(() => pricingRuleRepositoryMock.GetPricingRule(productId));

            //Act
            var strategy = sut.Create(productId);

            //Assert
            Assert.IsType<DiscountedPricingStrategy>(strategy);
        }
    }
}
