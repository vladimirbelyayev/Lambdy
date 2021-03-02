using System;
using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;
using Lambdy.Resolvers.NameResolvers;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class MemberExpressionResolver : ExpressionResolver
    {
        public override ExpressionNode ResolveExpression(Expression expression)
        {
            var memberExpression = (MemberExpression) expression;
            return ResolveExpression(memberExpression, memberExpression);
        }

        private static ExpressionNode ResolveExpression(MemberExpression memberExpression, MemberExpression rootExpression)
        {
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