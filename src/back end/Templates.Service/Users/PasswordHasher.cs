using System;
using System.Collections.Generic;
using System.Text;
using Templates.Common.Helpers;

namespace Templates.Application.Users
{
    public class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return CryptographyHelper.EncryptByMD5(password);
        }

        public static bool VerifyPassword(string password, string hash)
        {
            return CryptographyHelper.EncryptByMD5(password) == hash;
        }
    }
}
