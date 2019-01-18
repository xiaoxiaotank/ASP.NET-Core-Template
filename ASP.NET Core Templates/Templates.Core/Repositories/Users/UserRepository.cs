using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Templates.EntityFrameworkCore.Models;

namespace Templates.Core.Repositories.Users
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DbContext ctx) : base(ctx)
        {
        }
    }
}
