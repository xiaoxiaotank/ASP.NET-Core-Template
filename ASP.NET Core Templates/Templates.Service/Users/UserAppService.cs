using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Templates.Core.Repositories.Users;
using Templates.EntityFrameworkCore.Entities;
using System.Security.Cryptography;
using Templates.Common.Helpers;

namespace Templates.Application.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly Encoding encoding = Encoding.UTF8;
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        public IQueryable<User> Get(Expression<Func<User, bool>> predicate)
        {
            return _userRepository.Get(predicate);
        }

        public IQueryable<User> Get()
        {
            return _userRepository.Get();
        }

        public User Login(string userNameOrEmail, string password)
        {
            password = CryptographyHelper.EncryptByMD5(password);
            return _userRepository.Get(u => (u.UserName.Equals(userNameOrEmail) || u.Email.Equals(userNameOrEmail)) && u.Password.Equals(password))
                .FirstOrDefault();
        }

        public User Create(User user)
        {
            user.Password = CryptographyHelper.EncryptByMD5("123456");
            return _userRepository.Insert(user);
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

        public void Delete(int id)
        {
            _userRepository.Delete(_userRepository.Get(id));
        }

        public void Delete(IEnumerable<int> ids)
        {
            _userRepository.Delete(_userRepository.Get(u => ids.Contains(u.Id)));
        }

    }
}
