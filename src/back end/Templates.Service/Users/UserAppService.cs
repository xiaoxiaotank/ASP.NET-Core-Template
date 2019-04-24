using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Templates.Core.Repositories.Users;
using Templates.EntityFrameworkCore.Entities;
using System.Security.Cryptography;
using Templates.Common.Helpers;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Templates.Application.Users
{
    public class UserAppService : IUserAppService
    {
        public const string DefaultPassword = "123456";

        private readonly Encoding encoding = Encoding.UTF8;
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int Count() => _userRepository.Count();

        public int Count(Expression<Func<User, bool>> predicate) => _userRepository.Count(predicate);

        public async Task<int> CountAsync() => await _userRepository.CountAsync();

        public async Task<int> CountAsync(Expression<Func<User, bool>> predicate) => await _userRepository.CountAsync(predicate);

        public User Get(int id) => _userRepository.Get(id);

        public IQueryable<User> Get(Expression<Func<User, bool>> predicate) => _userRepository.Get(predicate);

        public IQueryable<User> Get() => _userRepository.Get();

        public async Task<User> GetAsync(int id) => await _userRepository.GetAsync(id);

        public User Login(string userNameOrEmail, string password)
        {
            var user = _userRepository.Get(u => (u.UserName.Equals(userNameOrEmail) || u.Email.Equals(userNameOrEmail))).SingleOrDefault();
            if(user == null)
            {
                throw new AppException("用户名或邮箱不存在");
            }

            if (!PasswordHasher.VerifyPassword(password, user.Password))
            {
                throw new AppException("密码错误");
            }

            user.Password = string.Empty;
            return user;
        }

        public async Task<User> LoginAsync(string userNameOrEmail, string password)
        {
            var user = await _userRepository.Get(u => (u.UserName.Equals(userNameOrEmail) || u.Email.Equals(userNameOrEmail))).SingleOrDefaultAsync();
            if (user == null)
            {
                throw new AppException("用户名或邮箱不存在");
            }
            if (!PasswordHasher.VerifyPassword(password, user.Password))
            {
                throw new AppException("密码错误");
            }

            user.Password = string.Empty;
            return user;
        }

        public void ChangePassword(int id, string oldPassword, string newPassword)
        {
            var user = _userRepository.Get(id);
            if(user == null)
            {
                throw new NotFoundException();
            }
            if(PasswordHasher.VerifyPassword(oldPassword, user.Password))
            {
                throw new AppException("旧密码不正确！");
            }

            user.Password = PasswordHasher.HashPassword(newPassword);
            _userRepository.Update(user);
        }

        public async Task ChangePasswordAsync(int id, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new NotFoundException();
            }
            if (PasswordHasher.VerifyPassword(oldPassword, user.Password))
            {
                throw new AppException("旧密码不正确！");
            }

            user.Password = PasswordHasher.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);
        }

        public User Create(User user)
        {
            if(_userRepository.Get(u => u.UserName == user.UserName).Any())
            {
                throw new AppException("用户名已存在！");
            }

            user.Password = PasswordHasher.HashPassword(DefaultPassword);
            return _userRepository.Insert(user);
        }
        public async Task<User> CreateAsync(User user)
        {
            if (await _userRepository.Get(u => u.UserName == user.UserName).AnyAsync())
            {
                throw new AppException("用户名已存在！");
            }

            user.Password = PasswordHasher.HashPassword(DefaultPassword);
            return await _userRepository.InsertAsync(user);
        }

        public void Create(IEnumerable<User> user)
        {
            _userRepository.Insert(user.Select(u =>
            {
                u.Password = PasswordHasher.HashPassword(DefaultPassword);
                return u;
            }));
        }

        public User Update(User user)
        {
            var origUser = _userRepository.Get(user.Id);
            if(origUser == null)
            {
                throw new NotFoundException();
            }

            origUser.Name = user.Name;
            origUser.Gender = user.Gender;
            return _userRepository.Update(origUser);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var origUser = await _userRepository.GetAsync(user.Id);
            if (origUser == null)
            {
                throw new NotFoundException();
            }

            origUser.Name = user.Name;
            origUser.Gender = user.Gender;
            return await _userRepository.UpdateAsync(origUser);
        }

        public void Delete(int id) => _userRepository.Delete(_userRepository.Get(id));

        public void Delete(IEnumerable<int> ids) => _userRepository.Delete(_userRepository.Get(u => ids.Contains(u.Id)));

        public async Task DeleteAsync(int id) => await _userRepository.DeleteAsync(_userRepository.Get(id));

        public async Task DeleteAsync(IEnumerable<int> ids) => await _userRepository.DeleteAsync(_userRepository.Get(u => ids.Contains(u.Id)));

    }
}
