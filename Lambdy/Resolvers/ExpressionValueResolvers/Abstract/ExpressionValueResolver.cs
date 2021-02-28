using System.Linq.Expressions;

namespace Lambdy.Resolvers.ExpressionValueResolvers.Abstract
{
    internal abstract class ExpressionValueResolver
    {
        public abstract object GetValue(Expression expression);
    }
}