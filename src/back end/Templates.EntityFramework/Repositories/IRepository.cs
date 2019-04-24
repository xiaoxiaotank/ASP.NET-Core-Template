using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.EntityFrameworkCore.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : Entity
    {

    }


    public interface IRepository<TEntity, TKey> 
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        int Count();

        int Count(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);

        long LongCount();

        long LongCount(Expression<Func<TEntity, bool>> predicate);

        Task<long> LongCountAsync();

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(TKey id);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Get();

        Task<TEntity> GetAsync(TKey id);

        TEntity Insert(TEntity entity);

        int Insert(IEnumerable<TEntity> entities);

        Task<TEntity> InsertAsync(TEntity entity);

        Task<int> InsertAsync(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        int Update(IEnumerable<TEntity> entities);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<int> UpdateAsync(IEnumerable<TEntity> entities);

        int Delete(TEntity entity);

        int Delete(TKey id);

        int Delete(IEnumerable<TEntity> entities);

        int Delete(Expression<Func<TEntity, bool>> predicate);

        Task<int> DeleteAsync(TEntity entity);

        Task<int> DeleteAsync(TKey id);

        Task<int> DeleteAsync(IEnumerable<TEntity> entities);

        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
    }

}
