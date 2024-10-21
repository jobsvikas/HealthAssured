namespace Pricing.PricingRules
{
    public interface IPricingRule
    {
        int ProductId { get; }

        decimal DiscountPerUnit { get; }

        int MinQuantityToQualifyForDiscount { get; }
    }
}