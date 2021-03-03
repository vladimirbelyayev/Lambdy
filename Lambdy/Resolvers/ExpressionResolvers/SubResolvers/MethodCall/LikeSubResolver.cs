using System;
using System.Linq.Expressions;
using Lambdy.Resolvers.NameResolvers;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;
using Lambdy.ValueObjects;

namespace Lambdy.Resolvers.ExpressionResolvers.SubResolvers.MethodCall
{
    internal class LikeSubResolver
    {
        public ExpressionNode ResolveExpression(MethodCallExpression callExpression)
        {
            var member = (MemberExpression) callExpression.Object;
            var fieldValue = ExpressionValueResolverMediator.GetValue(callExpression.Arguments[0]);
            
            var callFunction = (LikeMethod) Enum.Parse(typeof(LikeMethod), callExpression.Method.Name, true);
                
            return new LikeNode
            {
                MemberNode = new MemberNode
                {
                    TableName = TableNameResolver.GetTableName(member),
                    FieldName = ColumnNameResolver.GetColumnName(callExpression.Object)
                },
                Method = callFunction,
                Value = new ValueNode()
                {
                    Value = fieldValue
                }
            };
        }
    }
}