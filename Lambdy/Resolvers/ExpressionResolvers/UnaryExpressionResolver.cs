using System.Linq.Expressions;
using Lambdy.ExpressionNodes;
using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class UnaryExpressionResolver : ExpressionResolver
    {
        public override Node ResolveExpression(Expression expression)
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