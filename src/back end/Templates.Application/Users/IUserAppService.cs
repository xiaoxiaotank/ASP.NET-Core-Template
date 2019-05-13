using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Templates.Common.Attributes;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.Application.Users
{
    /// <summary>
    /// 使用id的部分疑虑？？？：
    ///     1. 明确为 int 类型， 提供明确的参数id
    ///     2. 采用Entity，类型与实体类型始终保持统一， 但参数不够明确（只传递id？），使用不方便
    ///     =>
    ///     主键类型不易变，采用明确类型参数id
    /// </summary>
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

        User Create([NotNull]User user);

        void Create([NotNull]IEnumerable<User> users);

        Task<User> CreateAsync([NotNull]User user);

        User Update([NotNull]User user);

        Task<User> UpdateAsync([NotNull]User user);

        void Delete(int id);

        void Delete([NotNull]IEnumerable<int> ids);

        Task DeleteAsync(int id);

        Task DeleteAsync([NotNull]IEnumerable<int> ids);

    }
}
