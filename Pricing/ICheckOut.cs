
using Pricing.Models;

public interface ICheckOut
{
    void Scan(Product product);

    decimal GetTotalPrice();
}
