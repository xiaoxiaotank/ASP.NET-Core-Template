using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> selector)
        {
            throw new NotImplementedException();
        }

        public virtual void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Insert(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    
        public virtual void Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

    }
}
