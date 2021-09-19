using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Interfaces
{
  public interface IRepositoryProduct {
      Task<IEnumerable<Product>> getProductsAsync();
      Task<Product> getProductByIdAsync(int id) ;
  }
}