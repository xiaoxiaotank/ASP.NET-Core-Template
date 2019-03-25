using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Templates.EntityFrameworkCore.Entities;
using Templates.EntityFrameworkCore.Repositories;

namespace Templates.Core.Repositories.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContext ctx) : base(ctx)
        {
        }

        public override User Insert(User entity)
        {
            entity.CreationTime = DateTime.Now;
            return base.Insert(entity);
        }

        public override void Insert(IEnumerable<User> entities)
        {
            var now = DateTime.Now;
            base.Insert(entities.Select(e => 
            {
                e.CreationTime = now;
                return e;
            }));
        }

        public override async Task<User> InsertAsync(User entity)
        {
            entity.CreationTime = DateTime.Now;
            return await base.InsertAsync(entity);
        }

        public override async Task InsertAsync(IEnumerable<User> entities)
        {
            var now = DateTime.Now;
            await base.InsertAsync(entities.Select(e =>
            {
                e.CreationTime = now;
                return e;
            }));
        }
    }
}
