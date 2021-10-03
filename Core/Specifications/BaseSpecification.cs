using System;
using System.Collections.Generic;
using System.Dynamic;
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
        public List<Expression<Func<T, object>>> Includes {get;}  = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy {get; private set;} // sẽ đc gán trong class khac  

        public Expression<Func<T, object>> OrderByDescending {get; private set;}
       
        protected void AddInclude(Expression<Func<T , object>> includeSpection) {
            Includes.Add(includeSpection);
        }
        protected void AddOrderBy(Expression<Func<T , object>> orderby_spec) {
            OrderBy = orderby_spec;
        }
          protected void AddOrderByDescing(Expression<Func<T , object>> orderbydescing_spec) {
            OrderByDescending = orderbydescing_spec;
        }

         public int Take {get; private set;}

        public int SKip  {get; private set;}

        public bool IsPagingEnabled  {get; private set;}
        protected void ApplyPaging(int skip , int take) {
            Take = take;
            SKip = skip;
            IsPagingEnabled = true;             
        }
    }
}