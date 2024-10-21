using Pricing.PricingRules;

namespace Pricing.Repository
{
    public class PricingRulesRepository : IPricingRulesRepository
    {
        public PricingRulesRepository(List<IPricingRule> rules)
        {
            Rules = rules;
        }

        public List<IPricingRule> Rules { get; }

        public IPricingRule GetPricingRule(int ProductId)
        {
            return Rules.FirstOrDefault(p => p.ProductId == ProductId);
        }
    }
}