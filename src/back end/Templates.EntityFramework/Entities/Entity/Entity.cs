using System;
using System.Collections.Generic;
using System.Text;

namespace Templates.EntityFrameworkCore.Entities
{

    public abstract class Entity : Entity<int>
    {
    }

    public abstract class Entity<TKey> : IEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; set; }
    }


}
