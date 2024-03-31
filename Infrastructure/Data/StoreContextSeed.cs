using System.Text.Json;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        LoadData<Product>(context, "../Infrastructure/Data/SeedData/products.json");
        LoadData<ProductType>(context, "../Infrastructure/Data/SeedData/types.json");
        LoadData<ProductBrand>(context, "../Infrastructure/Data/SeedData/brands.json");
        if (context.ChangeTracker.HasChanges()) await context.SaveChangesAsync();
    }

    public static void LoadData<TEntity>(DbContext context, string filePath) where TEntity : class
    {
        var dbSet = context.Set<TEntity>();
        if (!dbSet.Any())
        {
            var jsonData = File.ReadAllText(filePath);
            var entities = JsonSerializer.Deserialize<List<TEntity>>(jsonData);
            dbSet.AddRange(entities);
        }
    }
}
