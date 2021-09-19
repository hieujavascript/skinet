using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Core.Entity;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandSpecication : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandSpecication()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);            
        }
        // Specification sẽ có where id = id va 2 include
        public ProductsWithTypesAndBrandSpecication(int id) : base(w => w.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);  
        }
    }
}