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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext ctx) : base(ctx)
        {
        }
    }
}
