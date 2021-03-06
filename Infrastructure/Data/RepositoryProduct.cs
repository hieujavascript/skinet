

using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entity;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class RepositoryProduct : IRepositoryProduct
    {
        private readonly StoreContext _context;
        public RepositoryProduct(StoreContext context)
        {
            _context = context;
        }

        public async Task<List<ProductBrand>> getProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<Product> getProductByIdAsync(int id)
        {
            var product = await _context.Products
                                .Include(b => b.ProductBrand)
                                .Include(t => t.ProductType)
                                .FirstOrDefaultAsync(p => p.Id == id);
                         
            return product;
        }
        public async Task<IEnumerable<Product>> getProductsAsync()
        {
            var products = await _context.Products
                            .Include(b => b.ProductBrand)
                            .Include(t => t.ProductType)
                            .ToListAsync();
            return products;
        }

        public async Task<List<ProductType>> getProductTypesAsync()
        {
           return await _context.ProductTypes.ToListAsync();
        }
    }

}