using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data.Seeding
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.ProductBrands.Any())
                {
                    var brandData = File.ReadAllText("../Infrastructure/Data/SeedingData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                    context.ProductBrands.AddRange(brands);
                    await context.SaveChangesAsync();
                }

                if (!context.ProductTypes.Any())
                {
                    var typesData = File.ReadAllText("../Infrastructure/Data/SeedingData/types.json");
                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    context.ProductTypes.AddRange(types);
                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData = File.ReadAllText("../Infrastructure/Data/SeedingData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    context.Products.AddRange(products);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex,"Exception raise in seed class.");
            }
        }
    }
}