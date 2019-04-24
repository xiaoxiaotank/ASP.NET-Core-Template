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
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbContext _ctx;
        protected readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }

        public virtual int Count() => _dbSet.Count();

        public virtual int Count(Expression<Func<TEntity, bool>> predicate) => _dbSet.Count(predicate);

        public virtual async Task<int> CountAsync() => await _dbSet.CountAsync();

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.CountAsync(predicate);

        public virtual long LongCount() => _dbSet.LongCount();

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate) => _dbSet.LongCount();

        public virtual async Task<long> LongCountAsync() => await _dbSet.LongCountAsync();

        public virtual async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.LongCountAsync(predicate);

        public virtual TEntity Get(int id) => _dbSet.Find(id);

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
            => _dbSet.AsNoTracking().Where(predicate);

        public virtual IQueryable<TEntity> Get() => _dbSet.AsNoTracking();

        public virtual async Task<TEntity> GetAsync(int id) => await _dbSet.FindAsync(id);

        public virtual TEntity Insert(TEntity entity)
        {
            var entry = _dbSet.Add(entity);
            Save();
            return entry.Entity;
        }

        public virtual int Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return Save();
        }

        public virtual async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entry = await _dbSet.AddAsync(entity);
            await SaveAsync();
            return entry.Entity;
        }

        public virtual async Task<int> InsertAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return await SaveAsync();
        }

        public virtual TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            var entry = _dbSet.Update(entity);
            Save();
            return entry.Entity;
        }

        public virtual int Update(IEnumerable<TEntity> entities)
        {
            AttachIfNot(entities);
            _dbSet.UpdateRange(entities);
            return Save();
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            var entry = await Task.Run(() => _dbSet.Update(entity));
            await SaveAsync();
            return entry.Entity;
        }

        public virtual async Task<int> UpdateAsync(IEnumerable<TEntity> entities)
        {
            AttachIfNot(entities);
            await Task.Run(() => _dbSet.UpdateRange(entities));
            return await SaveAsync();
        }

        public virtual int Delete(TEntity entity)
        {
            AttachIfNot(entity);
            _dbSet.Remove(entity);
            return Save();
        }

        public int Delete(int id)
        {
            var entity = Get(id);
            if(entity != null)
            {
                return Delete(entity);
            }

            return 0;
        }

        public virtual int Delete(IEnumerable<TEntity> entities)
        {
            AttachIfNot(entities);
            _dbSet.RemoveRange(entities);
            return Save();
        }

        public int Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Get(predicate);
            if (entities.Any())
            {
                _dbSet.RemoveRange(entities);
            }
            return Save();
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            AttachIfNot(entity);
            await Task.Run(() => _dbSet.Remove(entity));
            return await SaveAsync();
        }

        public virtual async Task<int> DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            if(entity != null)
            {
                return await DeleteAsync(entity);
            }

            return await Task.FromResult(0);
        }

        public virtual async Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _dbSet.RemoveRange(entities));
            return await SaveAsync();
        }

        public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = Get(predicate);
            if (entities.Any())
            {
                _dbSet.RemoveRange(entities);
            }
            return await SaveAsync();
        }

        protected virtual int Save() => _ctx.SaveChanges();

        protected virtual async Task<int> SaveAsync() => await _ctx.SaveChangesAsync();

        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!_dbSet.Local.Contains(entity))
            {
                _dbSet.Attach(entity);
            }
        }

        protected virtual void AttachIfNot(IEnumerable<TEntity> entities)
        {
            var detachedEntities = entities.Where(e => !_dbSet.Local.Contains(e));
            if (detachedEntities.Any())
            {
                _dbSet.AttachRange(detachedEntities);
            }
        }
    }
}
