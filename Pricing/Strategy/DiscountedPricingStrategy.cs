using Pricing.Models;
using Pricing.PricingRules;

namespace Pricing.Strategy
{
    public class DiscountedPricingStrategy : NormalPricingStrategy,  IPricingStrategy
    {
        private readonly IPricingRule _rule;

        public DiscountedPricingStrategy(IPricingRule rule)
        {
            this._rule = rule;
        }

        public override decimal GetTotal(OrderItem item)
        {
            var total = base.GetTotal(item);

            if (item.Quantity >= this._rule.MinQuantityToQualifyForDiscount)
            {
                total = total - (item.Quantity * this._rule.DiscountPerUnit);    
            }

            return total;
        }
    }
}
