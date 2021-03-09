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

            var left = ExpressionResolverMediator
                .ResolveExpression(binaryExpression.Left);
            var right = ExpressionResolverMediator
                .ResolveExpression(binaryExpression.Right);

            if (right is ValueNode valueNode)
            {
                var isNullComparision = valueNode.Value == null;
                if (isNullComparision)
                {
                    return new NullOperationNode
                    {
                        Left = left,
                        Operator = binaryExpression.NodeType
                    };
                }
            }
            
            return new OperationNode
            {
                Left = left,
                Operator = binaryExpression.NodeType,
                Right = right
            };
        }
    }
}