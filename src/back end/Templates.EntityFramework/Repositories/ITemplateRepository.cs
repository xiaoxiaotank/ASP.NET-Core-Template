using System;
using System.Collections.Generic;
using System.Text;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.EntityFrameworkCore.Repositories
{
    public interface ITemplateRepository<TEntity> : IRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {

    }


    public interface ITemplateRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
    }
}
