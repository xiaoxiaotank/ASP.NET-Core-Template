﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
