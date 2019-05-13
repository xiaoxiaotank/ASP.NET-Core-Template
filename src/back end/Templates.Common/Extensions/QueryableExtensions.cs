using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq
{
    public static class QueryableExtensions
    {
        public static bool IsNullOrEmpty<TSource>(this IQueryable<TSource> source)
        {
            return source == null || !source.Any();
        }

        public static bool IsNotNullAndEmpty<TSource>(this IQueryable<TSource> source)
        {
            return !IsNullOrEmpty(source);
        }

        public static bool IsEmpty<TSource>(this IQueryable<TSource> sources)
        {
            return !sources.Any();
        }

        public static bool IsNotEmpty<TSource>(this IQueryable<TSource> sources)
        {
            return !IsEmpty(sources);
        }

        public static bool NotContains<TSource>(this IQueryable<TSource> source, TSource value)
        {
            return !source.Contains(value);
        }

        public static IQueryable<T> Paged<T>(this IQueryable<T> source, int page, int size)
        {
            return source.Skip((page - 1) * size).Take(size);
        }
    }
}
