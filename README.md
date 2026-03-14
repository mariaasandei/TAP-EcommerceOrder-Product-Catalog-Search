# 🛒 EcommerceOrder — Product Catalog Search

![.NET](https://img.shields.io/badge/.NET-10-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-Console%20App-239120?style=for-the-badge&logo=csharp&logoColor=white)
![SOLID](https://img.shields.io/badge/Principles-SOLID-blueviolet?style=for-the-badge)
![LINQ](https://img.shields.io/badge/LINQ-Query%20%2B%20Lambda-cyan?style=for-the-badge)

A **C# .NET 10 console application** implementing a fully-featured Product Catalog Search system, built following **SOLID principles**, clean architecture, and performance-conscious LINQ queries.

---

## ✨ Features

- 🔍 **Search products** with multiple filters (category, name, price range, stock)
- 📦 **Group products** by category with aggregated stats (average price, total stock, count)
- 🔃 **Sort results** by name, price, stock quantity, or category — ascending or descending
- 🎨 **Color-coded console UI** — pink/purple logo, yellow labels, cyan keywords
- 💡 **User-friendly prompts** — available categories shown inline when filtering
- 🏗️ **Clean separation of concerns** — storage, catalog logic, seeding, and display are all independent

---

## 🗂️ Project Structure

```
EcommerceOrder/
├── Models/
│   ├── Product.cs                  # Domain model with business logic
│   ├── ProductSearchFilter.cs      # Filter parameters object
│   └── ProductGroup.cs             # Grouped result model
├── Storage/
│   ├── IProductStorage.cs          # Storage abstraction
│   └── InMemoryProductStorage.cs   # In-memory storage implementation
├── Catalog/
│   ├── IProductCatalogService.cs   # Catalog service abstraction
│   ├── IProductSeeder.cs           # Seeder abstraction
│   ├── ProductCatalogService.cs    # Search, filter, group logic
│   └── ProductSeeder.cs            # Sample data seeding
├── Display/
│   ├── IProductDisplay.cs          # Display abstraction
│   └── ConsoleProductDisplay.cs    # Console rendering
└── Program.cs                      # Entry point, menu, color UI
```

---

## 🧱 SOLID Principles Applied

| Principle | How it's applied |
|---|---|
| **S** — Single Responsibility | Each class has one job: `ProductSeeder` seeds, `InMemoryProductStorage` stores, `ConsoleProductDisplay` renders |
| **O** — Open/Closed | New storage or display implementations can be added without modifying existing classes |
| **L** — Liskov Substitution | All implementations are fully interchangeable through their interfaces |
| **I** — Interface Segregation | `IProductStorage`, `IProductCatalogService`, `IProductSeeder`, `IProductDisplay` are small and focused |
| **D** — Dependency Inversion | `ProductCatalogService` depends on `IProductStorage`, not on a concrete class |

---

## ⚡ LINQ Usage

### Lambda Expressions
```csharp
query = query.Where(p =>
    p.Category.Equals(filter.Category, StringComparison.OrdinalIgnoreCase));

query = query.OrderBy(p => p.Price);

var categories = products.Select(p => p.Category).Distinct().OrderBy(c => c);
```

### LINQ Query Language
```csharp
return (
    from p in _storage.GetAll()
    group p by p.Category into g
    select new ProductGroup
    {
        Category     = g.Key,
        Products     = g.OrderBy(p => p.Price).ToList(),
        AveragePrice = g.Average(p => p.Price),
        TotalStock   = g.Sum(p => p.StockQuantity),
        ProductCount = g.Count()
    }
)
.OrderBy(g => g.Category)
.ToList();
```

---

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)

### Run the app

```bash
git clone https://github.com/your-username/EcommerceOrder.git
cd EcommerceOrder
dotnet run
```

---

## 🖥️ Console UI Preview

```
  +==================================+
  |   PRODUCT  CATALOG  SEARCH       |
  |   [ CART ] E-Commerce System     |
  +==================================+

  1. View all products
  2. Search with filters
  3. View products grouped by category
  4. Search and group by category
  5. Find product by ID
  0. Exit

  Choice:
```

When searching:
```
  --- Search Filter ---
  Category (Apparel, Books, Electronics, Home) or blank for all:
  Sort by (name / price / stock / category) [default: name]:
  In stock only? (y/n):
```

---

## 📊 Sample Data

| ID | Name | Category | Price | Stock |
|---|---|---|---|---|
| 1 | Wireless Mouse | Electronics | $29.99 | 100 |
| 2 | Mechanical Keyboard | Electronics | $79.99 | 50 |
| 4 | Noise-Cancel Headset | Electronics | $149.99 | 30 |
| 5 | Running Shoes | Apparel | $89.99 | 60 |
| 6 | Winter Jacket | Apparel | $119.99 | 40 |
| 7 | Python Programming | Books | $34.99 | 200 |
| 9 | Desk Lamp | Home | $24.99 | 80 |
| 10 | Coffee Maker | Home | $59.99 | 45 |

---

## 🏛️ Architecture — C4 Model (Container Level)

```
┌─────────────────────────────────────────────────┐
│             EcommerceOrder System               │
│                                                 │
│  ┌──────────────────────────────────────────┐   │
│  │        Console Application (.NET 10)     │   │
│  │                                          │   │
│  │   Program.cs ──► CatalogService          │   │
│  │                       │                  │   │
│  │                  ProductStorage          │   │
│  │              (In-Memory Database)        │   │
│  └──────────────────────────────────────────┘   │
└─────────────────────────────────────────────────┘
```

> Single deployment unit: a console application with an in-memory database.  
> External systems for payment and delivery would integrate via dedicated service interfaces.


