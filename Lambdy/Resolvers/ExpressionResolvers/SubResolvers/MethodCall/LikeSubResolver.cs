using System.Linq.Expressions;
using Lambdy.Maps;
using Lambdy.Resolvers.NameResolvers;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers.SubResolvers.MethodCall
{
    internal class LikeSubResolver
    {
        public ExpressionNode ResolveExpression(MethodCallExpression callExpression)
        {
            var member = (MemberExpression) callExpression.Object;
            var fieldValue = ExpressionValueResolverMediator.GetValue(callExpression.Arguments[0]);
            
            return new LikeNode
            {
                MemberNode = new MemberNode
                {
                    TableName = TableNameResolver.GetTableName(member),
                    FieldName = ColumnNameResolver.GetColumnName(callExpression.Object)
                },
                Method = LikeOperationMethodMap.OperationMethods[callExpression.Method.Name],
                Value = new ValueNode()
                {
                    Value = fieldValue
                }
            };
        }
    }
}