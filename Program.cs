using EcommerceOrder.Catalog;
using EcommerceOrder.Display;
using EcommerceOrder.Models;
using EcommerceOrder.Storage;

var storage = new InMemoryProductStorage();
var seeder = new ProductSeeder(storage);
var catalogService = new ProductCatalogService(storage);
var display = new ConsoleProductDisplay();

seeder.Seed();

RunMainMenu();

void RunMainMenu()
{
    while (true)
    {
        ShowHeader();

        WriteColored("  1.", ConsoleColor.Cyan);
        WriteLineColored(" View all products", ConsoleColor.Yellow);
        WriteColored("  2.", ConsoleColor.Cyan);
        WriteLineColored(" Search with filters", ConsoleColor.Yellow);
        WriteColored("  3.", ConsoleColor.Cyan);
        WriteLineColored(" View products grouped by category", ConsoleColor.Yellow);
        WriteColored("  4.", ConsoleColor.Cyan);
        WriteLineColored(" Search and group by category", ConsoleColor.Yellow);
        WriteColored("  5.", ConsoleColor.Cyan);
        WriteLineColored(" Find product by ID", ConsoleColor.Yellow);
        WriteColored("  0.", ConsoleColor.Cyan);
        WriteLineColored(" Exit", ConsoleColor.Yellow);

        Console.WriteLine();
        WriteColored("  Choice: ", ConsoleColor.Cyan);

        var input = Console.ReadLine()?.Trim();
        Console.WriteLine();

        switch (input)
        {
            case "1": HandleViewAll(); break;
            case "2": HandleSearchWithFilters(); break;
            case "3": HandleGroupedByCategory(); break;
            case "4": HandleSearchAndGroup(); break;
            case "5": HandleFindById(); break;
            case "0": return;
            default:
                WriteLineColored("  Invalid option. Press any key...", ConsoleColor.Yellow);
                Console.ReadKey();
                break;
        }
    }
}

void HandleViewAll()
{
    var products = catalogService.GetAllProducts();
    display.ShowProducts(products);
    Pause();
}

void HandleSearchWithFilters()
{
    var filter = BuildFilterFromUserInput();
    var results = catalogService.SearchProducts(filter);
    display.ShowProducts(results);
    Pause();
}

void HandleGroupedByCategory()
{
    var groups = catalogService.GetProductsGroupedByCategory();
    display.ShowProductGroups(groups);
    Pause();
}

void HandleSearchAndGroup()
{
    var filter = BuildFilterFromUserInput();
    var groups = catalogService.GetGroupsMatchingFilter(filter);
    display.ShowProductGroups(groups);
    Pause();
}

void HandleFindById()
{
    WriteColored("  Enter product ID: ", ConsoleColor.Cyan);
    var input = Console.ReadLine()?.Trim();

    if (!int.TryParse(input, out var id))
    {
        WriteLineColored("  Invalid ID.", ConsoleColor.Yellow);
        Pause();
        return;
    }

    var product = catalogService.GetProductById(id);

    if (product is null)
        WriteLineColored($"  No product found with ID {id}.", ConsoleColor.Yellow);
    else
        display.ShowProduct(product);

    Pause();
}

ProductSearchFilter BuildFilterFromUserInput()
{
    WriteLineColored("  --- Search Filter ---", ConsoleColor.Magenta);

    var availableCategories = catalogService
        .GetAllProducts()
        .Select(p => p.Category)
        .Distinct()
        .OrderBy(c => c)
        .ToList();

    var categoriesHint = string.Join(", ", availableCategories);

    WriteColored($"  Category ", ConsoleColor.Yellow);
    WriteColored($"({categoriesHint})", ConsoleColor.Cyan);
    WriteColored(" or blank for all: ", ConsoleColor.Yellow);
    var category = Console.ReadLine()?.Trim();

    WriteColored("  Name contains ", ConsoleColor.Yellow);
    WriteColored("(leave blank to skip)", ConsoleColor.Cyan);
    WriteColored(": ", ConsoleColor.Yellow);
    var name = Console.ReadLine()?.Trim();

    WriteColored("  Min price ", ConsoleColor.Yellow);
    WriteColored("(leave blank to skip)", ConsoleColor.Cyan);
    WriteColored(": ", ConsoleColor.Yellow);
    decimal? minPrice = decimal.TryParse(Console.ReadLine(), out var min) ? min : null;

    WriteColored("  Max price ", ConsoleColor.Yellow);
    WriteColored("(leave blank to skip)", ConsoleColor.Cyan);
    WriteColored(": ", ConsoleColor.Yellow);
    decimal? maxPrice = decimal.TryParse(Console.ReadLine(), out var max) ? max : null;

    WriteColored("  In stock only? ", ConsoleColor.Yellow);
    WriteColored("(y/n)", ConsoleColor.Cyan);
    WriteColored(": ", ConsoleColor.Yellow);
    var inStock = Console.ReadLine()?.Trim().ToLower() == "y";

    WriteColored("  Sort by ", ConsoleColor.Yellow);
    WriteColored("(name / price / stock / category)", ConsoleColor.Cyan);
    WriteColored(" [default: name]: ", ConsoleColor.Yellow);
    var sortBy = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(sortBy)) sortBy = "name";

    WriteColored("  Ascending? ", ConsoleColor.Yellow);
    WriteColored("(y/n)", ConsoleColor.Cyan);
    WriteColored(" [default: y]: ", ConsoleColor.Yellow);
    var ascending = Console.ReadLine()?.Trim().ToLower() != "n";

    Console.WriteLine();

    return new ProductSearchFilter
    {
        Category = string.IsNullOrWhiteSpace(category) ? null : category,
        NameContains = string.IsNullOrWhiteSpace(name) ? null : name,
        MinPrice = minPrice,
        MaxPrice = maxPrice,
        InStockOnly = inStock,
        SortBy = sortBy,
        Ascending = ascending
    };
}

void ShowHeader()
{
    Console.Clear();
    Console.WriteLine();

    WriteLineColored("  +==================================+", ConsoleColor.Magenta);
    WriteColored("  |   ", ConsoleColor.Magenta);
    WriteColored("PRODUCT  CATALOG  SEARCH      ", ConsoleColor.DarkMagenta);
    WriteLineColored(" |", ConsoleColor.Magenta);
    WriteColored("  |   ", ConsoleColor.Magenta);
    WriteColored("[ CART ] E-Commerce System    ", ConsoleColor.DarkMagenta);
    WriteLineColored(" |", ConsoleColor.Magenta);
    WriteLineColored("  +==================================+", ConsoleColor.Magenta);

    Console.WriteLine();
}

void Pause()
{
    WriteLineColored("\n  Press any key to continue...", ConsoleColor.Cyan);
    Console.ReadKey();
}

void WriteColored(string text, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.Write(text);
    Console.ResetColor();
}

void WriteLineColored(string text, ConsoleColor color)
{
    Console.ForegroundColor = color;
    Console.WriteLine(text);
    Console.ResetColor();
}