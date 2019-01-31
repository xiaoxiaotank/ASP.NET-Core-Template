﻿using System;
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
            password = CryptographyHelper.EncryptByMD5(password);
            return _userRepository.Get(u => (u.UserName.Equals(userNameOrEmail) || u.Email.Equals(userNameOrEmail)) && u.Password.Equals(password)).FirstOrDefault();
        }

        public async Task<User> LoginAsync(string userNameOrEmail, string password)
        {
            password = CryptographyHelper.EncryptByMD5(password);
            return await _userRepository.Get(u => (u.UserName.Equals(userNameOrEmail) || u.Email.Equals(userNameOrEmail)) && u.Password.Equals(password)).FirstOrDefaultAsync();
        }

        public void ChangePassword(int id, string oldPassword, string newPassword)
        {
            var user = _userRepository.Get(id);
            if(user == null)
            {
                throw new NotFoundException();
            }
            if(user.Password != CryptographyHelper.EncryptByMD5(oldPassword))
            {
                throw new AppException("旧密码不正确！");
            }

            user.Password = CryptographyHelper.EncryptByMD5(newPassword);
            _userRepository.Update(user);
        }

        public async Task ChangePasswordAsync(int id, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetAsync(id);
            if (user == null)
            {
                throw new NotFoundException();
            }
            if (user.Password != CryptographyHelper.EncryptByMD5(oldPassword))
            {
                throw new AppException("旧密码不正确！");
            }

            user.Password = CryptographyHelper.EncryptByMD5(newPassword);
            await _userRepository.UpdateAsync(user);
        }

        public User Create(User user)
        {
            user.Password = CryptographyHelper.EncryptByMD5(DefaultPassword);
            return _userRepository.Insert(user);
        }
        public async Task<User> CreateAsync(User user)
        {
            user.Password = CryptographyHelper.EncryptByMD5(DefaultPassword);
            return await _userRepository.InsertAsync(user);
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
