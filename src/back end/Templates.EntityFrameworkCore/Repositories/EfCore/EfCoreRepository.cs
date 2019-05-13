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
    public abstract class EfCoreRepository<TEntity, TKey> : Repository<TEntity, TKey> 
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly DbContext _ctx;
        protected readonly DbSet<TEntity> _dbSet;

        public EfCoreRepository(DbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }

        public override async Task<int> CountAsync() => await Get().CountAsync();

        public override async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate) => await Get(predicate).CountAsync();

        public override async Task<long> LongCountAsync() => await Get().LongCountAsync();

        public override async Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate) => await Get(predicate).LongCountAsync();

        public override IQueryable<TEntity> Get() => _dbSet.AsNoTracking();

        public override TEntity Insert(TEntity entity)
        {
            var entry = _dbSet.Add(entity);
            Save();
            return entry.Entity;
        }

        public override int Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
            return Save();
        }

        public override async Task<TEntity> InsertAsync(TEntity entity)
        {
            var entry = await _dbSet.AddAsync(entity);
            await SaveAsync();
            return entry.Entity;
        }

        public override async Task<int> InsertAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return await SaveAsync();
        }

        public override TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            var entry = _dbSet.Update(entity);
            Save();
            return entry.Entity;
        }

        public override int Update(IEnumerable<TEntity> entities)
        {
            AttachIfNot(entities);
            _dbSet.UpdateRange(entities);
            return Save();
        }

        public override async Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            return await base.UpdateAsync(entity);
        }

        public override async Task<int> UpdateAsync(IEnumerable<TEntity> entities)
        {
            AttachIfNot(entities);
            return await base.UpdateAsync(entities);
        }

        public override int Delete(TEntity entity)
        {
            AttachIfNot(entity);
            _dbSet.Remove(entity);
            return Save();
        }

        public override int Delete(IEnumerable<TEntity> entities)
        {
            AttachIfNot(entities);
            _dbSet.RemoveRange(entities);
            return Save();
        }


        public override async Task<int> DeleteAsync(TEntity entity)
        {
            AttachIfNot(entity);
            _dbSet.Remove(entity);
            return await SaveAsync();
        }

        public override async Task<int> DeleteAsync(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return await SaveAsync();
        }

        /// <summary>
        /// 保存改动
        /// </summary>
        /// <returns></returns>
        protected virtual int Save() => _ctx.SaveChanges();

        /// <summary>
        /// 保存改动
        /// </summary>
        /// <returns></returns>
        protected virtual async Task<int> SaveAsync() => await _ctx.SaveChangesAsync();

        /// <summary>
        /// 如果没有附加，则附加到数据库上下文
        /// </summary>
        /// <param name="entity">要附加的实体</param>
        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!_dbSet.Local.Contains(entity))
            {
                _dbSet.Attach(entity);
            }
        }

        /// <summary>
        /// 如果没有附加，则附加到数据库上下文
        /// </summary>
        /// <param name="entities">要附加的实体集合</param>
        protected virtual void AttachIfNot(IEnumerable<TEntity> entities)
        {
            var detachedEntities = entities.Where(e => !_dbSet.Local.Contains(e));
            if (detachedEntities.IsNotEmpty())
            {
                _dbSet.AttachRange(detachedEntities);
            }
        }
    }
}
