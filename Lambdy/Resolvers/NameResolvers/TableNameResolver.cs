using System;
using System.Linq.Expressions;

namespace Lambdy.Resolvers.NameResolvers
{
    internal static class TableNameResolver
    {
        public static string GetTableName<T>()
        {
            return GetTableFromType(typeof(T));
        }

        private static string GetTableFromType(Type type)
        {
            return type.Name;
        }

        public static string GetTableName(MemberExpression expression)
        {
            if(expression.Expression is MemberExpression memberExpr)
            {
                return memberExpr.Member.Name;
            } 
            else
            {
                return GetTableFromType(expression.Expression.Type);
            }
        }
    }
}