using EcommerceOrder.Models;

namespace EcommerceOrder.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Category { get; set; } = "";
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Description { get; set; } = "";

    public bool IsInStock() => StockQuantity > 0;

    public bool IsAvailable(int requestedQuantity) => StockQuantity >= requestedQuantity;

    public decimal GetDiscountedPrice(decimal discountPercent) =>
        Price * (1 - discountPercent / 100);
}