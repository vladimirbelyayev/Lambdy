using System.Linq.Expressions;
using Lambdy.ExpressionNodes;
using Lambdy.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers.SubResolvers.MethodCall
{
    internal class MethodResultResolver
    {
        public Node ResolveExpression(MethodCallExpression methodCallExpression)
        {
            var value = ExpressionValueResolverMediator.GetValue(methodCallExpression);
            return new ValueNode
            {
                Value = value
            };
        }
    }
}