namespace Pricing.PricingRules
{
    public class PricingRule : IPricingRule
    {
        public PricingRule(int productId, decimal discountPerUnit, int minQuantityToQualityForDiscount)
        {
            ProductId = productId;
            DiscountPerUnit = discountPerUnit;
            MinQuantityToQualifyForDiscount = minQuantityToQualityForDiscount;
        }

        public int ProductId { get; }

        public decimal DiscountPerUnit { get; }

        public int MinQuantityToQualifyForDiscount { get; }
    }
}