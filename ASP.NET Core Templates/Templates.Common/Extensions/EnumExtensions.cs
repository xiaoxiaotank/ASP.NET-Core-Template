using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Runtime.Serialization;

namespace Templates.Common.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// 获取枚举值的描述（Description）
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum value, string separator = ",")
        {
            var type = value.GetType();
            var descs = value.ToString()
                .Split(',')
                .Select(name =>
                {
                    var desc = type.GetField(name.Trim(' '))?
                        .GetCustomAttribute<DescriptionAttribute>()?
                        .Description;
                    return desc ?? name;
                });

            return string.Join(separator, descs);
        }

        /// <summary>
        /// 获取枚举值的display name
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetDisplayName(this Enum value, string separator = ",")
        {
            var type = value.GetType();
            var displayNames = value.ToString()
                .Split(',')
                .Select(name =>
                {
                    var displayName = type.GetField(name.Trim(' '))?
                        .GetCustomAttribute<DisplayNameAttribute>()?
                        .DisplayName;
                    return displayName ?? name;
                });

            return string.Join(separator, displayNames);
        }

        /// <summary>
        /// 获取枚举值的enum member name
        /// </summary>
        /// <param name="value"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string GetMemberValue(this Enum value, string separator = ",")
        {
            var type = value.GetType();
            var memberValues = value.ToString()
                .Split(',')
                .Select(name =>
                {
                    var memberValue = type.GetField(name.Trim(' '))?
                        .GetCustomAttribute<EnumMemberAttribute>()?
                        .Value;
                    return memberValue ?? name;
                });

            return string.Join(separator, memberValues);
        }
    }
}
