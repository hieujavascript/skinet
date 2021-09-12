using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entity;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class ContextSeed
    {
        public static async Task SeedAsync(StoreContext storeContext , ILoggerFactory loggerfactory ) {
            try {
                if(!storeContext.ProductBrands.Any())
                {
                    var BrandData = File.ReadAllText("../Infrastructure/Data/SeedData/brands.json");
                    var brand = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                    foreach(var item in brand)
                    {
                        storeContext.ProductBrands.Add(item);
                    }
                    await storeContext.SaveChangesAsync();
                }
                if(!storeContext.ProductTypes.Any())
                {
                    var TypeData = File.ReadAllText("../Infrastructure/Data/SeedData/types.json");
                    var type = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                    foreach(var item in type)
                    {
                        storeContext.ProductTypes.Add(item);
                    }
                    await storeContext.SaveChangesAsync();
                }
                if(!storeContext.Products.Any())
                {
                    var ProductData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
                    var productlist = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    foreach(var item in productlist)
                    {
                        storeContext.Products.Add(item);
                    }
                    await storeContext.SaveChangesAsync();
                }
                
            }
            catch(Exception e) {
                var logger = loggerfactory.CreateLogger<ContextSeed>();
                logger.LogError(e.Message);
            }
        } 
    }
}