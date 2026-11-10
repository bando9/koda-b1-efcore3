
using KodaStore.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace KodaStore.Api.Seeding;

public static class ProductSeedData
{
    
    private static IEnumerable<Product> InitialProducts()
    {
        yield return new Product("P004", "Keyboard Mekanik", 35000m, 20, "Elektronik");
        yield return new Product("P002", "Mouse", 20000m, 20, "Mouse");
        yield return new Product("P003", "Monitor", 15000m, 20, "Screen");


        // *500 Data seed generate
        // for(int i = 1; i<=100; i++)
        // {
        //     string productCode = $"P{i:D3}";
        //     string producName = $"Product Dummy {i}";

        //     decimal price = 10000m+(i*1000m);
        //     int stock=20;

        //     string category = i%3 ==0 ? "Mie Ayam" : (i%2 ==0 ? "Bakso" : "Lainnya");

        //     yield return new Product(productCode,producName,price,stock,category);
        // }
    }

    public static void Seed(DbContext context)
    {
        foreach(var product in InitialProducts())
        {
            
            bool exists = context.Set<Product>().Any(p=>p.ProductCode == product.ProductCode);

            if (!exists)
            {
                context.Set<Product>().Add(product);
            };

        }

        context.SaveChanges();
    }

    public static async Task SeedAsync(DbContext context, CancellationToken ct)
    {
        foreach (var product in InitialProducts())
        {
            bool exists = await context.Set<Product>().AnyAsync(p => p.ProductCode == product.ProductCode, ct);

            if (!exists)
            {
                context.Set<Product>().Add(product);
            }
        }

        await context.SaveChangesAsync();
    }
}