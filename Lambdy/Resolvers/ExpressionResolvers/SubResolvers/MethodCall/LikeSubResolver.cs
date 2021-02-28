using System;
using System.Linq;
using System.Linq.Expressions;
using Lambdy.ExpressionNodes;
using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Resolvers.NameResolvers;
using Lambdy.ValueObjects;

namespace Lambdy.Resolvers.ExpressionResolvers.SubResolvers.MethodCall
{
    internal class LikeSubResolver
    {
        public Node ResolveExpression(MethodCallExpression callExpression)
        {
            var member = (MemberExpression) callExpression.Object;
            var fieldValue = ExpressionValueResolverMediator.GetValue(callExpression.Arguments.First());
            
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