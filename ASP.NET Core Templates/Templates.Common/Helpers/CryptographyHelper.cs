using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Templates.Common.Helpers
{
    public sealed class CryptographyHelper
    {
        public static string EncryptByMD5(string plainText)
        {
            return EncryptByMD5(plainText, Encoding.UTF8);
        }

        public static string EncryptByMD5(string plainText, Encoding encoding)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(encoding.GetBytes(plainText));
                var sb = new StringBuilder();
                foreach (var t in hash)
                {
                    sb.Append(t.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
