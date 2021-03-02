using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class BinaryExpressionResolver : ExpressionResolver
    {
        public override ExpressionNode ResolveExpression(Expression expression)
        {
            var binaryExpression = (BinaryExpression) expression;

            return new OperationNode
            {
                Left = ExpressionResolverMediator
                    .ResolveExpression(binaryExpression.Left),
                Operator = binaryExpression.NodeType,
                Right = ExpressionResolverMediator
                    .ResolveExpression(binaryExpression.Right)
            };
        }
    }
}