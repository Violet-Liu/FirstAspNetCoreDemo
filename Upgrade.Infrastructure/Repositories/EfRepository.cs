using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Upgrade.Core.Bases;
using Upgrade.Core.Interfaces;
using Upgrade.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;

namespace Upgrade.Infrastructure.Repositories
{
    public class EfRepository<T> : IRepository<T> where T :EntityBase
    {
        #region Field
        protected readonly UDBContext uDBContext;
        #endregion

        #region Construction
        public EfRepository(UDBContext context)
        {
            this.uDBContext = context;
        }
        #endregion

        public T Add(T entity)
        {
            uDBContext.Set<T>().Add(entity);
            return entity;
        }

        public void AddRange(IEnumerable<T> entities) => uDBContext.Set<T>().AddRange(entities);

        public void Attach(T entity)=> uDBContext.Set<T>().Attach(entity);

        public void AttachAsModified(T entity)
        {
            Attach(entity);
            Update(entity);
        }

        public void AttachRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Attach(entity);
            }
        }

        public async Task<int> CountAsync()=> await uDBContext.Set<T>().CountAsync();

        public async Task<int> CountAsync(Expression<Func<T, bool>> criteria)=> await uDBContext.Set<T>().CountAsync(criteria);

        public void Delete(T entity)=> uDBContext.Set<T>().Remove(entity);

        
        public void DeleteRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                uDBContext.Set<T>().Remove(entity);
            }
        }

        public void DeleteWhere(Expression<Func<T, bool>> criteria)
        {
            IEnumerable<T> entities = uDBContext.Set<T>().Where(criteria);
            foreach (var entity in entities)
            {
                uDBContext.Entry(entity).State = EntityState.Deleted;
            }
        }

        public void Detach(T entity) => uDBContext.Entry(entity).State = EntityState.Detached;

        public void DetachRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Detach(entity);
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)=> await uDBContext.Set<T>().FindAsync(id);


        public virtual async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
               .Aggregate(uDBContext.Set<T>().AsQueryable(),
                   (current, include) => current.Include(include));
            return await queryableResultWithIncludes.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> criteria)
        {
           return await uDBContext.Set<T>().FirstOrDefaultAsync(criteria);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(uDBContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));
            return await queryableResultWithIncludes.FirstOrDefaultAsync(criteria);
        }

        public async Task<T> GetSingleAsync<TProperty>(Expression<Func<T, bool>> criteria, Expression<Func<T, TProperty>> orderByExpression, bool ascending = true)
        {
            if (ascending)
                return await uDBContext.Set<T>().OrderBy(orderByExpression).FirstOrDefaultAsync(criteria);
            else
                return await uDBContext.Set<T>().OrderByDescending(orderByExpression).FirstOrDefaultAsync(criteria);
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> criteria)=> uDBContext.Set<T>().Where(criteria).AsEnumerable();

        public IEnumerable<T> List(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(uDBContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));
            return queryableResultWithIncludes.Where(criteria).AsEnumerable();
        }

        public IQueryable<T> GetAll() => uDBContext.Set<T>();
        public IEnumerable<T> ListAll()=> uDBContext.Set<T>().AsEnumerable();

        public async Task<List<T>> ListAllAsync()=> await uDBContext.Set<T>().ToListAsync();

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria)
        {
            return await uDBContext.Set<T>().Where(criteria).ToListAsync(); ;
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes)
        {
            var queryableResultWithIncludes = includes
                .Aggregate(uDBContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));
            return await queryableResultWithIncludes.Where(criteria).ToListAsync();
        }

        public void Update(T entity)
        {
            uDBContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteByIdAsync(int Id)
        {
            var entity = await GetByIdAsync(Id);
            if (entity != null)
            {
                uDBContext.Set<T>().Remove(entity);
            }
        }
    }
}
