using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Core.Entity;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    // lop nay thuc thi va TRẢ VỀ IQUERYABLE
    public class SpecificationEvaluator<TEntity> where TEntity: BaseEntity {
      
      public static IQueryable<TEntity> MakeQuery(IQueryable<TEntity> inputQuery , ISpecification<TEntity> spec) {
        var query = inputQuery;
        if(spec.Criteria != null)
        query = query.Where(spec.Criteria);
        
        query = spec.Includes.Aggregate(query,(current , include) => current.Include(include));
        return query;
      }

    }
}