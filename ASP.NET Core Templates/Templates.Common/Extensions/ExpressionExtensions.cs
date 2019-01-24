using System;
using System.Collections.Generic;
using System.Text;

namespace System.Linq.Expressions
{
    public static class ExpressionExtensions
    {
        public static Expression<TDelegate> AndAlso<TDelegate>(this Expression<TDelegate> left, params Expression<TDelegate>[] rights)
        {
            Expression<TDelegate> exp = left;
            var parameters = exp.Parameters;
            foreach (var right in rights)
            {
                exp = Expression.Lambda<TDelegate>(Expression.AndAlso(exp, Expression.Invoke(right, parameters)), parameters);
            }
            return exp;
        }

        public static Expression<TDelegate> AndAlso<TDelegate>(this Expression<TDelegate> left, IEnumerable<Expression<TDelegate>> rights)
        {
            Expression<TDelegate> exp = left;
            var parameters = exp.Parameters;
            foreach (var right in rights)
            {
                exp = Expression.Lambda<TDelegate>(Expression.AndAlso(exp.Body, Expression.Invoke(right, parameters)), parameters);
            }
            return exp;
        }
    }
}
