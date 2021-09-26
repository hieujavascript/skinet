using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entity;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification: BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productparam): base(x => 
                (string.IsNullOrEmpty(productparam.Search) || x.Name.Contains(productparam.Search)) 
               && (!productparam.BrandId.HasValue || x.ProductBrandId == productparam.BrandId)  
               && (!productparam.TypeId.HasValue || x.ProductTypeId == productparam.TypeId)  
              )         
        {
        }
    }
}