using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.Application.Users
{
    public interface IUserAppService
    {
        int Count();

        int Count(Expression<Func<User, bool>> predicate);

        Task<int> CountAsync();

        Task<int> CountAsync(Expression<Func<User, bool>> predicate);

        User Get(int id);

        IQueryable<User> Get(Expression<Func<User, bool>> predicate);

        IQueryable<User> Get();

        Task<User> GetAsync(int id);

        User Login(string userNameOrEmail, string password);

        Task<User> LoginAsync(string userNameOrEmail, string password);

        void ChangePassword(int id, string oldPassword, string newPassword);

        Task ChangePasswordAsync(int id, string oldPassword, string newPassword);

        User Create(User user);

        void Create(IEnumerable<User> user);

        Task<User> CreateAsync(User user);

        User Update(User user);

        Task<User> UpdateAsync(User user);

        void Delete(int id);

        void Delete(IEnumerable<int> ids);

        Task DeleteAsync(int id);

        Task DeleteAsync(IEnumerable<int> ids);

    }
}
