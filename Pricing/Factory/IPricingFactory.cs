using Pricing.Strategy;

namespace Pricing.Factory
{
    public interface IPricingFactory
    {
        IPricingStrategy Create(int productId);
    }

}
