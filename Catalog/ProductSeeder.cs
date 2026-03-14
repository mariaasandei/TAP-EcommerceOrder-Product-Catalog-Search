using EcommerceOrder.Models;
using EcommerceOrder.Storage;

namespace EcommerceOrder.Catalog;

public class ProductSeeder : IProductSeeder
{
    private readonly IProductStorage _storage;

    public ProductSeeder(IProductStorage storage)
    {
        _storage = storage;
    }

    public void Seed()
    {
        _storage.AddRange(new[]
        {
            new Product { Id = 1,  Name = "Wireless Mouse",        Category = "Electronics", Price = 29.99m,  StockQuantity = 100, Description = "Ergonomic wireless mouse with long battery life." },
            new Product { Id = 2,  Name = "Mechanical Keyboard",   Category = "Electronics", Price = 79.99m,  StockQuantity = 50,  Description = "RGB mechanical keyboard with tactile switches." },
            new Product { Id = 3,  Name = "USB-C Hub",             Category = "Electronics", Price = 39.99m,  StockQuantity = 75,  Description = "7-in-1 USB-C hub with HDMI and SD card." },
            new Product { Id = 4,  Name = "Noise-Cancel Headset",  Category = "Electronics", Price = 149.99m, StockQuantity = 30,  Description = "Over-ear headset with active noise cancellation." },
            new Product { Id = 5,  Name = "Running Shoes",         Category = "Apparel",     Price = 89.99m,  StockQuantity = 60,  Description = "Lightweight running shoes for all terrains." },
            new Product { Id = 6,  Name = "Winter Jacket",         Category = "Apparel",     Price = 119.99m, StockQuantity = 40,  Description = "Insulated winter jacket, waterproof." },
            new Product { Id = 7,  Name = "Python Programming",    Category = "Books",       Price = 34.99m,  StockQuantity = 200, Description = "Comprehensive guide to Python 3." },
            new Product { Id = 8,  Name = "Clean Code Book",       Category = "Books",       Price = 29.99m,  StockQuantity = 150, Description = "Best practices for writing clean maintainable code." },
            new Product { Id = 9,  Name = "Desk Lamp",             Category = "Home",        Price = 24.99m,  StockQuantity = 80,  Description = "LED desk lamp with adjustable brightness." },
            new Product { Id = 10, Name = "Coffee Maker",          Category = "Home",        Price = 59.99m,  StockQuantity = 45,  Description = "12-cup programmable coffee maker." },
            new Product { Id = 11, Name = "Yoga Mat",              Category = "Apparel",     Price = 19.99m,  StockQuantity = 120, Description = "Non-slip yoga mat, 6mm thickness." },
            new Product { Id = 12, Name = "Monitor Stand",         Category = "Electronics", Price = 44.99m,  StockQuantity = 65,  Description = "Adjustable monitor stand with USB ports." },
        });
    }
}