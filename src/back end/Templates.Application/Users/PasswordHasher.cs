using System;
using System.Collections.Generic;
using System.Text;
using Templates.Common;
using Templates.Common.Helpers;

namespace Templates.Application.Users
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            Ensure.NotNull(password, nameof(password));

            return CryptographyHelper.EncryptByMD5(password);
        }

        public static bool VerifyPassword(string password, string hash)
            => HashPassword(password) == hash;

    }
}
