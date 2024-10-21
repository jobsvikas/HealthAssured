using Pricing.Models;

namespace Pricing.Strategy
{
    public interface IPricingStrategy
    {
        decimal GetTotal(OrderItem item);
    }
}
