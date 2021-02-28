using System;
using System.Linq.Expressions;
using System.Reflection;

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
        
        public static string GetColumnName<T>(Expression<Func<T, object>> selector)
        {
            return GetColumnName(GetMemberExpression(selector.Body));
        }

        public static string GetColumnName(Expression expression)
        {
            var member = GetMemberExpression(expression);
            return GetColumnName(member);
        }

        public static string GetColumnName(MemberAssignment expression)
        {
            return GetColumnName(expression.Member);
        }

        public static string GetColumnName(MemberExpression member)
        {
            return GetColumnName(member.Member);
        }

        public static string GetColumnName(MemberInfo memberInfo)
        {
            return memberInfo.Name;
        }
    }
}