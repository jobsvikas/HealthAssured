using Pricing.PricingRules;

namespace Pricing.Repository
{
    public interface IPricingRulesRepository
    {
        IPricingRule GetPricingRule(int ProductId);
    }
}
