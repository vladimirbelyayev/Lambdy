using System.Linq.Expressions;
using Lambdy.TreeNodes.ExpressionNodes;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers.SubResolvers.MethodCall
{
    internal class MethodResultSubResolver
    {
        public ExpressionNode ResolveExpression(MethodCallExpression methodCallExpression)
        {
            return new ValueNode
            {
                Value = ExpressionValueResolverMediator.GetValue(methodCallExpression)
            };
        }
    }
}