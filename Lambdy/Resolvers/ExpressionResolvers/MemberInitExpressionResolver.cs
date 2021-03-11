using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;
using Lambdy.Resolvers.NameResolvers;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class MemberInitExpressionResolver : ExpressionResolver
    {
        public override ExpressionNode ResolveExpression(Expression expression)
        {
            var memberInitExpression = (MemberInitExpression) expression;
            var expressionBindingsLength = memberInitExpression.Bindings.Count;
            var constructorNode =  new ConstructorNode()
            {
                ConstructorArgumentNodes = new ConstructorArgumentNode[expressionBindingsLength]
            };
            
            var constructorArgumentNodes = constructorNode.ConstructorArgumentNodes;
            for (var i = 0; i < expressionBindingsLength; i++)
            {
                var memberBinding = (MemberAssignment) memberInitExpression.Bindings[i];

                constructorArgumentNodes[i] = new ConstructorArgumentNode()
                {
                    Left = new ConstructorMemberNode()
                    {
                        FieldName = ColumnNameResolver.GetColumnName(memberBinding.Member)
                    },
                    Right = ExpressionResolverMediator.ResolveExpression(memberBinding.Expression)
                };
            }

            return constructorNode;
        }
    }
}