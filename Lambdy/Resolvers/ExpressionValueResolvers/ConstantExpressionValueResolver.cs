using System.Linq.Expressions;
using Lambdy.Resolvers.ExpressionValueResolvers.Abstract;

namespace Lambdy.Resolvers.ExpressionValueResolvers
{
    internal class ConstantExpressionValueResolver : ExpressionValueResolver
    {
        public override object GetValue(Expression expression)
        {
            return ((ConstantExpression)expression).Value;
        }
    }
}