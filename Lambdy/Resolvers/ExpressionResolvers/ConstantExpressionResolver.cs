using System.Linq.Expressions;
using Lambdy.ExpressionNodes;
using Lambdy.ExpressionNodes.Abstract;
using Lambdy.Resolvers.ExpressionResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers
{
    internal class ConstantExpressionResolver : ExpressionResolver
    {
        public override Node ResolveExpression(Expression expression)
        {
            var value = ExpressionValueResolverMediator.GetValue(expression);
            return new ValueNode { Value = value };
        }
    }
}