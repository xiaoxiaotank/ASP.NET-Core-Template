using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Templates.Core.Repositories
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

        public virtual TEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.AsNoTracking().Where(predicate);
        }

        public virtual IQueryable<TEntity> Get()
        {
            return _dbSet.AsNoTracking();
        }

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

        protected virtual int Save() => _ctx.SaveChanges();
    }
}
