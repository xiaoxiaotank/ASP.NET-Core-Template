using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Templates.EntityFrameworkCore.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _ctx;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(DbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }

        public int Count() => _dbSet.Count();

        public int Count(Expression<Func<TEntity, bool>> predicate) => _dbSet.Count(predicate);

        public async Task<int> CountAsync() => await _dbSet.CountAsync();

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.CountAsync(predicate);

        public long LongCount() => _dbSet.LongCount();

        public long LongCount(Expression<Func<TEntity, bool>> predicate) => _dbSet.LongCount();

        public async Task<long> LongCountAsync() => await _dbSet.LongCountAsync();

        public async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.LongCountAsync(predicate);

        public virtual TEntity Get(int id) => _dbSet.Find(id);

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
            => _dbSet.AsNoTracking().Where(predicate);

        public virtual IQueryable<TEntity> Get() => _dbSet.AsNoTracking();

        public async Task<TEntity> GetAsync(int id) => await _dbSet.FindAsync(id);


        public virtual TEntity Insert(TEntity entity)
        {
            var entry = _dbSet.Add(entity);
            Save();
            return entry.Entity;
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            Save();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entry = await _dbSet.AddAsync(entity);
            await SaveAsync();
            return entry.Entity;
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await SaveAsync();
        }

        public virtual TEntity Update(TEntity entity)
        {
            var entry = _dbSet.Update(entity);
            Save();
            return entry.Entity;
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            Save();
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var entry = await Task.Run(() => _dbSet.Update(entity));
            await SaveAsync();
            return entry.Entity;
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _dbSet.UpdateRange(entities));
            await SaveAsync();
        }

        public virtual int Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            return Save();
        }

        public virtual int Delete(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return Save();
        }

        public async Task<int> DeleteAsync(TEntity entity)
        {
            await Task.Run(() => _dbSet.Remove(entity));
            return await SaveAsync();
        }

        public async Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        {
            await Task.Run(() => _dbSet.RemoveRange(entities));
            return await SaveAsync();
        }

        protected virtual int Save() => _ctx.SaveChanges();

        protected virtual async Task<int> SaveAsync() => await _ctx.SaveChangesAsync();




      
    }
}
