using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class ConstantExpressionResolver : ExpressionResolver
    {
        public override ExpressionNode ResolveExpression(Expression expression)
        {
            return new ValueNode
            {
                Value = ExpressionValueResolverMediator.GetValue(expression)
            };
        }
    }
}