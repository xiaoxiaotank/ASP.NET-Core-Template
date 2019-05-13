using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Templates.Application.Users;
using Templates.Common;
using Templates.Core.Repositories.Users;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.Application.Authentications
{
    public class AuthenticationAppService : IAuthenticationAppService
    {
        private readonly IUserRepository _userRepository;

        public AuthenticationAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Login(string userNameOrEmail, string password)
        {
            var user = _userRepository.Get(u => (u.UserName.Equals(userNameOrEmail) || u.Email.Equals(userNameOrEmail))).SingleOrDefault();
            Ensure.Found(user, string.Format(ServiceValidation.NotExist, "用户名或邮箱"));

            if (!PasswordHasher.VerifyPassword(password, user.Password))
            {
                throw new AppException(ServiceValidation.PasswordError);
            }

            user.Password = string.Empty;
            return user;
        }

        public async Task<User> LoginAsync(string userNameOrEmail, string password)
        {
            var user = await _userRepository.Get(u => (u.UserName.Equals(userNameOrEmail) || u.Email.Equals(userNameOrEmail))).SingleOrDefaultAsync();
            Ensure.Found(user, string.Format(ServiceValidation.NotExist, "用户名或邮箱"));

            if (!PasswordHasher.VerifyPassword(password, user.Password))
            {
                throw new AppException(ServiceValidation.PasswordError);
            }

            user.Password = string.Empty;
            return user;
        }

        public void ChangePassword(int id, string oldPassword, string newPassword)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                throw new NotFoundException();
            }
            if (!PasswordHasher.VerifyPassword(oldPassword, user.Password))
            {
                throw new AppException(string.Format(ServiceValidation.Incorrect, "旧密码"));
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
            if (!PasswordHasher.VerifyPassword(oldPassword, user.Password))
            {
                throw new AppException(string.Format(ServiceValidation.Incorrect, "旧密码"));
            }

            user.Password = PasswordHasher.HashPassword(newPassword);
            await _userRepository.UpdateAsync(user);
        }
    }
}
