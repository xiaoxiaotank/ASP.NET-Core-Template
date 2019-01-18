using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Templates.Core.Repositories
{
    public interface IRepository<TEntity, TKey> 
        where TEntity : class
        where TKey : IEquatable<TKey>
    {
        TEntity Get(TKey id);

        IEnumerable<TEntity> Get(Func<TEntity, bool> selector);

        void Insert(TEntity entity);

        void Insert(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);

        void Delete(IEnumerable<TEntity> entities);
    }

    public interface IRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : class
    {

    }
}
