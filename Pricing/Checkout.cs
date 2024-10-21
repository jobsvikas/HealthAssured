using Pricing.Factory;
using Pricing.Models;

namespace Pricing
{
    public class Checkout : ICheckOut
    {
        private readonly List<OrderItem> _orderItems;
        private readonly IPricingFactory _pricingFactory;
        public Checkout(IPricingFactory pricingFactory)
        {
            _orderItems = new List<OrderItem>();
            _pricingFactory = pricingFactory;
        }

        public void Scan(Product product)
        {
            var existingOrderItem = _orderItems.FirstOrDefault(p => p.Product.ProductId == product.ProductId);

            if (existingOrderItem != null)
            {
                existingOrderItem.Add();
            }
            else
            {
                var pricingStrategy = _pricingFactory.Create(product.ProductId);
                _orderItems.Add(new OrderItem(product, pricingStrategy));
            }
        }

        public decimal GetTotalPrice()
        {
            decimal orderTotal = 0;
            foreach (var item in _orderItems)
            {
                orderTotal += item.GetPrice();
            }

            return orderTotal  ;
        }
    }
}
