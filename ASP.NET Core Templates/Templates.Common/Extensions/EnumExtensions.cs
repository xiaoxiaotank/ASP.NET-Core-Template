using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using System.Linq;

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
    }
}
