using System.Linq.Expressions;
using Lambdy.TreeNodes.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers.Abstract
{
    internal abstract class ExpressionResolver
    {
        public abstract ExpressionNode ResolveExpression(Expression expression);
    }
}