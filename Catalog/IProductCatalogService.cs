using EcommerceOrder.Models;

namespace EcommerceOrder.Catalog;

public interface IProductCatalogService
{
    List<Product> GetAllProducts();
    Product? GetProductById(int id);
    List<Product> SearchProducts(ProductSearchFilter filter);
    List<ProductGroup> GetProductsGroupedByCategory();
    List<ProductGroup> GetGroupsMatchingFilter(ProductSearchFilter filter);
}