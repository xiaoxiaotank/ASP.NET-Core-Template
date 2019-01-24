using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// 判断集合是否为null或空
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// 判断集合是否不为null和空
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsNotNullAndEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source != null && source.Any();
        }

        /// <summary>
        /// 通过键获取值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool TryGetValueSafely<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dic, TKey key, out TValue value)
        {
            value = default(TValue);
            return key != null && dic != null && dic.TryGetValue(key, out value);
        }

        /// <summary>
        /// 判断字典是否包含键
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool ContainsKeySafely<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dic, TKey key)
        {
            return dic != null && key != null && dic.ContainsKey(key);
        }

        /// <summary>
        /// 通过使用默认的相等比较器确定序列是否 不包含 指定的元素
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool NotContains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            return !source.Contains(value);
        }
    }
}
