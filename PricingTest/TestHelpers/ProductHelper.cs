using Pricing.Models;

namespace PricingTest.Helpers
{
    public static class ProductHelper
    {
        public static void GetProducts(string[] products, IList<Product> _products)
        {
            foreach (var item in products)
            {
                var a = new Product(int.Parse(item.Split('_')[0]), item.Split('_')[1], decimal.Parse(item.Split('_')[2]));
                _products.Add(a);
            }
        }
    }
}
