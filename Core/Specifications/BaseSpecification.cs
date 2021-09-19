using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using Core.Entity;
//2
namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public BaseSpecification() {} // de lopke thua khong bi loi vi Contructor o tren co tham so , lop ke thua se bi doi hoi truyen tham so
        public Expression<Func<T, bool>> Criteria {get;}
        public List<Expression<Func<T, object>>> Includes {get;} = new List<Expression<Func<T, object>>>();
        protected void AddInclude(Expression<Func<T , object>> includeSpection) {
            Includes.Add(includeSpection);
        }
        
    }
}