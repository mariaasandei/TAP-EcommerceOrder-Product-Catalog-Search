using EcommerceOrder.Models;

namespace EcommerceOrder.Display;

public interface IProductDisplay
{
    void ShowProducts(List<Product> products);
    void ShowProductGroups(List<ProductGroup> groups);
    void ShowProduct(Product product);
}