using System.Linq.Expressions;
using Lambdy.ExpressionNodes.Abstract;

namespace Lambdy.Resolvers.ExpressionResolvers.Abstract
{
    internal abstract class ExpressionResolver
    {
        public abstract Node ResolveExpression(Expression expression);
    }
}