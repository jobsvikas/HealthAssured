
using FakeItEasy;
using Pricing.Factory;
using Pricing;
using Pricing.Models;
using Pricing.Strategy;
using Pricing.PricingRules;
using PricingTest.Helpers;
using Xunit;

namespace PricingTest
{
    public class CheckoutTests
    {

        private readonly IPricingFactory pricingFactory;

        public CheckoutTests()
        {
        }

        [Theory]
        [InlineData(1, "Apple", 0.5, 0.5)]
        public void Checkout_WhenGetTotalPriceIsCalled_ThenGetTotalRetunsTypeOfDecimal(int productId, string productName, decimal unitPrice, decimal expectedPrice)
        {
            //Arrange
            var pricingFactoryMock = A.Fake<IPricingFactory>();
            var normalPricingStrategy = new NormalPricingStrategy();
            A.CallTo(() => pricingFactoryMock.Create(A<int>.Ignored)).Returns(normalPricingStrategy);
            var _checkout = new Checkout(pricingFactoryMock);
            var _product = new Product(productId, productName, unitPrice);

            //Act            
            _checkout.Scan(_product);
            var price = _checkout.GetTotalPrice();

            //Assert
            Assert.IsType<decimal>(price);
        }

        /*
        When NORMAL Pricing is applied.
        Product
        ["1_Apple_0.5"]= ProductId 1, Apple, 0.5P a piece
        ["5_Grapes_2.9"]= ProductId 5, Grapes, 2.9£ a pack
         */

        [Theory]
        [InlineData(true, 0.5, "1_Apple_0.5")]
        [InlineData(true, 1.0, "1_Apple_0.5", "1_Apple_0.5")]
        [InlineData(true, 3.5, "1_Apple_0.5", "1_Apple_0.5", "2_Banana_0.5", "3_PineApple_2.0")]
        [InlineData(true, 4.4, "1_Apple_0.5", "1_Apple_0.5", "2_Banana_0.5", "5_Grapes_2.9")]
        public void Checkout_WhenNoOffersAndItemIsScanned_WithNormalPricing_ThenGetTotalRetunsCorrectValues2(bool useNormalPricing, decimal expectedPrice, params string[] products)
        {
            //Arrange
            IList<Product> _products = new List<Product>();
            ProductHelper.GetProducts(products, _products);

            var pricingFactoryMock = A.Fake<IPricingFactory>();
            var pricingRuleMock = A.Fake<IPricingRule>();
            var pricingStrategy = useNormalPricing ? new NormalPricingStrategy() : new DiscountedPricingStrategy(pricingRuleMock);
            A.CallTo(() => pricingFactoryMock.Create(A<int>.Ignored)).Returns(pricingStrategy);
            var _checkout = new Checkout(pricingFactoryMock);

            //Act            
            foreach (var item in _products)
            {
                _checkout.Scan(item);
            }
            var price = _checkout.GetTotalPrice();

            //Assert
            Assert.Equal(price, expectedPrice);
        }


        /*
        When Discounted Pricing is applied.
        Rule 
        ["1_0.25_5"] = Product Id 1, 0.25P discount, with min qty of 5
        ["2_1.5_1"] = Product Id 1, 1.50P discount, with min qty of 1
        Product
        ["1_Apple_0.5"]= ProductId 1, Apple, 0.5P a piece
        ["5_Grapes_2.9"]= ProductId 5, Grapes, 2.9£ a pack
         */
        [Theory]
        [InlineData("Discount Should be applied ## 0.5 X 5 = 2.5 - discount(0.25 X 5)= 1.25, Expected Price Should be = 2.5 - 1.25==> 1.25",
                    false, 1.25, "1_0.25_5", "1_Apple_0.5", "1_Apple_0.5", "1_Apple_0.5", "1_Apple_0.5", "1_Apple_0.5")]

        [InlineData("Discount Should be applied ## 0.1 X 6 = 0.60 - discount(0.10 X 6)= 0.60, Expected Price Should be = (29*5) - 0.50==>0.95",
                    false, 0.95, "1_0.1_5", "1_Apple_0.29", "1_Apple_0.29", "1_Apple_0.29", "1_Apple_0.29", "1_Apple_0.29")]

        [InlineData("No Discount Should be applied ## 0.1 X 6 = 0.60 - discount(0.0 X 6)= 0.0, Expected Price Should be = (29*5) - 0.00==>1.45",
                    true, 1.45, "1_0.1_5", "1_Apple_0.29", "1_Apple_0.29", "1_Apple_0.29", "1_Apple_0.29", "1_Apple_0.29")]
        public void Checkout_WhenNoOffersAndItemIsScanned_WithDiscountedPricing_ThenGetTotalRetunsCorrectValues2(string testDescription, bool useNormalPricing, decimal expectedPrice, string pricingRule, params string[] products)
        {
            //Arrange
            IList<Product> _products = new List<Product>();
            ProductHelper.GetProducts(products, _products);
            var pricingFactoryMock = A.Fake<IPricingFactory>();
            var pricingRuleMock = A.Fake<IPricingRule>();

            A.CallTo(() => pricingRuleMock.ProductId).Returns(int.Parse(pricingRule.Split('_')[0]));
            A.CallTo(() => pricingRuleMock.DiscountPerUnit).Returns(decimal.Parse(pricingRule.Split('_')[1]));
            A.CallTo(() => pricingRuleMock.MinQuantityToQualifyForDiscount).Returns(int.Parse(pricingRule.Split('_')[2]));

            var pricingStrategy = useNormalPricing ? new NormalPricingStrategy() : new DiscountedPricingStrategy(pricingRuleMock);
            A.CallTo(() => pricingFactoryMock.Create(A<int>.Ignored)).Returns(pricingStrategy);
            var _checkout = new Checkout(pricingFactoryMock);

            //Act            
            foreach (var item in _products)
            {
                _checkout.Scan(item);
            }
            var price = _checkout.GetTotalPrice();

            //Assert
            Assert.Equal(price, expectedPrice);
        }
    }
}