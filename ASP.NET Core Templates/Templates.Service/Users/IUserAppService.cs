using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Templates.EntityFrameworkCore.Models;

namespace Templates.Application.Users
{
    public interface IUserAppService
    {
        User Get(int id);

        IQueryable<User> Get(Expression<Func<User, bool>> predicate);

        IQueryable<User> Get();

        User Login(string userNameOrEmail, string password);

        User Create(User user);

        User Update(User user);

        void Delete(int id);

        void Delete(IEnumerable<int> ids);
    }
}
