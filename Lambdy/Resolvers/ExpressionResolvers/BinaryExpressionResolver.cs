using System.Linq.Expressions;
using Lambdy.ExpressionNodes;
using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class BinaryExpressionResolver : ExpressionResolver
    {
        public override Node ResolveExpression(Expression expression)
        {
            var binaryExpression = (BinaryExpression) expression;
            var left = ExpressionResolverMediator
                .ResolveExpression(binaryExpression.Left);

            var right = ExpressionResolverMediator
                .ResolveExpression(binaryExpression.Right);
            
            return new OperationNode
            {
                Left = left,
                Operator = binaryExpression.NodeType,
                Right = right
            };
        }
    }
}