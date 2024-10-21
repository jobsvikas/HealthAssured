using FakeItEasy;
using Pricing.Models;
using Pricing.Strategy;
using Xunit;

namespace PricingTest.Models
{
    public class ProductTests
    {
        [Theory]
        [InlineData(1, "Apple", 1.0)]
        [InlineData(3, "Banana", 5.0)]
        public void Product_WhenReferenced_ThenItShouldReturnCorrectProduct(int productId, string productName, decimal UnitPrice) {

            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();
            var product = new Product(productId, productName, UnitPrice);
            
            //Act
            var sut = new OrderItem(product, pricingStrategyMock);

            //Assert
            Assert.Equal(sut.Product, product);
        }

        [Theory]
        [InlineData(1, "Apple", 1.0)]
        [InlineData(3, "Banana", 5.0)]
        public void Product_WhenReferenced_ThenItShouldReturnCorrectProductType(int productId, string productName, decimal UnitPrice)
        {
            //Arrange
            var pricingStrategyMock = A.Fake<IPricingStrategy>();

            //Act
            var sut = new OrderItem(new Product(productId,productName,UnitPrice), pricingStrategyMock);

            //Assert
            Assert.IsType<Product>(sut.Product);
        }
    }
}
