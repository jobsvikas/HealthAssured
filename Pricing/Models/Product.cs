namespace Pricing.Models
{
    public class Product
    {
        public Product(int productId, string productName, decimal UnitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            this.UnitPrice = UnitPrice;
        }
        public int ProductId { get; }

        public string ProductName { get; }

        public decimal UnitPrice { get; }
    }
}
