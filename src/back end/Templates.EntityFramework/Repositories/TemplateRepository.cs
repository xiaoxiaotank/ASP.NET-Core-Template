using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.EntityFrameworkCore.Repositories
{
    public abstract class TemplateRepository<TEntity> : TemplateRepository<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        public TemplateRepository(DbContext ctx) : base(ctx)
        {
        }
    }

    public abstract class TemplateRepository<TEntity, TKey> : EfCoreRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public TemplateRepository(DbContext ctx) : base(ctx)
        {
        }
    }
}
