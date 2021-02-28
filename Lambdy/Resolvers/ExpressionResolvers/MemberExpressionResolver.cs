using System;
using System.Linq.Expressions;
using Lambdy.ExpressionNodes;
using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;
using Lambdy.Resolvers.NameResolvers;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class MemberExpressionResolver : ExpressionResolver
    {
        public override Node ResolveExpression(Expression expression)
        {
            return ResolveExpression((MemberExpression) expression, null);
        }

        private static Node ResolveExpression(MemberExpression memberExpression, MemberExpression rootExpression)
        {
            if (rootExpression == null)
            {
                rootExpression = memberExpression;
            }

            if (memberExpression.Expression == null)
            {
                return new ValueNode
                {
                    Value = ExpressionValueResolverMediator.GetValue(rootExpression)
                };
            }

            switch (memberExpression.Expression.NodeType)
            {
                case ExpressionType.Parameter:
                    return new MemberNode
                    {
                        TableName = TableNameResolver.GetTableName(rootExpression),
                        FieldName = ColumnNameResolver.GetColumnName(rootExpression)
                    };

                case ExpressionType.MemberAccess:
                    return ResolveExpression((MemberExpression) memberExpression.Expression, rootExpression);

                case ExpressionType.Call:
                case ExpressionType.Constant:
                    return new ValueNode
                    {
                        Value = ExpressionValueResolverMediator.GetValue(rootExpression)
                    };

                default:
                    throw new ArgumentException("Expected member expression");
            }
        }
    }
}