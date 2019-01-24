using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Templates.Core.Repositories
{
    public interface IRepository<TEntity, TKey> 
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        TEntity Get(TKey id);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Get();

        TEntity Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        int Delete(TEntity entity);

        int Delete(IEnumerable<TEntity> entities);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : class
    {

    }
}
