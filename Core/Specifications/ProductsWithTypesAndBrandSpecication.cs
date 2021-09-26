using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Core.Entity;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandSpecication : BaseSpecification<Product>
    {
        // noi day al dieu kien
        public ProductsWithTypesAndBrandSpecication(ProductSpecParams productSpecParams)
        : base(x => 
                (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search)) 
               && (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId)  
               && (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)  
              )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(p => p.Name); // descing != null  đc định nghĩa trong SpecificationEvaluator;
            ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1) , productSpecParams.PageSize);
            switch(productSpecParams.Sort) {
               case "priceAsc" : AddOrderBy(p => p.Price); break;
               case "priceDesc" : AddOrderByDescing(p => p.Price); break;
               default : AddOrderBy(n => n.Name); break;// descing != null  đc định nghĩa trong SpecificationEvaluator;
           }
        }        
        // Specification sẽ có where id = id va 2 include
        public ProductsWithTypesAndBrandSpecication(int id) : base(w => w.Id == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);  
        }
    }
}