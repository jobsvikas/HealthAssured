using Pricing.Strategy;

namespace Pricing.Models
{
    public class OrderItem
    {
        private readonly Product _product;
        private int _quantity;
        private readonly IPricingStrategy _pricingStrategy;
        
        public OrderItem(Product product, IPricingStrategy pricingStrategy, int quanitity = 1)
        {
            _product = product;
            _quantity = quanitity;
            _pricingStrategy = pricingStrategy;
        }

        public Product Product
        {
            get { return _product; }
        }

        public int Quantity
        {
            get { return _quantity; }
        }

        public void Add(int quantity = 1)
        {
            this._quantity = this._quantity + quantity;
        }

        public decimal GetPrice()
        {
            return _pricingStrategy.GetTotal(this);
        }
    }
}
