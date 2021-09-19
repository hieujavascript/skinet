using System.Reflection;
using Core.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {            
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }    
}
/*  REMOVE VA TAO LAI MIGRATIONS
dotnet ef migrations remove  -p Infrastructure -s API
dotnet ef database drop -p Infrastructure -s API
 dotnet ef migrations remove  -p Infrastructure -s API
dotnet ef migrations add InitialCreate  -p Infrastructure -s API -o Data/Migrations 
*/