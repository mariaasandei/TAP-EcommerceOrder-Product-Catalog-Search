using EcommerceOrder.Models;

namespace EcommerceOrder.Storage;

public interface IProductStorage
{
    IReadOnlyList<Product> GetAll();
    Product? FindById(int id);
    void Add(Product product);
    void AddRange(IEnumerable<Product> products);
}