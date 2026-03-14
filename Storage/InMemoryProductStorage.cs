using EcommerceOrder.Models;

namespace EcommerceOrder.Storage;

public class InMemoryProductStorage : IProductStorage
{
    private readonly List<Product> _products = new();

    public IReadOnlyList<Product> GetAll() => _products.AsReadOnly();

    public Product? FindById(int id) =>
        _products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product) => _products.Add(product);

    public void AddRange(IEnumerable<Product> products) =>
        _products.AddRange(products);
}