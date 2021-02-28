using System;
using System.Linq.Expressions;
using System.Reflection;
using Lambdy.Resolvers.ExpressionValueResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionValueResolvers
{
    internal class MemberAccessExpressionValueResolver : ExpressionValueResolver
    {
        public override object GetValue(Expression expression)
        {
            var memberExpr = (MemberExpression) expression;
            if (memberExpr.Expression == null)
            {
                return ResolveValue(memberExpr.Member, null);
            }

            var obj = ExpressionValueResolverMediator.GetValue(memberExpr.Expression);
            return ResolveValue(memberExpr.Member, obj);
        }
        
        private static object ResolveValue(MemberInfo member, object obj)
        {
            switch (member)
            {
                case PropertyInfo propertyInfo:
                    return propertyInfo.GetValue(obj, null);
                case FieldInfo fieldInfo:
                    return fieldInfo.GetValue(obj);
                default:
                    throw new ArgumentException($"Member '{member.Name}' is not supported");
            }
        }
    }
}