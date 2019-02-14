using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    public static class CollectionExtensions
    {
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

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

        public static bool NotContains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            return !source.Contains(value);
        }

        public static IQueryable<T> Paged<T>(this IQueryable<T> source, int page, int size)
        {
            return source.Skip((page - 1) * size).Take(size);
        }
    }
}
