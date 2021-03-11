using System;
using System.Linq.Expressions;
using Lambdy.Constants;

namespace Lambdy.Resolvers.NameResolvers
{
    internal static class TableNameResolver
    {
        private static string GetTableFromType(Type type)
        {
            return type.Name;
        }

        public static string GetTableName(MemberExpression expression)
        {
            if(expression.Expression is MemberExpression memberExpr)
            {
                if (expression.Member.Name == CSharpNullable.UnderlyingValueAccessor &&
                    Nullable.GetUnderlyingType(memberExpr.Type) != null)
                {
                    // Is nullable type and has .Value accessor
                    // need to fetch table name from encasedExpression
                    var encasedExpression = ((MemberExpression) memberExpr.Expression);
                    return encasedExpression.Member.Name;
                }
                return memberExpr.Member.Name;
            } 
            else
            {
                return GetTableFromType(expression.Expression.Type);
            }
        }
    }
}