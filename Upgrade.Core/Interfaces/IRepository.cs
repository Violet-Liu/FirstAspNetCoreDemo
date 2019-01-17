﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Upgrade.Core.Bases;

namespace Upgrade.Core.Interfaces
{
    public interface IRepository<T> where T :  EntityBase
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> criteria);

        Task<T> GetSingleAsync<TProperty>(Expression<Func<T, bool>> criteria, Expression<Func<T, TProperty>> orderByExpression, bool ascending = true);

        Task<T> GetSingleAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes);

        IQueryable<T> GetAll();

        IEnumerable<T> ListAll();
        Task<List<T>> ListAllAsync();

        IEnumerable<T> List(Expression<Func<T, bool>> criteria);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria);
        IEnumerable<T> List(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> criteria, params Expression<Func<T, object>>[] includes);

        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);

        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);

        Task DeleteByIdAsync(int Id);

        void DeleteWhere(Expression<Func<T, bool>> criteria);
        void AddRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        void Attach(T entity);
        void AttachRange(IEnumerable<T> entities);
        void Detach(T entity);
        void DetachRange(IEnumerable<T> entities);
        void AttachAsModified(T entity);
    }
}