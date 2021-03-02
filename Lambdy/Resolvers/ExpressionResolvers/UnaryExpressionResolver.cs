using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class UnaryExpressionResolver : ExpressionResolver
    {
        public override ExpressionNode ResolveExpression(Expression expression)
        {
            var unaryExpression = (UnaryExpression) expression;
            return new SingleOperationNode
            {
                Operator = expression.NodeType,
                Child = ExpressionResolverMediator.ResolveExpression(unaryExpression.Operand)            
            };
        }
    }
}