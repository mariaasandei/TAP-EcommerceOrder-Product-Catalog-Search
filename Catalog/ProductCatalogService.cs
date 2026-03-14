using EcommerceOrder.Models;
using EcommerceOrder.Storage;

namespace EcommerceOrder.Catalog;

public class ProductCatalogService : IProductCatalogService
{
    private readonly IProductStorage _storage;

    public ProductCatalogService(IProductStorage storage)
    {
        _storage = storage;
    }

    public List<Product> GetAllProducts() =>
        _storage.GetAll().ToList();

    public Product? GetProductById(int id) =>
        _storage.FindById(id);

    public List<Product> SearchProducts(ProductSearchFilter filter)
    {
        var query = _storage.GetAll().AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Category))
            query = query.Where(p =>
                p.Category.Equals(filter.Category, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(filter.NameContains))
            query = query.Where(p =>
                p.Name.Contains(filter.NameContains, StringComparison.OrdinalIgnoreCase));

        if (filter.MinPrice.HasValue)
            query = query.Where(p => p.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(p => p.Price <= filter.MaxPrice.Value);

        if (filter.InStockOnly == true)
            query = query.Where(p => p.IsInStock());

        query = ApplySorting(query, filter.SortBy, filter.Ascending);

        return query.ToList();
    }

    public List<ProductGroup> GetProductsGroupedByCategory()
    {
        return (
            from p in _storage.GetAll()
            group p by p.Category into g
            select new ProductGroup
            {
                Category = g.Key,
                Products = g.OrderBy(p => p.Price).ToList(),
                AveragePrice = g.Average(p => p.Price),
                TotalStock = g.Sum(p => p.StockQuantity),
                ProductCount = g.Count()
            }
        )
        .OrderBy(g => g.Category)
        .ToList();
    }

    public List<ProductGroup> GetGroupsMatchingFilter(ProductSearchFilter filter)
    {
        var filtered = SearchProducts(filter);

        return (
            from p in filtered
            group p by p.Category into g
            select new ProductGroup
            {
                Category = g.Key,
                Products = g.ToList(),
                AveragePrice = g.Average(p => p.Price),
                TotalStock = g.Sum(p => p.StockQuantity),
                ProductCount = g.Count()
            }
        )
        .OrderBy(g => g.Category)
        .ToList();
    }

    private static IQueryable<Product> ApplySorting(
        IQueryable<Product> query, string sortBy, bool ascending)
    {
        return sortBy.ToLower() switch
        {
            "price" => ascending
                ? query.OrderBy(p => p.Price)
                : query.OrderByDescending(p => p.Price),
            "stock" => ascending
                ? query.OrderBy(p => p.StockQuantity)
                : query.OrderByDescending(p => p.StockQuantity),
            "category" => ascending
                ? query.OrderBy(p => p.Category)
                : query.OrderByDescending(p => p.Category),
            _ => ascending
                ? query.OrderBy(p => p.Name)
                : query.OrderByDescending(p => p.Name)
        };
    }
}