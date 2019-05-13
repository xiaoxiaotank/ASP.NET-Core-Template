using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Templates.Common.Attributes;
using Templates.EntityFrameworkCore.Entities;

namespace Templates.Application.Authentications
{
    public interface IAuthenticationAppService
    {
        User Login([NotNull]string userNameOrEmail, [NotNull]string password);

        Task<User> LoginAsync([NotNull]string userNameOrEmail, [NotNull]string password);

        void ChangePassword(int id, [NotNull]string oldPassword, [NotNull]string newPassword);

        Task ChangePasswordAsync(int id, [NotNull]string oldPassword, [NotNull]string newPassword);
    }
}
