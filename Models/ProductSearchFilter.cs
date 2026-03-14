namespace EcommerceOrder.Models;

public class ProductSearchFilter
{
    public string? Category { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public bool? InStockOnly { get; set; }
    public string? NameContains { get; set; }
    public string SortBy { get; set; } = "name";
    public bool Ascending { get; set; } = true;
}