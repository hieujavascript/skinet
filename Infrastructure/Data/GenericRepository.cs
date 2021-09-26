using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Threading.Tasks.Sources;
using Core.Entity;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private  StoreContext _storeContext = null;
         private DbSet<T> tableName = null;
        
        public GenericRepository(StoreContext storeContext)
        {
            this._storeContext = storeContext;
            this.tableName = _storeContext.Set<T>();
        }

        public async Task<T> getByIdAsync(int id)
        {
           var t_Entity  = await tableName.FindAsync(id);
           return t_Entity;
        }
        public async Task<List<T>> ListAllAsync()
        {
            var tl_Entity = await tableName.ToListAsync();
            return tl_Entity;
        }
        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            //  controller  truyen du lieu 1 where va 2 include
            //  ApplySpecification(spec) tra về câu query có 1 where và 2 include
            // sau do .SingleOrDefaultAsync lay ra recore phu hop voi dieu kien ISpecification spec
            return await ApplySpecification(spec).SingleOrDefaultAsync();
        }
        public async Task<List<T>> ListAsync(ISpecification<T> spec)
        {             
              var list = await ApplySpecification(spec).ToListAsync();
              return list;
        }
        public IQueryable<T> ApplySpecification(ISpecification<T> spec) {
            // nó sẽ trả về 1 câu Query với where , include , orderby , orderbyDescending v... đc truyền vào từ Spec
            // thông qua controller khi gọi ListAsync , GetEntityWithSpec
             var my_IQueryable = SpecificationEvaluator<T>.MakeQuery(_storeContext.Set<T>().AsQueryable() , spec);
            return my_IQueryable;
        }

        public Task<int> CountAsync(ISpecification<T> spec)
        {
            return ApplySpecification(spec).CountAsync();
        }
    }
}