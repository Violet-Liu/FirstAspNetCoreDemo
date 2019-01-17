using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Mall.Common.Extension
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Determines whether <see cref="IEnumerable<T>"/> is empty or null
        /// </summary>
        /// <typeparam name="T">generic object</typeparam>
        /// <param name="source">data source</param>
        /// <returns>bool</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.Count() <= 0;
        }

        public static IQueryable<T> DataSorting<T>(this IQueryable<T> source, string orderExpression, bool ascending)
        {
            //错误查询
            if (string.IsNullOrEmpty(orderExpression))
            {
                return source;
            }
            string sortingDir = string.Empty;
            if (ascending)
                sortingDir = "OrderBy";
            else
                sortingDir = "OrderByDescending";
            Expression sourceexpression = source.Expression;
            var elementType = typeof(T);
            ParameterExpression param = Expression.Parameter(elementType, "o");
            PropertyInfo pi = typeof(T).GetProperty(orderExpression);
            Type[] types = new Type[2];
            types[0] = elementType;
            types[1] = pi.PropertyType;
            Expression expr = Expression.Call(typeof(Queryable), sortingDir, types, sourceexpression,
                Expression.Lambda(Expression.Property(param, orderExpression), param));
            IQueryable<T> query = source.AsQueryable().Provider.CreateQuery<T>(expr);
            return query;
        }
    }
}
