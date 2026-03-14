using EcommerceOrder.Models;

namespace EcommerceOrder.Display;

public class ConsoleProductDisplay : IProductDisplay
{
    public void ShowProducts(List<Product> products)
    {
        if (!products.Any())
        {
            Console.WriteLine("  No products found.");
            return;
        }

        Console.WriteLine($"  {"ID",-4} {"Name",-26} {"Category",-14} {"Price",8} {"Stock",6}");
        Console.WriteLine(new string('-', 65));

        products.ForEach(p =>
            Console.WriteLine($"  {p.Id,-4} {p.Name,-26} {p.Category,-14} {p.Price,8:C} {p.StockQuantity,6}"));

        Console.WriteLine($"\n  Total: {products.Count} product(s)");
    }

    public void ShowProductGroups(List<ProductGroup> groups)
    {
        if (!groups.Any())
        {
            Console.WriteLine("  No groups found.");
            return;
        }

        foreach (var group in groups)
        {
            Console.WriteLine($"\n  [{group.Category}]  {group.ProductCount} items  |  Avg: {group.AveragePrice:C}  |  Total stock: {group.TotalStock}");
            Console.WriteLine(new string('-', 65));

            group.Products.ForEach(p =>
                Console.WriteLine($"    {p.Id,-4} {p.Name,-26} {p.Price,8:C}  Stock: {p.StockQuantity}"));
        }
    }

    public void ShowProduct(Product product)
    {
        Console.WriteLine($"  ID       : {product.Id}");
        Console.WriteLine($"  Name     : {product.Name}");
        Console.WriteLine($"  Category : {product.Category}");
        Console.WriteLine($"  Price    : {product.Price:C}");
        Console.WriteLine($"  Stock    : {product.StockQuantity}");
        Console.WriteLine($"  In Stock : {(product.IsInStock() ? "Yes" : "No")}");
        if (!string.IsNullOrWhiteSpace(product.Description))
            Console.WriteLine($"  Desc     : {product.Description}");
    }
}