using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Templates.Common
{
    public static class Ensure
    {
        public const string EmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        public static void NotNull<T>(T param, string paramName)
        {
            if(param == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        public static void IsEmail(string value)
        {
            NotNull(value, nameof(value));

            var regex = new Regex(EmailRegex);
            if (!regex.IsMatch(value))
            {
                throw new FormatException("Invalid email format");
            }
        }
    }
}
