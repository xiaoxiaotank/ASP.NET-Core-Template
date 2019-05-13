using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

        public static bool IsNotNullAndEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return !IsNullOrEmpty(source);
        }

        public static bool IsEmpty<TSource>(this IEnumerable<TSource> sources)
        {
            return !sources.Any();
        }

        public static bool IsNotEmpty<TSource>(this IEnumerable<TSource> sources)
        {
            return !IsEmpty(sources);
        }

        public static bool NotContains<TSource>(this IEnumerable<TSource> source, TSource value)
        {
            return !source.Contains(value);
        }

        public static IEnumerable<T> Paged<T>(this IEnumerable<T> source, int page, int size)
        {
            return source.Skip((page - 1) * size).Take(size);
        }
    }
}
