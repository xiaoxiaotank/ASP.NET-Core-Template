using System;
using System.Collections.Generic;
using System.Text;
using Templates.EntityFrameworkCore.Entities;
using Templates.EntityFrameworkCore.Repositories;

namespace Templates.Core.Repositories.Users
{
    public interface IUserRepository : IRepository<User>
    {
    }
}
