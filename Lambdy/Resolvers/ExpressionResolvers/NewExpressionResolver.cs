using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;
using Lambdy.Resolvers.NameResolvers;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class NewExpressionResolver : ExpressionResolver
    {
        public override ExpressionNode ResolveExpression(Expression expression)
        {
            var newExpression = (NewExpression) expression;

            var expressionArgumentLength = newExpression.Arguments.Count;
            var constructorNode =  new ConstructorNode()
            {
                ConstructorArgumentNodes = new ConstructorArgumentNode[expressionArgumentLength]
            };
            var constructorArgumentNodes = constructorNode.ConstructorArgumentNodes;
            
            // ReSharper disable once ForCanBeConvertedToForeach (Reason - performance)
            for (var i = 0; i < expressionArgumentLength; i++)
            {
                var memberExpression = (MemberExpression) newExpression.Arguments[i];

                constructorArgumentNodes[i] = new ConstructorArgumentNode()
                {
                    Left = new ConstructorMemberNode()
                    {
                        FieldName = ColumnNameResolver.GetColumnName(newExpression.Members[i])
                    },
                    Right = ExpressionResolverMediator.ResolveExpression(memberExpression)
                };
            }

            return constructorNode;
        }
    }
}