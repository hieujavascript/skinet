using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Interfaces
{
  public interface IRepositoryProduct {
      Task<IEnumerable<Product>> getProductsAsync();
      Task<Product> getProductIdAsync(int id) ;
      Task<List<ProductBrand>> getProductBrandsAsync();
      Task<List<ProductType>> getProductTypesAsync();
  }
}