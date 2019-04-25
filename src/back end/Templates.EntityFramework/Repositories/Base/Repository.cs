using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.EntityFrameworkCore.Repositories
{
    public abstract class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        #region Count
        public virtual int Count() => Get().Count();

        public virtual int Count(Expression<Func<TEntity, bool>> predicate) => Get(predicate).Count();

        public virtual Task<int> CountAsync() => Task.FromResult(Get().Count());

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(Get(predicate).Count());

        public virtual long LongCount() => Get().LongCount();

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate) => Get(predicate).LongCount();

        public virtual Task<long> LongCountAsync() => Task.FromResult(Get().LongCount());

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) => Task.FromResult(Get(predicate).LongCount());
        #endregion

        #region Get
        public virtual TEntity Get(TKey id) => Get().FirstOrDefault(e => e.Id.Equals(id));

        public abstract IQueryable<TEntity> Get();

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate) => Get().Where(predicate);

        public virtual Task<TEntity> GetAsync(TKey id) => Task.FromResult(Get().FirstOrDefault(e => e.Id.Equals(id)));
        #endregion

        #region Insert
        public abstract TEntity Insert(TEntity entity);

        public abstract int Insert(IEnumerable<TEntity> entities);

        public abstract Task<TEntity> InsertAsync(TEntity entity);

        public abstract Task<int> InsertAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Update
        public abstract TEntity Update(TEntity entity);

        public abstract int Update(IEnumerable<TEntity> entities);

        public virtual Task<TEntity> UpdateAsync(TEntity entity) => Task.FromResult(Update(entity));

        public virtual Task<int> UpdateAsync(IEnumerable<TEntity> entities) => Task.FromResult(Update(entities));
        #endregion

        #region Delete
        public abstract int Delete(TEntity entity);

        public virtual int Delete(TKey id)
        {
            var entity = Get(id);
            if (entity != null)
            {
                return Delete(entity);
            }

            return 0;
        }

        public abstract int Delete(IEnumerable<TEntity> entities);

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Get(predicate);
            if (entities.Any())
            {
                Delete(entities);
            }

            return 0;
        }

        public virtual Task<int> DeleteAsync(TEntity entity) => Task.FromResult(Delete(entity));

        public virtual async Task<int> DeleteAsync(TKey id)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                return await DeleteAsync(entity);
            }

            return await Task.FromResult(0);
        }

        public virtual Task<int> DeleteAsync(IEnumerable<TEntity> entities) => Task.FromResult(Delete(entities));

        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Get(predicate);
            if (entities.Any())
            {
                await DeleteAsync(entities);
            }

            return await Task.FromResult(0);
        } 
        #endregion
    }
}
