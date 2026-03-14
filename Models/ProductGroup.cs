namespace EcommerceOrder.Models;

public class ProductGroup
{
    public string Category { get; set; } = "";
    public List<Product> Products { get; set; } = new();
    public decimal AveragePrice { get; set; }
    public int TotalStock { get; set; }
    public int ProductCount { get; set; }
}