using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Templates.Common.Helpers
{
    public sealed class CryptographyHelper
    {
        #region MD5
        public static string EncryptByMD5(string plainText) => EncryptByMD5(plainText, Encoding.UTF8);
        public static string EncryptByMD5(string plainText, Encoding encoding)
        {
            var buffer = encoding.GetBytes(plainText);
            return EncryptByMD5(buffer, encoding);
        }

        public static string EncryptByMD5(byte[] buffer) => EncryptByMD5(buffer, Encoding.UTF8);
        public static string EncryptByMD5(byte[] buffer, Encoding encoding)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(buffer);
                return FromHash(hash);
            }
        }

        public static string EncryptByMD5(Stream inputStream) => EncryptByMD5(inputStream, Encoding.UTF8);
        public static string EncryptByMD5(Stream inputStream, Encoding encoding)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(inputStream);
                return FromHash(hash);
            }
        } 

        private static string FromHash(byte[] hash)
        {
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("x2"));
            }

            return sb.ToString();
        }
        #endregion
    }
}
