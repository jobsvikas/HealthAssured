using Pricing.Repository;
using Pricing.Strategy;

namespace Pricing.Factory
{
    public class PricingFactory : IPricingFactory
    {
        private readonly IPricingRulesRepository _repository;
        public PricingFactory(IPricingRulesRepository pricingRules)
        {
            _repository = pricingRules;
        }

        public IPricingStrategy Create(int productId)
        {
            var rule =  this._repository.GetPricingRule(productId);

            if (rule == null)
            {
                return new NormalPricingStrategy();
            }

            return new DiscountedPricingStrategy(rule);
        }
    }
}
