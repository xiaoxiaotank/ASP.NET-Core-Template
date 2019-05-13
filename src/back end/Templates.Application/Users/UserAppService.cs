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
using Templates.Common;

namespace Templates.Application.Users
{
    public class UserAppService : IUserAppService
    {
        public const string DefaultPassword = "123456";

        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int Count() => _userRepository.Count();

        public int Count(Expression<Func<User, bool>> predicate) 
            => predicate == null ? Count() : _userRepository.Count(predicate);

        public async Task<int> CountAsync() => await _userRepository.CountAsync();

        public async Task<int> CountAsync(Expression<Func<User, bool>> predicate) 
            => await (predicate == null ? CountAsync() : _userRepository.CountAsync(predicate));

        public User Get(int id) => _userRepository.Get(id);

        public IQueryable<User> Get(Expression<Func<User, bool>> predicate)
            => predicate == null ? Get() : _userRepository.Get(predicate);

        public IQueryable<User> Get() => _userRepository.Get();

        public async Task<User> GetAsync(int id) => await _userRepository.GetAsync(id);       

        public User Create(User user)
        {
            if(_userRepository.Get(u => u.UserName == user.UserName).Any())
            {
                throw new AppException(string.Format(ServiceValidation.Existing, "用户名"));
            }

            user.Password = PasswordHasher.HashPassword(DefaultPassword);
            return _userRepository.Insert(user);
        }
        public async Task<User> CreateAsync(User user)
        {
            if (await _userRepository.Get(u => u.UserName == user.UserName).AnyAsync())
            {
                throw new AppException(string.Format(ServiceValidation.Existing, "用户名"));
            }

            user.Password = PasswordHasher.HashPassword(DefaultPassword);
            return await _userRepository.InsertAsync(user);
        }

        public void Create(IEnumerable<User> users)
        {
            users = users.Where(u => u != null).Select(u =>
            {
                u.Password = PasswordHasher.HashPassword(DefaultPassword);
                return u;
            });

            if (users.IsNotEmpty())
            {
                _userRepository.Insert(users);
            }
        }

        public User Update(User user)
        {
            var origUser = _userRepository.Get(user.Id);
            Ensure.Found(origUser);

            origUser.Name = user.Name;
            origUser.Gender = user.Gender;
            return _userRepository.Update(origUser);
        }

        public async Task<User> UpdateAsync(User user)
        {
            var origUser = await _userRepository.GetAsync(user.Id);
            Ensure.Found(origUser);

            origUser.Name = user.Name;
            origUser.Gender = user.Gender;
            return await _userRepository.UpdateAsync(origUser);
        }

        public void Delete(int id)
        {
            var user = _userRepository.Get(id);
            Ensure.Found(user);

            _userRepository.Delete(user);
        }

        public void Delete(IEnumerable<int> ids) => _userRepository.Delete(_userRepository.Get(u => ids.Contains(u.Id)));

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetAsync(id);
            Ensure.Found(user);

            await _userRepository.DeleteAsync(user);
        }

        public async Task DeleteAsync(IEnumerable<int> ids) => await _userRepository.DeleteAsync(_userRepository.Get(u => ids.Contains(u.Id)));

    }
}
