using System;
using System.Linq.Expressions;
using System.Reflection;
using Lambdy.Constants;

namespace Lambdy.Resolvers.NameResolvers
{
    internal static class ColumnNameResolver
    {
        private static MemberExpression GetMemberExpression(Expression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return expression as MemberExpression;
                case ExpressionType.Convert:
                    return GetMemberExpression((expression as UnaryExpression)?.Operand);
            }

            throw new ArgumentException("Member expression expected");
        }

        public static string GetColumnName(Expression expression)
        {
            var member = GetMemberExpression(expression);
            return GetColumnName(member);
        }

        public static string GetColumnName(MemberExpression member)
        {
            if (member.Member.Name == CSharpNullable.UnderlyingValueAccessor &&
                Nullable.GetUnderlyingType(member.Expression.Type) != null)
            {
                // Is nullable type and has .Value accessor
                // need to fetch table name from encasedExpression
                var encasedExpression = ((MemberExpression) member.Expression);
                return GetColumnName(encasedExpression.Member);
            }
            
            return GetColumnName(member.Member);
        }

        public static string GetColumnName(MemberInfo memberInfo)
        {
            return memberInfo.Name;
        }
    }
}