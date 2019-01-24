using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        public static bool IsNotNullAndWhiteSpace(this string value) => !string.IsNullOrWhiteSpace(value);

        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);
        
        public static bool IsNotNullAndEmpty(this string value) => !string.IsNullOrEmpty(value);

        public static bool NotEquals(this string value1, string value2) => !value1.Equals(value2);

        /// <summary>
        /// 去除所有空格
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string RemoveAllWhiteSpace(this string value) => Regex.Replace(value, @"\s", "");

        /// <summary>
        /// 去除两边小括号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimParentheses(this string value) => value.Trim('(', ')');
    }
}
