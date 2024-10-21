using Pricing.Models;

namespace Pricing.Strategy
{
    public class NormalPricingStrategy : IPricingStrategy
    {
        public NormalPricingStrategy()
        {
        }

        public virtual decimal GetTotal(OrderItem item)
        {
            return item.Quantity * item.Product.UnitPrice;
        }
    }
}
